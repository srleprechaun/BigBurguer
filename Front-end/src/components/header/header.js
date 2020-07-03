import React, { Component } from 'react';
import { AsyncStorage } from 'AsyncStorage';
import logo from '../../assets/img/logos/logo.png';
import apiBase from '../../services/base';
import './style.css'

import $ from 'jquery';

const AUTH_KEY = "AUTHORIZATION_KEY";

export default class Header extends Component {
  state = {
      isOpen: false,
      logged: false,
      loggedUser: "",
      loggedUserRole: ""
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
    this.getLoggedUser();
  }

  toggleOpen = () => this.setState({ isOpen: !this.state.isOpen });

  async getLoggedUser() {
    var auth = await this._retrieveData(AUTH_KEY);
    var loggedUser = "Visitante";
    var loggedUserRole = "Customer";
    var logged = false;
    if (auth && auth.token) {
      let config = { headers: { 'Authorization': 'Bearer ' + auth.token } };
      let self = this;
      let user = await apiBase.get('/Users/' + auth.id, config)
      .catch(async function (error) {
        if (error.response) {
          await self._storeData(AUTH_KEY, {});
          window.location.reload();
        }
      });;
      if (user) {
        let userRole = await apiBase.get('/Users/' + auth.id + '/role', config);
        loggedUser = user.data && user.data.name ? user.data.name : "Visitante";
        loggedUserRole = userRole && userRole.data ? userRole.data : "Customer";
        logged = user ? true : false;
      }
    }
    this.setState({ loggedUser: loggedUser, logged: logged, loggedUserRole: loggedUserRole });
  }

  logout = async () => {
    await this._storeData(AUTH_KEY, {});
    window.location.reload();
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
    const menuClass = `dropdown-menu dropdown-menu-right${this.state.isOpen ? " show" : ""}`;
    const logged = this.state.logged;
    const loggedUser = this.state.loggedUser;
    const loggedUserRole = this.state.loggedUserRole;

    return (
      <header>
        <a href="" id="scroll" style={{display: "none"}}><span></span></a>
        <div className="navbar navbar-dark bg-light shadow-sm">
          <div className="container d-flex justify-content-between">
            <a className="navbar-brand d-flex align-items-center" href="/">
              <img src={logo} alt="Logotipo Big Burguer" width="100" height="59.8"></img>
            </a>
            <h6 className="card-title text-center">Olá, {loggedUser}!</h6>
            <div className="btn-group" onClick={this.toggleOpen}>
              <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                aria-haspopup="true" aria-expanded="false">
                <span className="navbar-toggler-icon"></span>
              </button>
              <div className={menuClass}>
                <a href="/carrinho" className="dropdown-item">Carrinho</a>
                <a href="/compras" className="dropdown-item">{(logged && loggedUserRole === 'Admin') ? "Vendas" :  "Minhas Compras"}</a>
                <a href="/produto" className="dropdown-item" hidden={!(logged && loggedUserRole === 'Admin')}>Cadastrar Produto</a>
                <a href="/conta" className="dropdown-item" hidden={!logged}>Meus dados</a>
                <a className="dropdown-item" onClick={this.logout.bind(this)} hidden={!logged}>Sair</a>
                <a href="/login" className="dropdown-item" hidden={logged}>Entrar</a>
              </div>
            </div>
          </div>
        </div>
      </header>
    )
  }
}