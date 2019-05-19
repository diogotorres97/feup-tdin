import React, { Component } from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import RaisedButton from 'material-ui/RaisedButton';
import Login from './Login';
import './Login.scss';
import Register from '../register/Register';
class Loginscreen extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            loginscreen: [],
            loginmessage: '',
            buttonLabel: 'Register',
            isLogin: true
        }
    }
    componentWillMount() {
        var loginscreen = [];
        loginscreen.push(<Login key={1} parentContext={this} appContext={this.props.parentContext} />);
        var loginmessage = "Not registered yet, Register Now";
        this.setState({
            loginscreen: loginscreen,
            loginmessage: loginmessage
        })
    }
    render() {
        return (
            <div className="loginscreen">
                {this.state.loginscreen}
                <div>
                    {this.state.loginmessage}
                    <MuiThemeProvider>
                        <div>
                            <RaisedButton label={this.state.buttonLabel} primary={true} onClick={(event) => this.handleClick(event)} />
                        </div>
                    </MuiThemeProvider>
                </div>
            </div>
        );
    }

    handleClick(event) {
        // console.log("event",event);
        var loginmessage;
        if (this.state.isLogin) {
            var loginscreen = [];
            loginscreen.push(<Register key={1} parentContext={this} />);
            loginmessage = "Already registered.Go to Login";
            this.setState({
                loginscreen: loginscreen,
                loginmessage: loginmessage,
                buttonLabel: "Login",
                isLogin: false
            })
        }
        else {
            var loginscreen = [];
            loginscreen.push(<Login key={1} parentContext={this} />);
            loginmessage = "Not Registered yet.Go to registration";
            this.setState({
                loginscreen: loginscreen,
                loginmessage: loginmessage,
                buttonLabel: "Register",
                isLogin: true
            })
        }
    }    
}

export default Loginscreen;