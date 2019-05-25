import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import AppBar from 'material-ui/AppBar';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';
import React, {Component} from 'react';
import './CreateOrderPage.scss';
import {Redirect} from 'react-router-dom';

import Grid from '@material-ui/core/Grid';

import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import AddCircleIcon from '@material-ui/icons/AddCircle';

import ClientsMethods from '../ClientsMethods';
import AuthHelperMethods from '../AuthHelperMethods';
import OrdersMethods from '../OrdersMethods';
import BooksMethods from '../BooksMethods';
import withAuth from '../withAuth';

function importAll(r) {
    let images = {};
    r.keys().forEach((item, index) => {
        images[index] = r(item);
    });
    return images;
}

const images = importAll(require.context('../../images/', false, /\.(PNG|png|jpe?g|svg)$/));


class CreateOrderPage extends Component {
    Order = new OrdersMethods();
    Client = new ClientsMethods();
    Auth = new AuthHelperMethods();
    Book = new BooksMethods();


    constructor(props) {
        super(props);
        this.state = {
            order: Object,
            loaded: false,
            toLogin: false,
            toHome: false,
            client: null,
            clientName: "",
            clientAddress: "",
            quantity: 1,
            selectedBook: Object,
            books: []
        };
        this._selectBook = this._selectBook.bind(this);
        this._createOrder = this._createOrder.bind(this)
    }

    _handleLogout = () => {
        this.Auth.logout();
        this.setState({
            toLogin: true
        });
    };

    _handleHome = () => {
        this.setState({
            toHome: true
        });
    };

    _handleChange = (e) => {
        this.setState(
            {
                [e.target.name]: e.target.value
            }
        )
    };

    _selectBook = (book) => {
        this.setState({
            selectedBook: book
        })
    };

    _createOrder() {
        if (!this.state.client) {
            if (this.state.clientName && this.state.clientAddress) {
                this.Client.createClient(this.state.clientName, this.state.clientAddress)
                    .then(res => {

                        if (res.data !== "") {

                            this.setState({
                                client: res.data
                            })
                        }

                    })
                    .catch(err => {

                        console.log(err);
                    });
            }
        }
        if (this.state.selectedBook.id && this.state.client) {
            this.Order.createOrder(this.state.client.id, this.state.quantity, this.state.selectedBook.id)
                .then(res => {

                    this.setState({
                        toHome: true
                    });

                })
                .catch(err => {

                    console.log(err);
                });
        }
    }

    componentWillMount() {
        this.Client.getClient()
            .then(res => {

                if (res.data !== "") {

                    this.setState({
                        client: res.data
                    })
                }

            })
            .catch(err => {

                console.log(err);
            });

        this.Book.getBooks()
            .then(res => {

                if (res.data !== "") {
                    let newBooks = [];
                    let index;
                    for (index = 0; index < res.data.length; ++index) {
                        let book = res.data[index];
                        book.img = images[book.id - 1];
                        newBooks.push(book);
                    }


                    this.setState({
                        books: newBooks
                    })

                }

            })
            .catch(err => {

                console.log(err);
            })
    }

    componentDidMount() {
        this.setState({
            order: this.props.order,
            loaded: true
        })
    }


