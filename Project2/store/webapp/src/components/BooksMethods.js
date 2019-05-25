import axios from 'axios';
import AuthHelperMethods from './AuthHelperMethods';

const configs = require('../utils/Utils').configs;


export default class BooksMethods {
    Auth = new AuthHelperMethods();

    constructor(domain) {
        //THIS LINE IS ONLY USED WHEN YOU'RE IN PRODUCTION MODE!
        this.domain = domain || "http://localhost:3000"; // API server domain
    }


    getBooks = () => {
        let apiBaseUrl = configs.SERVER_HOST;

        let header = {
            headers: {
                Authorization: 'Bearer ' + this.Auth.getToken() //the token is a variable which holds the token
            }
        };

        return axios.get(apiBaseUrl + '/books', header)
            .then(res => {
                return Promise.resolve(res);
            });
    }
}