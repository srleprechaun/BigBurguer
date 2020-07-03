import React, { Component } from 'react';
import { AsyncStorage } from 'AsyncStorage';
import apiBase from '../../services/base';

import './style.css';

const AUTH_KEY = "AUTHORIZATION_KEY";
const orderStatus = [ 
  { id: 1, description: "Recebido" },
  { id: 2, description: "Em Preparo" },
  { id: 3, description: "Pronto" },
  { id: 4, description: "Entregue" },
];

export default class Order extends Component {
  state = {
    orders: [],
    latestOrder: 0,
    changeStatus: false
  }

  componentDidMount() {
    this.setUserPageType();
    setInterval(function() { window.location.reload() }, 10000);
  }

  async setUserPageType() {
    var loggedUser = await this._retrieveData(AUTH_KEY);
    if (!loggedUser.token) {
      alert('Você precisa estar logado para acessar essa página.');
      window.location = 'http://localhost:3000/login';
    } else {
      let config = { headers: { 'Authorization': 'Bearer ' + loggedUser.token } };
      let userRole = await apiBase.get('/Users/' + loggedUser.id +'/role', config);
      if (userRole.data === 'Customer')
        this.getOrders(loggedUser.id);
      else if (userRole.data === 'Admin' || userRole.data === 'Employee'){
        this.setState({ changeStatus: true });
        this.getOrders(null);
      }
      else {
        alert('Usuário não identificado.');
        window.location = 'http://localhost:3000/login';
      }

    }
  }

  async getOrders(userId) {
    const config = await this.getAuthorization();
    let url = userId ? `/Order/${userId}` : '/Order'
    const response = await apiBase.get(url, config)
    .catch(function (error) {
      if (error.response) {
        if (error.response.status === 401) {
          alert('Você não possui autorização para esta ação.');
        }
        else {
          alert('Ocorreu um erro ao recuperar suas compas.');
        }
      }
    });

    if (response) {
      const orders = [];
      
      Object.values(response.data).forEach(o => {
        let order = { id: o.id, products: "", senha: o.id, total: 0, status: "" };
        let products = "";
        let total = 0;
        o.orderProducts.forEach(p => { products += p.productName + " - "; total += p.quantity * p.price; });
        order.products = products.substring(0, products.length - 3);
        order.total = total;
        order.status = orderStatus.find(s => s.id === o.orderStatusId).description;
        orders.push(order);
      });

      if (orders.length > 0) {
        orders.sort(function(a, b){return b.id-a.id});
        this.setState({ orders: orders, latestOrder: orders[0].id });
      }
    }
  }

  async changeStatus(id) {
    const config = await this.getAuthorization();
    const response = await apiBase.put('/Order/' + id + "/OrderStatus", null, config)
    .catch(function (error) {
      if (error.response) {
        if (error.response.status === 401) {
          alert('Você não possui autorização para esta ação.');
        }
        else {
          alert('Ocorreu um erro ao mudar o status.');
        }
      }
    });
  }

  getAuthorization = async () => {
    const auth = await this._retrieveData(AUTH_KEY);
    if (auth) {
      return { headers: { 'Authorization': 'Bearer ' + auth.token } };
    }
    else {
      return null;
    }
  }

  _storeData = async (key, obj) => {
    try {
      let value = JSON.stringify(obj);
      await AsyncStorage.setItem(key, value);
    } catch (error) {
      console.log(error);
    }
  }

  _retrieveData = async (key) => {
    try {
      const value = await AsyncStorage.getItem(key);
      if (value) {
        return JSON.parse(value);
      }
    } catch (error) {
      console.log(error);
    }
  }

  render() {
    const { orders } = this.state;
    const latestOrder = this.state.latestOrder;
    const changeStatus = this.state.changeStatus;

    return (
      <div role="main">
        <section className="jumbotron text-center" style={{paddingTop: "30px", paddingBottom: "10px", marginBottom: "20px"}}>
          <div className="container">
            <h1 className="jumbotron-heading">Minhas Compras</h1>
            <p className="lead text-muted"></p>
          </div>
        </section>
        <div className="container">
          <div className="mx-auto col-10 col-sm-5 col-md-5 col-lg-3">
            <div className="div-senha">
              <span className="senha">{latestOrder}</span>
              <span className="label-senha">Senha atual.</span>
            </div>
          </div>
          <br />
          <div className="row">
            <div className="mx-auto" style={{width: "60%"}}>
              <div className="list-group">
                <div className="list-group-item list-group-item-action active">
                  <div className="row" className="text-center"><h4>Ultimas Compras</h4></div>
                  <div className="row">
                    <div className="col-lg-7">Produtos</div>
                    <div className="col-lg-1">Senha</div>
                    <div className="col-lg-2 text-center">Total</div>
                    <div className="col-lg-2 text-center">Status</div>
                  </div>
                </div>
                {orders.map(o => (
                  <div key={o.id} className="list-group-item list-group-item-action">
                      <div className="row">
                        <div className="col-lg-7">{o.products}</div>
                        <div className="col-lg-1">{o.senha}</div>
                        <div className="col-lg-2 text-center">R$ {o.total}</div>
                        <div className="col-lg-2 text-center">{o.status}
                          <button hidden={!changeStatus} type="button" className="btn btn-primary" onClick={(e) => this.changeStatus(o.id)}>Alterar</button>
                        </div>
                      </div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
      </div>
  )
}
}