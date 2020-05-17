import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import Home from './pages/home/home';
import Product from './pages/product/product';
import Cart from './pages/cart/cart';

const Routes = () => (
    <BrowserRouter>
        <Switch>
            <Route exact path="/" component={Home} />
            <Route exact path="/produto" component={Product}></Route>
            <Route exact path="/carrinho" component={Cart}></Route>
        </Switch>
    </BrowserRouter>
);

export default Routes;