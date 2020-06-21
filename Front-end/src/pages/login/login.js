import React, { Component } from 'react';
import './style.css';

export default class Product extends Component {
    state = {
        login: {
            login: "",
            password: ""
        }
    }

    handleSubmit(event) {
        console.log(this.state.login);
        event.preventDefault();
    }

    handleChange(event) {
        switch (event.target.id) {
            case "txtLogin":
                this.setState({ login: { ...this.state.login, login: event.target.value } })
                break;
            case "txtPassword":
                this.setState({ login: { ...this.state.login, password: event.target.value } })
                break;
        }
    }

    render() {
        return (
            <div role="main">
                <div className="container">

                    <div id="login" >
                        <h1>Login</h1>
                        <form onSubmit={this.handleSubmit.bind(this)}>
                            <div>
                                <label>CPF ou E-mail:</label>
                                <input id="txtLogin" type="text" className="form-control" onChange={this.handleChange.bind(this)} ></input>
                            </div>
                            <div>
                                <label>Senha:</label>
                                <input id="txtPassword" type="password" className="form-control" onChange={this.handleChange.bind(this)} ></input>
                            </div>
                            <input id="btnSubmit" type="submit" value="Logar" className="btn btn-primary"></input>
                        </form>
                </div>

            </div>
        </div>
        )
    }
}
