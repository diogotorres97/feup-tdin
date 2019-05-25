import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import AppBar from 'material-ui/AppBar';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';
import React, { Component } from 'react';
import { Form } from 'reactstrap';
import './Login.scss';
import { Link, Redirect } from 'react-router-dom';
import AuthHelperMethods from '../AuthHelperMethods';


class Login extends Component {
   
    Auth = new AuthHelperMethods();

    state = {
        email: "",
        password: "",
        toHome: false,
        showError: false
        }

    /* Fired off every time the use enters something into the input fields */
    _handleChange = (e) => {
        this.setState(
            {
                [e.target.name]: e.target.value
    }
        )
    }

    handleFormSubmit = (e) => {

        e.preventDefault();
        /* Here is where all the login logic will go. Upon clicking the login button, we would like to utilize a login method that will send our entered credentials over to the server for verification. Once verified, it should store your token and send you to the protected route. */
        this.Auth.login(this.state.email, this.state.password)
            .then(res => {
                if (res === false) {
                    return alert("Sorry those credentials don't exist!");
                }
                this.setState({
                    toHome: true,
                    showError: false
                });
                
            })
            .catch(err => {
                this.setState({
                    showError: true
                })
                console.log(err);
            })
    }

    componentWillMount() {
        /* Here is a great place to redirect someone who is already logged in to the protected route */
        if (this.Auth.loggedIn())
            this.setState({
                toHome: true
            });
    }

    render() {
        if (this.state.toHome === true) {
            return <Redirect to='/' />
        }

        let errorMessage = "";
        if(this.state.showError){
            errorMessage = "Invalid Email or Password!";
        }

        return (
            <React.Fragment>
            <div className="login">
                <MuiThemeProvider>
                    <div>
                        <AppBar
                            title="Login"
                                showMenuIconButton={false}
                        />
                            <Form className="box-form">
                                <TextField
                                    className="form-item"
                                    hintText = "Enter your Email"
                                    name="email"
                                    type="email"
                                    onChange={this._handleChange}
                                />
                                <br/>
                                <TextField
                                    className="form-item"
                                    hintText="Enter your password"
                                    name="password"
                                    type="password"
                                    onChange={this._handleChange}
                                />
                                <br />
                                <p className="errorMessage">{errorMessage}</p>
                                <RaisedButton className="form-submit" onClick={this.handleFormSubmit}>Login</RaisedButton>
                            </Form>
                            <p className="textSignup">Not registered yet? Register Now</p>
                            <Link className="link" to="/signup"><span className="link-signup">Signup</span></Link>
                    </div>
                </MuiThemeProvider>
            </div>
            </React.Fragment>
        );
    }

   
}
export default Login;