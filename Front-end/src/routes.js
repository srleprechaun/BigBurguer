import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import Home from './pages/home/home';
import Product from './pages/product/product';
import Cart from './pages/cart/cart';
import Login from './pages/login/login';
import Order from './pages/order/order';
import User from './pages/user/user';

const Routes = () => (
    <BrowserRouter>
        <Switch>
            <Route exact path="/" component={Home} />
            <Route exact path="/produto" component={Product}></Route>
            <Route exact path="/carrinho" component={Cart}></Route>
            <Route exact path="/login" component={Login}></Route>
            <Route exact path="/compras" component={Order}></Route>
            <Route exact path="/conta" component={User}></Route>
        </Switch>
    </BrowserRouter>
);

export default Routes;