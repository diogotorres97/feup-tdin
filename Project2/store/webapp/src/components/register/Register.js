import Login from '../login/Login';
import React, { Component } from 'react';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import AppBar from 'material-ui/AppBar';
import RaisedButton from 'material-ui/RaisedButton';
import './Register.scss';
import TextField from 'material-ui/TextField';
import { Col, Row, Form, FormGroup, Label, Input } from 'reactstrap';
import axios from 'axios';
import AuthHelperMethods from '../AuthHelperMethods';
import { Link, Redirect } from 'react-router-dom';

const configs = require('../../utils/Utils').configs;

class Register extends Component {
    Auth = new AuthHelperMethods();
    state = {
        email: "",
        password: "",
        toHome: false
    }

    _handleChange = (e) => {

        this.setState(
            {
                [e.target.name]: e.target.value
            }
        )
    }

    handleFormSubmit = (e) => {

        e.preventDefault();

        this.Auth.signup(this.state.email, this.state.password)
            .then(res => {
                if (res === false) {
                    return alert("Registration failed!");
                }
                this.setState({
                    toHome: true
                });

            })
            .catch(err => {
                console.log(err);
            })

       
    }

    componentDidMount() {
        console.log(this.Auth.loggedIn());
        if (this.Auth.loggedIn()) {
            this.setState({
                toHome: true
            });

        }
    }

    render() {
        if (this.state.toHome === true) {
            return <Redirect to='/' />
        }
        return (
            <React.Fragment>
                <div className="register">
                    <MuiThemeProvider>
                        <div>
                            <AppBar
                                title="Register"
                                showMenuIconButton={false}
                            />
                            <div className="content">
                                <Form className="box-form">
                                    <TextField
                                        className="form-item"
                                        hintText="Enter the Email"
                                        name="email"
                                        type="email"
                                        onChange={this._handleChange}
                                    />
                                    <br />
                                    <TextField
                                        className="form-item"
                                        hintText="Enter the password"
                                        name="password"
                                        type="password"
                                        onChange={this._handleChange}
                                    />
                                    <br />
                                    <RaisedButton className="form-submit" onClick={this.handleFormSubmit}>SignUp</RaisedButton>
                                </Form>
                                <p className="textLogin">Already registered. Go to Login</p>
                                <Link className="link" to="/login"><span className="link-login">Login</span></Link>
                            </div>
                        </div>
                    </MuiThemeProvider>
                </div>
            </React.Fragment>
        );
    }

    

}

export default Register;