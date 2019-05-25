import axios from 'axios';
import AuthHelperMethods from './AuthHelperMethods';
const configs = require('../utils/Utils').configs;


export default class OrderMethods {
    Auth = new AuthHelperMethods();
    constructor(domain) {
        //THIS LINE IS ONLY USED WHEN YOU'RE IN PRODUCTION MODE!
        this.domain = domain || "http://localhost:3000"; // API server domain
    }

  

    createOrder = (clientId, quantity, bookId) => {
        let apiBaseUrl = configs.SERVER_HOST;

        let header = {
            headers: {
                Authorization: 'Bearer ' + this.Auth.getToken() //the token is a variable which holds the token
            }
        };

        let payload = {
            "quantity": quantity,
            "bookId": bookId,
            "clientId": clientId
        }


        return axios.post(apiBaseUrl + '/orders', payload, header)
            .then(res => {
                return Promise.resolve(res);
            });
    }
}