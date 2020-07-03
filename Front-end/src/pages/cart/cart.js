import React, { Component } from 'react';
import { AsyncStorage } from 'AsyncStorage';
import apiBase from '../../services/base';

const AUTH_KEY = "AUTHORIZATION_KEY";
const PAYMENT_CART_KEY = "PAYMENT_CART_KEY";
const PRODUCTS_CART_KEY = "PRODUCTS_CART_KEY";

export default class Cart extends Component {
  state = {
    products: [],
    formsOfPayment: [
      { id: "1", name: "Cartão de Crédito" },
      { id: "2", name: "Cartão de Débito" },
      { id: "3", name: "Dinheiro" },
      { id: "4", name: "Vale Refeição" }
    ],
    selectedFormOfPayment: "",
    total: 0,
    canSubmit: true
  }

  componentDidMount() {
    this.getSelectedProducts();
    this.setSelectedFormOfPayment();
  }

  async submitCart() {
    let { products, selectedFormOfPayment } = this.state;
    let cart = { products: [], paymentMethodId: "", orderDate: null, customerId: "" };

    let customer;
    try
    {
      customer = await this.getLoggedCustomer();
      cart.customerId = customer.id;
    }
    catch (ex) {
      alert(ex);
      window.location = "http://localhost:3000/login";
      return;
    }

    products.forEach(p => cart.products.push({ productId: p.id, quantity: p.selected, price: p.price, discount: 0 }));
    cart.paymentMethodId = +selectedFormOfPayment;
    cart.orderDate = new Date().toISOString();
    const config = { headers: { 'Authorization': 'Bearer ' + customer.token } };

    const response = await apiBase.post('/Order', cart, config)
      .catch(function (error) {
        if (error.response) {
          if (error.response.status == 401) {
            alert('Você não possui autorização para esta ação.');
          }
          else {
            alert('Ocorreu um erro ao cadastrar a compra.');
          }
        }
      });

    if (response) {
      this._storeData(PRODUCTS_CART_KEY, []);
      this._storeData(PAYMENT_CART_KEY, { id: "" });
      this.getSelectedProducts();
      this.setSelectedFormOfPayment();
      window.location = "http://localhost:3000/compras";
    }
  }

  async getSelectedProducts() {
    let cartProducts = await this._retrieveData(PRODUCTS_CART_KEY);
    if (cartProducts) this.setState({ products: cartProducts });
    this.calculaTotal();
    this.validateCanSubmit();
  }

  calculaTotal() {
    let total = 0;
    this.state.products.forEach(x => total += x.price * x.selected);
    this.setState({ total: total });
  }

  async selectFormOfPayment(event) {
    this.setState({ selectedFormOfPayment: event.target.value });
    await this._storeData(PAYMENT_CART_KEY, { id: event.target.value });
    this.validateCanSubmit();
  }

  async setSelectedFormOfPayment() {
    let selected = await this._retrieveData(PAYMENT_CART_KEY);
    if (selected) {
      this.setState({ selectedFormOfPayment: selected.id });
    }
    this.validateCanSubmit();
  }

  async addToCart(id) {
    let products = this.state.products; 
    let product = products.find(x => x.id === id);
    if (product){
      let cartProducts = await this._retrieveData(PRODUCTS_CART_KEY);
      if (!cartProducts){
        cartProducts = [];
        cartProducts.push({ id: product.id, name: product.name, price: product.price, selected: 1 });
        ++product.selected;
        this._storeData(PRODUCTS_CART_KEY, cartProducts);
        this.setState({ products: products });
        this.calculaTotal();
      } else {
        let productInCart = cartProducts.find(p => p.id === id);
        if (productInCart) {
          ++productInCart.selected;
          ++product.selected;
        } else {
          cartProducts.push({ id: product.id, name: product.name, price: product.price, selected: 1 });
          ++product.selected;
        }
        this._storeData(PRODUCTS_CART_KEY, cartProducts);
        this.setState({ products: products });
        this.calculaTotal();
        this.validateCanSubmit();
      }
    }
  }

