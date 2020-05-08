import React, { Component } from 'react';
import { AsyncStorage } from 'AsyncStorage';

import cartLogo from '../../assets/img/logos/cart-icon.png';
import apiBase from '../../services/base';

import './style.css';

const PRODUCTS_CART_KEY = "PRODUCTS_CART_KEY";

export default class Home extends Component {
  state = {
    products: []
  }

  componentDidMount() {
    this.loadProducts()
  }

  loadProducts = async () => {
      const response = await apiBase.get('/products');
      const products = [];
      
      Object.values(response.data).forEach(p => {
        products.push({ id: p.id, imageUrl: p.imageUrl, name: p.name, ingredients: p.ingredients, price: p.price, type: p.type, selected: 0 });
      });
      this.setState({ products: products });
      // this.selectQuantidadeNoCarrinho();
  };

  async adicionar(id) {
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
      }
    }
  }

  async remover(id) {
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
      }
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

  render () {
    const { products } = this.state;
    const hamburguers = [...products];
    const drinks = [...products];

    return (
      <main role="main">
        <img id="cart" src={cartLogo} alt="Carrinho"></img>
        <section className="jumbotron text-center" style={{paddingTop: "30px", paddingBottom: "10px", marginBottom: "0px"}}>
          <div className="container">
          <h1 className="jumbotron-heading">Big Burguer</h1>
          <p className="lead text-muted"></p>
          </div>
        </section>
        <div className="album py-5 bg-light">
          <div className="container">
            <div className="row">
              <h3 className="card-title pl-3">Lanches</h3>
            </div>
            <div className="row">
              {products.length > 0 && hamburguers.filter(p => p.type === "Hamburguer").map(product => (
                <div className="col-md-3" key={product.id}>
                  <div className="card mb-3 shadow-sm">
                    <img src={product.imageUrl} alt="descricao" style={{width: "253px", height: "206px"}}></img>
                    <div className="card-body">
                      <p className="card-text">{product.name}</p>
                      <p className="card-text">R$ {product.price}</p>
                      <div className="btn-group">
                        <button type="button" className="btn btn-sm btn-outline-primary rounded-pill" onClick={(e) => this.remover(product.id)}>-</button>
                        <span className="pr-1 pl-1 pt-1">{product.selected}</span>
                        <button type="button" className="btn btn-sm btn-outline-primary rounded-pill" onClick={(e) => this.adicionar(product.id)}>+</button>
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
            <div className="row">
              <h3 className="card-title pl-3">Bebida</h3>
            </div>
            <div className="row">
              {products.length > 0 && drinks.filter(p => p.type === "Drink").map(product => (
                <div className="col-md-3" key={product.id}>
                  <div className="card mb-3 shadow-sm">
                    <img src={product.imageUrl} alt="descricao" style={{width: "253px", height: "206px"}}></img>
                    <div className="card-body">
                      <p className="card-text">{product.name}</p>
                      <p className="card-text">R$ {product.price}</p>
                      <div className="btn-group">
                        <button type="button" className="btn btn-sm btn-outline-primary rounded-pill" onClick={(e) => this.remover(product.id)}>-</button>
                        <span className="pr-1 pl-1 pt-1">{product.selected}</span>
                        <button type="button" className="btn btn-sm btn-outline-primary rounded-pill" onClick={(e) => this.adicionar(product.id)}>+</button>
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
            {products.length === 0 && <h3 className="card-title pl-3">Nenhum produto encontrado</h3>}
          </div>
        </div>
      </main>
    )
  }
}