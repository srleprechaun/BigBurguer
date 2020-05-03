import React, { Component } from 'react';
import logo from '../../assets/img/logos/logo.png'
import './style.css'

import $ from 'jquery';
import Popper from 'popper.js';

export default class Header extends Component {
  state = {
      isOpen: false
  };

  componentDidMount() {
    $(document).ready(function () {
      $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
          $('#scroll').fadeIn();
        } else {
          $('#scroll').fadeOut();
        }
      });
      $('#scroll').click(function () {
        scrollToTop();
        return false;
      });
    });
    const scrollToTop = () => {
      const c = document.documentElement.scrollTop || document.body.scrollTop;
      if (c > 0) {
        window.requestAnimationFrame(scrollToTop);
        window.scrollTo(0, c - c / 8);
      }
    }
  }

  toggleOpen = () => this.setState({ isOpen: !this.state.isOpen });

  render () {
    const menuClass = `dropdown-menu dropdown-menu-right${this.state.isOpen ? " show" : ""}`;

    return (
      <header>
        <a href="" id="scroll" style={{display: "none"}}><span></span></a>
        <div className="navbar navbar-dark bg-light shadow-sm">
          <div className="container d-flex justify-content-between">
            <a className="navbar-brand d-flex align-items-center">
              <img src={logo} alt="Logotipo Big Burguer" width="100" height="59.8"></img>
            </a>
            <div className="btn-group" onClick={this.toggleOpen}>
              <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                aria-haspopup="true" aria-expanded="false">
                <span className="navbar-toggler-icon"></span>
              </button>
              <div className={menuClass}>
                <a href="" className="dropdown-item">Carrinho</a>
                <a href="" className="dropdown-item">Minhas compras</a>
                <a href="" className="dropdown-item">Meus dados</a>
                <a href="" className="dropdown-item">Sair</a>
                <a href="" className="dropdown-item">Entrar</a>
              </div>
            </div>
          </div>
        </div>
      </header>
    )
  }
}