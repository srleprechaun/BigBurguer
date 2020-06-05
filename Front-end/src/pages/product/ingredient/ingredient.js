import React, { Component } from "react";
import PropTypes from 'prop-types';

export default class Ingredient extends Component {
  constructor(props) {
    super(props)
  }

  saveIngredient() {
    this.props.updateIngredients();
  }

  render() {
    return (
      <div role="main">
        <h4>Cadastro de Ingrediente</h4>
        <div>
          <div className="row">
            <div className="col-lg-6">
              <label id="lblNome">Nome:</label>
              <input id="txtNome" type="text" className="form-control" />
            </div>
            <div className="col-lg-3">
              <label id="lblValor">Valor:</label>
              <input id="txtValor" type="number" className="form-control" />
            </div>
            <div className="col-lg-2">
              <label id="lblValor">Quantidade:</label>
              <input id="txtValor" type="number" className="form-control" />
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
