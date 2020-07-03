import React, { Component } from "react";
import { AsyncStorage } from 'AsyncStorage';

import apiBase from '../../../services/base';

const AUTH_KEY = "AUTHORIZATION_KEY";

export default class Ingredient extends Component {
  state = {
    ingredient: {
      name: "",
      quantity: 0
    }
  }

  constructor(props) {
    super(props)
  }

  async saveIngredient() {
    const config = await this.getAuthorization();
    await apiBase.post('/Ingredients', {name: this.state.ingredient.name, quantity: +this.state.ingredient.quantity }, config );
    this.props.updateIngredients();
  }

  handleChange(event) {
    switch (event.target.id) {
      case "txtName":
        this.setState({ ingredient: { ...this.state.ingredient, name: event.target.value } })
        break;
      case "txtQuantity":
        this.setState({ ingredient: { ...this.state.ingredient, quantity: event.target.value } })
        break;
    
      default:
        break;
    }
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
    return (
      <div role="main">
        <h4>Cadastro de Ingrediente</h4>
        <div>
          <div className="row">
            <div className="col-lg-8">
              <label id="lblNome">Nome:</label>
              <input id="txtName" type="text" className="form-control" autoComplete="off" onChange={this.handleChange.bind(this)} />
            </div>
            <div className="col-lg-3">
              <label id="lblValor">Quantidade:</label>
              <input id="txtQuantity" type="number" className="form-control" onChange={this.handleChange.bind(this)} />
            </div>
            <div className="col-lg-1">
              <label id="lblValor">&nbsp;</label>
              <input id="btnCadastroIngrediente" type="button" className="btn btn-primary" value="Cadastrar" onClick={this.saveIngredient.bind(this)} />
            </div>
          </div>
        </div>
      </div>
    );
  }
}
