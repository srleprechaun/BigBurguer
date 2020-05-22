import React, { Component } from 'react';
import CurrencyInput from 'react-currency-input';

export default class Product extends Component {
  state = {
    product: {
      name: "",
      price: 0,
      ingredients: [],
      imageUrl: "",
      type: ""
    }
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

  handleSubmit(event) {
    console.log(this.state.product);
    event.preventDefault();
  }

  render () {
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
            <form onSubmit={this.handleSubmit.bind(this)}>
              <div>
                <label id="lblName">Nome:</label>
                <input id="txtName" type="text" className="form-control" onChange={this.handleChange.bind(this)} />
              </div>
              <div>
                <label id="lblPrice">Valor:</label>
                <CurrencyInput id="txtPrice" className="form-control" prefix="R$" decimalSeparator="," value={this.state.product.price} onChangeEvent={this.handleChange.bind(this)}/>
              </div>
              <div>
                <label id="lblType">Tipo:</label>
                <select id="slcType" className="custom-select my-1 mr-sm-2" value={this.state.value} onChange={this.handleChange.bind(this)}>
                  <option value="" defaultValue>Selecione</option>
                  <option value="1">Hamburguer</option>
                  <option value="2">Bebida</option>
                </select>
              </div>
              <div>
                <label id="lblIngredients">Lista de Ingredientes:</label>
                <input id="txtIngredients" type="text" className="form-control" onChange={this.handleChange.bind(this)} />
              </div>
              <div>
                <label id="lblProductUrl">Imagem do Produto:</label>
                <input id="txtProductUrl" type="text" className="form-control" onChange={this.handleChange.bind(this)} />
              </div>
              <br></br>
              <input id="btnSubmit" type="submit" className="btn btn-primary" value="Cadastrar Produto" />
            </form>
          </div>
        </div>
      </div>
    );
  }
}
