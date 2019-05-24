import React, { Component } from 'react';
import logo from '../../logo.svg';
import './App.scss';

import injectTapEventPlugin from 'react-tap-event-plugin';
// Needed for onTouchTap
// http://stackoverflow.com/a/34015469/988941
import Loginscreen from '../login/Loginscreen'
//injectTapEventPlugin();

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loginPage: [],
      uploadScreen: []
    }
  }
  componentWillMount() {
    var loginPage = [];
    loginPage.push(<Loginscreen key={1} parentContext={this} />);
    this.setState({
      loginPage: loginPage
    })
  }
  render() {
    return (
      <div className="App">
        {this.state.loginPage}
        {this.state.uploadScreen}
      </div>
    );
  }
}

export default App;