import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import Gallery from './components/Gallery';
import SignInSide from './components/Auth/SignIn'
import SignUp from './components/Auth/SignUp'
import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/gallery' component={Gallery} />
                <Route path='/login' component={SignInSide} />
                <Route path='/signup' component={SignUp} />
            </Layout>
        );
    }
}