  async removeFromCart(id) {
    let products = this.state.products; 
    let product = products.find(x => x.id === id);
    if (product.selected > 0){
      let cartProducts = await this._retrieveData(PRODUCTS_CART_KEY);
      if (cartProducts){
        let productInCart = cartProducts.find(x => x.id === id);
        if (productInCart.selected > 1){
          --productInCart.selected;
          --product.selected;
        }
        else {
          cartProducts.splice(cartProducts.findIndex(p => p.id === id), 1);
          product.selected = 0;
        }
        this._storeData(PRODUCTS_CART_KEY, cartProducts);
        this.setState({ products: products });
        this.calculaTotal();
        this.validateCanSubmit();
      }
    }
  }

  validateCanSubmit() {
    let productsInCart = this.state.products;
    let productsValid = productsInCart && productsInCart.length > 0 && productsInCart.find(p => p.selected == 0) == null;
    this.setState({ canSubmit: productsValid });
  }

  async getLoggedCustomer() {
    const loggedUser = await this._retrieveData(AUTH_KEY);
    if (loggedUser && loggedUser.id) {
      return loggedUser;
    }
    else {
      throw new Error('Você precisa estar logado para completar essa ação.');
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
    const products = this.state.products;
    const formsOfPayment = this.state.formsOfPayment;
    const selectedFormOfPayment = this.state.selectedFormOfPayment;
    const canSubmit = this.state.canSubmit;
    const total = this.state.total;
    return (
      <main>
        <section className="jumbotron text-center" style={{paddingTop: "30px", paddingBottom: "10px", marginBottom: "0px"}}>
          <div className="container">
            <h1 className="jumbotron-heading">Carrinho de Compras</h1>
            <p className="lead text-muted"></p>
          </div>
        </section>
        <div className="container">
          <div className="div-carrinho">
            <br></br>
            <div className="mx-auto">
              <div className="list-group">
                <div className="list-group-item list-group-item-action active">
                  <div className="row">
                    <div className="col-lg-5">Produto</div>
                    <div className="col-lg-2">Unitário</div>
                    <div className="col-lg-2 text-center">Qtd</div>
                    <div className="col-lg-3 text-center">Total</div>
                  </div>
                </div>
                {products.length > 0 && products.map(product => (
                  <div className="list-group-item list-group-item-action" key={product.id}>
                    <div className="row">
                      <div className="col-lg-5">{product.name}</div>
                      <div className="col-lg-2">R$ {product.price}</div>
                      <div className="col-lg-2 text-center">
                        <div className="btn-group">
                          <button type="button" className="btn btn-sm btn-outline-primary rounded-pill" onClick={(e) => this.removeFromCart(product.id)}>-</button>
                          <span className="pr-1 pl-1 pt-1">{product.selected}</span>
                          <button type="button" className="btn btn-sm btn-outline-primary rounded-pill" onClick={(e) => this.addToCart(product.id)}>+</button>
                        </div>
                      </div>
                      <div className="col-lg-3 text-center">R$ {product.price * product.selected}</div>
                    </div>
                  </div>
                ))}
                {products.length <= 0 && 
                  <h3 className="mx-auto">Você ainda não selecionou nenhum produto.</h3>
                }
                <div className="list-group-item list-group-item-action">
                  <div className="row">
                    <div className="col-lg-3">Total</div>
                    <div className="col-lg-6"></div>
                    <div className="col-lg-3 text-center">R$ {total}</div>
                  </div>
                </div>
              </div>
            </div>
            <div className="row">
              <div className="mx-auto pt-3">
                <select className="custom-select" value={selectedFormOfPayment} onChange={this.selectFormOfPayment.bind(this)}>
                  <option defaultValue value="">Selecione uma forma de pagamento</option>
                  {formsOfPayment && formsOfPayment.map(f => (
                    <option value={f.id} key={f.id}>{f.name}</option>
                  ))}
                </select>
              </div>
            </div>
            <div className="row">
              <div className="mx-auto pt-3">
                <a href="/"><button className="btn btn-primary">Voltar as Compras</button></a>
              </div>
              <div className="mx-auto pt-3">
                <button className="btn btn-primary" onClick={this.submitCart.bind(this)} disabled={!(canSubmit && selectedFormOfPayment !== "")}>Finalizar compra</button>
              </div>
            </div>
          </div>
        </div>
      </main>
    );
  }
}
