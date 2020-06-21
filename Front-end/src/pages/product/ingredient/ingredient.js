import React, { Component } from "react";

import apiBase from '../../../services/base';

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
    await apiBase.post('/Ingredients', {name: this.state.ingredient.name, quantity: +this.state.ingredient.quantity } );
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
