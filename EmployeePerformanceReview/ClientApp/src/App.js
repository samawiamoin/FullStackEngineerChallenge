import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Login } from './components/Login';
import AdminDashboard from './components/AdminDashboard';
import EmployeeDashboard from './components/EmployeeDashboard';
import { AddEmployee } from './components/AddEmployee';
import { AddPerformanceReview } from './components/AddPerformanceReview';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Login} />
        <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
            <Route path="/login" exact component={Login} />
            <Route path="/admin-dashboard" exact component={AdminDashboard} />
            <Route path="/employee-dashboard/:id" exact component={EmployeeDashboard} />
            <Route path="/add-employee" exact component={AddEmployee} />
            <Route path="/add-performancereview" exact component={AddPerformanceReview} />
      </Layout>
    );
  }
}
