import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import AppBar from 'material-ui/AppBar';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';
import React, { Component } from 'react';
import { Col, Row, Form, FormGroup, Label, Input } from 'reactstrap';
import './Order.scss';
import { Link, NavLink, Redirect } from 'react-router-dom';
import OrdersMethods from '../OrdersMethods';

import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';

class Order extends Component {
    state = {
        order: Object,
        loaded: false,
    }

    componentDidMount(){
        this.setState({
            order: this.props.order,
            loaded: true
        })
    }
    

    render() {
        if (this.state.loaded === true) {
            return(
                <React.Fragment>
                     <Card className="card">
                        <CardContent>
                            <Typography className="title" color="textSecondary" gutterBottom>
                                UUID: {this.state.order.uuid}
                            </Typography>
                            <Typography variant="h6" component="h2">
                                Quantity: {this.state.order.quantity}
                            </Typography>
                            <Typography variant="h6" component="h2">
                                Total Price: {this.state.order.totalPrice} €
                            </Typography>
                            <Typography variant="h6" component="h2">
                                State: {this.state.order.state}
                            </Typography>
                            
                            <Typography variant="h6" component="h2">
                                State Date: {this.state.order.stateDate ? this.state.order.stateDate : "No Date"}
                            </Typography>
                            
                            <Typography variant="h6" component="h2">
                                Book title: {this.state.order.Book.title}
                            </Typography>
                        </CardContent>
                       
                    </Card>
                </React.Fragment>
            );
        }else{
            return null;
        }
    }
}

export default Order;