import React, { Component } from 'react';
import { AsyncStorage } from 'AsyncStorage';
import apiBase from '../../services/base';
import './style.css';

const AUTH_KEY = "AUTHORIZATION_KEY";

export default class Login extends Component {
  state = {
    login: {
      login: "",
      password: ""
    }
  }

  async handleSubmit(event) {
    const request = { password: this.state.login.password };
    if (this.state.login.login.match("[0-9]{11}"))
      request.cpf = this.state.login.login;
    else 
      request.email = this.state.login.login

    const response = await apiBase.post('/Auth/login', request)
    .catch(function (error) {
      if (error.response) {
        if (error.response.status === 400) {
          alert('Login ou senha inválidos.');
        }
        else {
          alert('Ocorreu um erro ao processar a requisição');
        }
      }
    });

    if (response && response.data) {
      await this._storeData(AUTH_KEY, response.data);
      window.location = "http://localhost:3000/";
    }
    event.preventDefault();
  }

  handleChange(event) {
    switch (event.target.id) {
      case "txtLogin":
        this.setState({login: {...this.state.login, login: event.target.value}})
        break;
      case "txtPassword":
        this.setState({login: {...this.state.login, password: event.target.value}})
        break;
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

  render() {
    return (
      <div role="main">
        <div className="container">
          <div id="login" >
            <h1>Login</h1>
            <form>
              <div>
                <label>CPF ou E-mail:</label>
                <input id="txtLogin" type="text" className="form-control" onChange={this.handleChange.bind(this)} ></input>
              </div>
              <div>
                <label>Senha:</label>
                <input id="txtPassword" type="password" className="form-control" onChange={this.handleChange.bind(this)} ></input>
              </div>
              <input id="btnSubmit" type="button" value="Logar" className="btn btn-primary" onClick={this.handleSubmit.bind(this)}></input>
            </form>
          </div>
        </div>
      </div>
  )
}
}