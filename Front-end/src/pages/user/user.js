import React, { Component } from 'react';
import apiBase from '../../services/base';

const days = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 ];
const months = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 ];

export default class User extends Component {
  state = {
    user:{
      name: "",
      cpf: "",
      password: "",
      birthday: {
        day: 0,
        month: 0,
        year: ""
      },
      email: ""
    },
    passwordRepeat: ""
  }

  handleChange(event) {
    switch (event.target.id) {
      case "txtName":
        this.setState({ user: { ...this.state.user, name: event.target.value } })
        break;
      case "txtPassword":
        this.setState({ user: { ...this.state.user, password: event.target.value } })
        break;
      case "txtPasswordRepeat":
        this.setState({ passwordRepeat: event.target.value })
        break;
      case "txtEmail":
        this.setState({ user: { ...this.state.user, email: event.target.value } })
        break;
      case "slcDay":
        this.setState({ user: { ...this.state.user, birthday: {...this.state.user.birthday, day: event.target.value} } })
        break;
      case "slcMonth":
        this.setState({ user: { ...this.state.user, birthday: {...this.state.user.birthday, month: event.target.value} } })
        break;
      case "txtYear":
        this.setState({ user: { ...this.state.user, birthday: {...this.state.user.birthday, year: event.target.value} } })
        break;
      case "txtCpf":
        this.setState({ user: { ...this.state.user, cpf: event.target.value } })
        break;
      
      default:
        break;
    }
  }

  async handleSubmit(event) {
    let state = this.state;
    if (state.user.password !== state.passwordRepeat) {
      alert('As senhas não coincidem.');
    } else {
      let userRequest = { birthDate: "", password: "", name: "", cpf: "", email: "" };
      
      userRequest.birthDate = state.user.birthday.year + "-" + state.user.birthday.month + "-" + state.user.birthday.day;
      userRequest.password = state.user.password;
      userRequest.name = state.user.name;
      userRequest.cpf = state.user.cpf;
      userRequest.email = state.user.email;

      console.log(userRequest);

      const response = await apiBase.post('/Auth/register', userRequest)
      .catch(function (error) {
        if (error.response) {
          if (error.response.status === 401) {
            alert('Você não possui autorização para esta ação.');
          }
          else {
            alert('Ocorreu um erro ao cadastrar o usuario.');
          }
        }
      });

      if (response) {
        alert('Usuário Cadastrado com Sucesso');
        window.location = "http://localhost:3000/login";
      }

      event.preventDefault();
    }
  }

  render () {
    return (
      <div role="main">
        <section className="jumbotron text-center" style={{paddingTop: "30px", paddingBottom: "10px", marginBottom: "0px"}}>
          <div className="container">
          <h1 className="jumbotron-heading">Cadasto de Usuario</h1>
          <p className="lead text-muted"></p>
          </div>
        </section>
        <div className="container">
          <div id='cadastroUsuario'>
            <h1>Cadastro de Usuario</h1>
            <form>
              <div className="row">
                <div className="col-md-6">
                  <label id="lblName">Nome Completo:</label><br/>
                  <input id="txtName" type="text" className="form-control" autoComplete="off"
                          value={this.state.user.name} onChange={this.handleChange.bind(this)} />
                </div>
              </div>
              <br />
              <div className="row">
                <div className="col-md-6">
                    <label id="lblPassword">Senha:</label><br/>
                    <input id="txtPassword" type="password" className="form-control" autoComplete="off"
                            value={this.state.user.password} onChange={this.handleChange.bind(this)} />
                </div>

                <div className="col-md-6">
                  <label id="lblPasswordReapt">Digite a senha novamente:</label><br/>
                  <input id="txtPasswordRepeat" type="password" className="form-control" autoComplete="off"
                          value={this.state.passwordRepeat}  onChange={this.handleChange.bind(this)} />
                </div>
              </div>
              <br />
              <div className="row">
                <div className="col-md-6">
                    <label id="lblEmail">Email:</label><br/>
                    <input id="txtEmail" type="email" className="form-control" autoComplete="off"
                            value={this.state.user.email} onChange={this.handleChange.bind(this)}/>
                </div>
              </div>
              <br />
              <div className="row">
                <div className="col-md-6">
                  <label id="lblBirthday">Data de Nascimento:</label><br/>
                  <select id="slcDay" className="custom-select my-1 mr-sm-2" onChange={this.handleChange.bind(this)}>
                    <option value="" defaultValue>Dia</option>
                    {days.map(d => <option key ={d} value={d} >{d}</option>)}
                  </select>

                  <select id="slcMonth" className="custom-select my-1 mr-sm-2" onChange={this.handleChange.bind(this)}>
                    <option value="" defaultValue>Mês</option>
                    {months.map(d => <option key ={d} value={d} >{d}</option>)}
                  </select>

                  <input id="txtYear" type="txt" className="form-control" maxLength="4" autoComplete="off" placeholder="Ano"
                          value={this.state.user.birthday.year} onChange={this.handleChange.bind(this)} />
                </div>

                <div className="col-md-6">
                  <label id="lblCpf">CPF: (somente números)</label><br/>
                  <input id="txtCpf" type="text" className="form-control" maxLength="11" autoComplete="off"
                          value={this.state.user.cpf} onChange={this.handleChange.bind(this)}/>
                </div>
              </div>

              <br />
              <input id="btnSubmit" type="button" className="btn btn-primary" value="Cadastrar" onClick={this.handleSubmit.bind(this)} />
            </form>
          </div>
        </div>
      </div>
    );
  }
}