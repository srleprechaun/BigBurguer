import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import Home from './pages/home/home';
import cadastroProduto from './pages/cadastroProduto/cadastroProduto';

const Routes = () => (
    <BrowserRouter>
        <Switch>
            <Route exact path="/" component={Home} />
            <Route exact path="/cadastroProduto" component={cadastroProduto}></Route>
        </Switch>
    </BrowserRouter>
);

export default Routes;