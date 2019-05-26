import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import AppBar from 'material-ui/AppBar';
import RaisedButton from 'material-ui/RaisedButton';
import React, {Component} from 'react';
import {Link, Redirect} from 'react-router-dom';
import './App.scss';
import Order from '../order/Order';
import Grid from '@material-ui/core/Grid';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import ListSubheader from '@material-ui/core/ListSubheader';
/* Once the 'Authservice' and 'withAuth' componenets are created, import them into App.js */
import AuthHelperMethods from '../AuthHelperMethods';
import OrdersMethods from '../OrdersMethods';
import ClientsMethods from '../ClientsMethods';
//Our higher order component
import withAuth from '../withAuth';

class App extends Component {
    Auth = new AuthHelperMethods();
    Orders = new OrdersMethods();
    Client = new ClientsMethods();

    state = {
        username: '',
        toLogin: false,
        confirm: null,
        orders: [],
        client: Object
    };

    _handleLogout = () => {
        this.Auth.logout();
        this.setState({
            toLogin: true
        });
    };

    componentWillReceiveProps(props) {
        if (props.refresh) {
            this.getClient();
            if (this.state.client)
                this.listOrders();

        }
    }

    listOrders() {

    }

    getClient() {
        this.Client.getClient()
            .then(res => {

                if (res.data !== "") {

                    this.setState({
                        client: res.data,
                        orders: res.data.Orders
                    })
                }

            })
            .catch(err => {

                console.log(err);
            })
    }

    componentWillMount() {
        let conf = this.Auth.getConfirm();
        this.setState({
            confirm: conf
        });
        this.getClient();
        if (this.state.client)
            this.listOrders();

    }


    render() {
        if (this.state.toLogin === true) {
            return <Redirect to='/login'/>
        }

        let name = null;
        if (this.state.confirm) {
            name = "Welcome, " + this.state.confirm.user.email;
        }


        return (
            <div className="App">
                <MuiThemeProvider>
                    <div className="main-page">
                        <div className="top-section">
                            <AppBar
                                title={name}
                                showMenuIconButton={false}>
                                <RaisedButton className="btnLogout" onClick={this._handleLogout}>Logout</RaisedButton>
                            </AppBar>
                        </div>
                        <Grid
                            className="listOrders"
                            container
                            spacing={0}
                            direction="column"
                            alignItems="center"
                            /*justify="center"*/
                            style={{minHeight: '100vh'}}
                        >
                            {this.state.orders.length > 0 ?
                                <GridList cellHeight={260} className="gridList">
                                    <GridListTile key="Subheader" cols={this.state.orders.length > 1 ? 2 : 1}
                                                  style={{height: 'auto'}}>
                                        <ListSubheader component="div">Orders List</ListSubheader>
                                    </GridListTile>
                                    {this.state.orders.map((or, key) =>
                                        <GridListTile cols={this.state.orders.length > 1 ? 1 : 2} key={key}>
                                            <Order order={or}/>
                                        </GridListTile>
                                    )}

                                </GridList>
                                : <p>You have no orders!</p>
                            }


                            <Link className="link" to="/createorder"><span
                                className="link-create-order">Create Order</span></Link>
                        </Grid>


                    </div>
                </MuiThemeProvider>
            </div>
        );
    }
}

export default withAuth(App);