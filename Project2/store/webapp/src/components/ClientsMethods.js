import axios from 'axios';
import AuthHelperMethods from './AuthHelperMethods';
const configs = require('../utils/Utils').configs;


export default class ClientsMethods {
    Auth = new AuthHelperMethods();

    constructor(domain) {
        //THIS LINE IS ONLY USED WHEN YOU'RE IN PRODUCTION MODE!
        this.domain = domain || "http://localhost:3000"; // API server domain
    }

    getClient = () => {
        let apiBaseUrl = configs.SERVER_HOST;

        let header = {
                headers: {
                    Authorization: 'Bearer ' + this.Auth.getToken() //the token is a variable which holds the token
                }
            };

        // Get a token from api server using the fetch api
        return axios.get(apiBaseUrl + '/clients/association', header)
            .then(res => {
                return Promise.resolve(res);
            });
    };

    createClient = (name, address) => {
         let apiBaseUrl = configs.SERVER_HOST;

        let header = {
                headers: {
                    Authorization: 'Bearer ' + this.Auth.getToken() //the token is a variable which holds the token
                }
            };

        let payload = {
            "name": name,
            "address": address,
            "email": this.Auth.getConfirm().user.email
        }


        // Get a token from api server using the fetch api
        return axios.post(apiBaseUrl + '/clients/association', payload, header)
            .then(res => {
                return Promise.resolve(res);
            });
    };

   
}