import React, { Component } from 'react';
import { AsyncStorage } from 'AsyncStorage';
import apiBase from '../../services/base';

import './style.css';

const AUTH_KEY = "AUTHORIZATION_KEY";

export default class Order extends Component {
  state = {
    login: {
      login: "",
      password: ""
    }
  }

  async handleSubmit(event) {
    const request = { password: this.state.login.password };
    if (this.state.login.login.match("[0-9]{11}"))
      request.cpf = this.state.login.login;
    else 
      request.email = this.state.login.login

    const response = await apiBase.post('/Auth/login', request)
    .catch(function (error) {
      if (error.response) {
        if (error.response.status === 400) {
          alert('Login ou senha inválidos.');
        }
        else {
          alert('Ocorreu um erro ao processar a requisição');
        }
      }
    });

    if (response && response.data) {
      await this._storeData(AUTH_KEY, response.data);
      window.location = "http://localhost:3000/";
    }
    event.preventDefault();
  }

  handleChange(event) {
    switch (event.target.id) {
      case "txtLogin":
        this.setState({login: {...this.state.login, login: event.target.value}})
        break;
      case "txtPassword":
        this.setState({login: {...this.state.login, password: event.target.value}})
        break;
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

  render() {
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
              <span className="senha">1234</span>
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
                <div className="list-group-item list-group-item-action">
                  <div className="row">
                    <div className="col-lg-7">Teste</div>
                    <div className="col-lg-1">1234</div>
                    <div className="col-lg-2 text-center">R$ 100,00</div>
                    <div className="col-lg-2 text-center">Recebido</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
  )
}
}