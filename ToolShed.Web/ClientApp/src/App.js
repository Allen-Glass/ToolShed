import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Login from './Pages/Login';
import FetchData from './components/FetchData';
import RentalStart from './Pages/RentalStart';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/login' component={Login} />
    <Route path='/rental/start' component={RentalStart} />
    <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
  </Layout>
);
