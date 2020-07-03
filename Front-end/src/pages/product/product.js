import React, { Component } from 'react';
import { AsyncStorage } from 'AsyncStorage';
import CurrencyInput from 'react-currency-input';
import Ingredient from './ingredient/ingredient';
import apiBase from '../../services/base';

import './style.css';

const AUTH_KEY = "AUTHORIZATION_KEY";

export default class Product extends Component {
  state = {
    product: {
      name: "",
      price: 0,
      ingredients: [],
      imageUrl: "",
      type: ""
    },
    ingredients: [],
    isOpen: false
  }

  productBase = {
    name: "",
    price: 0,
    ingredients: [],
    imageUrl: "",
    type: ""
  };

  componentDidMount() {
    this.verifyLoggedUser();
    this.updateIngredients();
  }

  handleChange(event, maskedvalue, floatvalue) {
    switch (event.target.id) {
      case "txtName":
        this.setState({ product: { ...this.state.product, name: event.target.value } })
        break;
      case "txtPrice":
        this.setState({ product: { ...this.state.product, price: floatvalue } })
        break;
      case "txtProductUrl":
        this.setState({ product: { ...this.state.product, imageUrl: event.target.value } })
        break;
      case "slcType":
        this.setState({ product: { ...this.state.product, type: event.target.value } })
        break;
    
      default:
        break;
    }
  }

  async handleSubmit(event) {
    let product = this.state.product;
    let ingredients = this.state.ingredients;
    product.ingredients = ingredients.filter(i => i.quantity > 0);
    console.log(product);

    const config = await this.getAuthorization();
    const response = await apiBase.post('/Products', product, config)
      .catch(function (error) {
        if (error.response) {
          if (error.response.status === 401) {
            alert('Você não possui autorização para esta ação.');
          }
          else {
            alert('Ocorreu um erro ao cadastrar o produto.');
          }
        }
      });

      if (response) {
        ingredients.forEach(i => i.quantity = 0);
            this.setState({ product: this.productBase, ingredients: ingredients });
            alert('Produto Cadastrado com Sucesso');
      }

  }

  updateIngredients = async () => {
    const response = await apiBase.get('/Ingredients');
    const ingredients = [];

    Object.values(response.data).forEach(p => {
      ingredients.push({ ingredientId: p.id, name: p.name, quantity: 0});
    });
    this.setState({ ingredients: ingredients });
  }

  changeIngredient(event, ingredientId) {
    let ingredients = this.state.ingredients;
    let ingredient = ingredients.find(i => i.ingredientId === ingredientId);
    if (ingredient)
      ingredient.quantity = +event.target.value;
    
    this.setState({ ingredients: ingredients })
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

  async verifyLoggedUser() {
    const auth = await this._retrieveData(AUTH_KEY);
    if (auth) {
      let config = { headers: { 'Authorization': 'Bearer ' + auth.token } };
      let userRole = await apiBase.get('/Users/' + auth.id +'/role', config);
      if (!(userRole.data === 'Admin' || userRole.data === 'Employee')){
        alert('Você não possui autorização para acessar essa página.');
        window.location = "http://localhost:3000";
      }
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

  toggleIngredientOpen = () => this.setState({ isOpen: !this.state.isOpen });

  render () {
    const ingredients = [...this.state.ingredients];
    
    return (
      <div role="main">
        <section className="jumbotron text-center" style={{paddingTop: "30px", paddingBottom: "10px", marginBottom: "0px"}}>
          <div className="container">
          <h1 className="jumbotron-heading">Gerenciamento de Produtos</h1>
          <p className="lead text-muted"></p>
          </div>
        </section>
        <div className="container">
          <div id='cadastroProduto'>
            <h1>Cadastro de Produto</h1>
            <form>
              <div>
                <label id="lblName">Nome:</label>
                <input id="txtName" type="text" value={this.state.product.name} className="form-control" onChange={this.handleChange.bind(this)} />
              </div>
              <div>
                <label id="lblPrice">Valor:</label>
                <CurrencyInput id="txtPrice" className="form-control" prefix="R$" decimalSeparator="," value={this.state.product.price} onChangeEvent={this.handleChange.bind(this)}/>
              </div>
              <div>
                <label id="lblType">Tipo:</label>
                <select id="slcType" className="custom-select my-1 mr-sm-2" value={this.state.product.type} onChange={this.handleChange.bind(this)}>
                  <option value="" defaultValue>Selecione</option>
                  <option value="Hamburguer">Hamburguer</option>
                  <option value="Drink">Bebida</option>
                </select>
              </div>
              <div>
                <label id="lblIngredients">Lista de Ingredientes:</label>
                {ingredients.map(i => (
                    <div key={"div" + i.ingredientId} className="row">
                      <div key={"div1" + i.ingredientId} className="col-lg-1">
                        <span key={i.ingredientId}>{i.name}</span>
                      </div>
                      <div key={"div2" + i.ingredientId} className="col-lg-1">
                        <input key={"input" + i.ingredientId} type="number" value={i.quantity} className="form-control" onChange={(e) => {this.changeIngredient(e, i.ingredientId)}}></input>
                      </div>
                    </div>
                  ))}
              </div>
              <p></p>
              <div className="btn-group" onClick={this.toggleIngredientOpen}>
                <button className="btn btn-primary" type="button" id="createIngredient" aria-haspopup="true" aria-expanded="false">
                  Cadastrar Ingrediente
                </button>
              </div>
              {this.state.isOpen &&
                <div>
                  <Ingredient updateIngredients={this.updateIngredients}></Ingredient>
                </div>
              }
              <p></p>
              <div>
                <label id="lblProductUrl">Imagem do Produto:</label>
                <input id="txtProductUrl" type="text" className="form-control" value={this.state.product.imageUrl} onChange={this.handleChange.bind(this)} />
              </div>
              <br></br>
              <input id="btnSubmit" type="button" className="btn btn-primary" value="Cadastrar Produto" onClick={this.handleSubmit.bind(this)} />
            </form>
          </div>
        </div>
      </div>
    );
  }
}
