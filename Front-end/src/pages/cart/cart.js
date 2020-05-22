import React, { Component } from 'react';
import { AsyncStorage } from 'AsyncStorage';
import apiBase from '../../services/base';

const PRODUCTS_CART_KEY = "PRODUCTS_CART_KEY";

export default class Cart extends Component {
  state = {
    products: [],
    formsOfpayment: [],
    selectedFormOfPayment: "",
    total: 0,
    canSubmit: true
  }

  componentDidMount() {
    this.getSelectedProducts();
    this.getFormsOfPayment();
  }

  async submitCart() {
    let { products, selectedFormOfPayment } = this.state;
    let cart = { products: [], formOfPayment: "" };
    products.forEach(p => cart.products.push({id: p.id, selected: p.selected}));
    cart.formOfPayment = selectedFormOfPayment;
    console.log(cart);
  }

  async getFormsOfPayment() {
    const response = await apiBase.get('/formsOfPayment');
    const formsOfPayment = [];
    
    Object.values(response.data).forEach(p => {
      formsOfPayment.push({ id: p.id, name: p.name});
    });
    this.setState({ formsOfPayment: formsOfPayment });
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

  selectFormOfPayment(event) {
    this.setState({ selectedFormOfPayment: event.target.value });
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
                <select className="custom-select" value={this.state.value} onChange={this.selectFormOfPayment.bind(this)}>
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