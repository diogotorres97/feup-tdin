import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import './App.scss';

/* Once the 'Authservice' and 'withAuth' componenets are created, import them into App.js */
import AuthHelperMethods from '../AuthHelperMethods';

//Our higher order component
import withAuth from '../withAuth';

class App extends Component {
  Auth = new AuthHelperMethods();
  
  state = {
    username: '',
    toLogin: false
  }
  
  _handleLogout = () => {
    this.Auth.logout()
    this.setState({
      toLogin: true
    });
  }


  render() {
    if (this.state.toLogin === true) {
      return <Redirect to='/login' />
    }

    let name = null;
    if (this.props.confirm) {
      name = this.props.confirm.user.email;
    }

    console.log("Rendering Appjs!")

    return (
      <div className="App">
        <div className="main-page">
          <div className="top-section">
            <h1>Welcome, {name}</h1>
          </div>
          <div className="bottom-section">
            <button onClick={this._handleLogout}>LOGOUT</button>
          </div>
        </div>
      </div>
    );
  }
}

export default withAuth(App);