    render() {
        if (this.state.toLogin === true) {
            return <Redirect to='/login'/>
        }

        if (this.state.toHome === true) {
            return <Redirect to='/' refresh={true}/>
        }

        let name = null;
        if (this.props.confirm) {
            name = "Create order for: " + this.props.confirm.user.email;
        }

        if (this.state.loaded === true) {
            return (
                <div>

                    <React.Fragment>

                        <MuiThemeProvider>
                            <div className="page">
                                <AppBar className="appBar"
                                        title={name}
                                        showMenuIconButton={false}>
                                    <RaisedButton className="btnLogout" onClick={this._handleHome}>Home</RaisedButton>
                                    <RaisedButton className="btnLogout"
                                                  onClick={this._handleLogout}>Logout</RaisedButton>
                                </AppBar>


                                <GridList cellHeight={180} className="gridList">
                                    <GridListTile key="Subheader" cols={2} style={{height: 'auto'}}>
                                        <ListSubheader component="div">Book List</ListSubheader>
                                    </GridListTile>
                                    {this.state.books.map(book => (
                                        <GridListTile key={book.id}>
                                            <img src={book.img} alt={book.title}/>
                                            <GridListTileBar
                                                title={book.title}
                                                subtitle={<span>by: {book.author}</span>}
                                                actionIcon={
                                                    <IconButton onClick={() => this._selectBook(book)}>
                                                        <span
                                                            style={{color: "white"}}>{book.price}€</span><AddCircleIcon
                                                        className="actionIcon"/>
                                                    </IconButton>
                                                }
                                            />
                                        </GridListTile>
                                    ))}

                                </GridList>
                                <Grid
                                    container
                                    spacing={0}
                                    direction="row"
                                    alignItems="center"
                                    justify="center"
                                >
                                    <Grid item xs={2}>
                                        <p>Selected Book:</p>
                                    </Grid>
                                    <Grid item xs={2}>
                                        <p>{this.state.selectedBook.title ? this.state.selectedBook.title : "No Book Selected"}</p>
                                    </Grid>


                                </Grid>

                                <Grid
                                    container
                                    spacing={0}
                                    direction="row"
                                    alignItems="center"
                                    justify="center"
                                >
                                    <Grid item xs={2}>
                                        <p>Quantity:</p>
                                    </Grid>
                                    <Grid item xs={2}>
                                        <TextField
                                            id="outlined-number"
                                            label="Number"
                                            value={this.state.quantity}
                                            name="quantity"
                                            onChange={this._handleChange}
                                            type="number"
                                            className="selectQuantity"
                                            min="1" max="100"
                                            margin="normal"
                                            variant="outlined"

                                        />
                                    </Grid>


                                </Grid>

                                <Grid
                                    container
                                    spacing={0}
                                    direction="row"
                                    alignItems="center"
                                    justify="center"
                                >
                                    <Grid item xs={2}>
                                        <p>Total:</p>
                                    </Grid>
                                    <Grid item xs={2}>
                                        <TextField
                                            id="outlined-number"
                                            label="Total"
                                            value={this.state.quantity * (this.state.selectedBook.price ? this.state.selectedBook.price : 0)}
                                            name="total"
                                            margin="normal"
                                            variant="outlined"
                                            disabled

                                        />
                                    </Grid>


                                </Grid>
                                {this.state.client ? null :
                                    <div className="clientSection">
                                        <Grid
                                            container
                                            spacing={0}
                                            direction="row"
                                            alignItems="center"
                                            justify="center"
                                        >
                                            <Grid item xs={2}>
                                                <p>Client Name:</p>
                                            </Grid>
                                            <Grid item xs={2}>
                                                <TextField
                                                    id="outlined-clientName"
                                                    label="Name"
                                                    value={this.state.clientName}
                                                    name="clientName"
                                                    onChange={this._handleChange}
                                                    type="text"
                                                    className="textName"
                                                    margin="normal"
                                                    variant="outlined"
                                                />
                                            </Grid>
                                        </Grid>
                                        <Grid
                                            container
                                            spacing={0}
                                            direction="row"
                                            alignItems="center"
                                            justify="center"
                                        >
                                            <Grid item xs={2}>
                                                <p>Client Address:</p>
                                            </Grid>
                                            <Grid item xs={2}>
                                                <TextField
                                                    id="outlined-clientAddress"
                                                    label="Address"
                                                    value={this.state.clientAddress}
                                                    name="clientAddress"
                                                    onChange={this._handleChange}
                                                    type="text"
                                                    className="textAddress"
                                                    margin="normal"
                                                    variant="outlined"
                                                />
                                            </Grid>
                                        </Grid>
                                    </div>}
                                <RaisedButton className="btnCreate" onClick={this._createOrder}
                                              color="red">Create</RaisedButton>

                            </div>
                        </MuiThemeProvider>
                    </React.Fragment>
                </div>

            );
        } else {
            return null;
        }
    }
}

export default withAuth(CreateOrderPage);