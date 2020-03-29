import React, { Component } from 'react';
import cartLogo from '../../assets/img/logos/cart-icon.png';
import productimage from '../../assets/img/produtos/Hamburguers/hamburguer 1.jpg';

import './style.css';

export default class Home extends Component {
  render () {
    return (
      <main role="main">
        <img id="cart" src={cartLogo} alt="Carrinho"></img>
        <section class="jumbotron text-center" style={{paddingTop: "30px", paddingBottom: "10px", marginBottom: "0px"}}>
          <div class="container">
          <h1 class="jumbotron-heading">Big Burguer</h1>
          <p class="lead text-muted"></p>
          </div>
        </section>
        <div class="album py-5 bg-light">
          <div class="container">
            <div class="row">
              <h3 class="card-title pl-3">Lanches</h3>
            </div>
            <div class="row">
              <div class="col-md-3">
                <div class="card mb-3 shadow-sm">
                  <img src={productimage} alt="descricao" style={{width: "253px", height: "206px"}}></img>
                  <div class="card-body">
                    <p class="card-text">descricao</p>
                    <p class="card-text">R$ 10,00</p>
                    <div class="btn-group">
                      <button type="button" class="btn btn-sm btn-outline-primary rounded-pill">-</button>
                      <span class="pr-1 pl-1 pt-1">0</span>
                      <button type="button" class="btn btn-sm btn-outline-primary rounded-pill">+</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </main>
    )
  }
}