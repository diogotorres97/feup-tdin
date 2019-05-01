const port = process.env.PORT || 3000;

const express = require('express');
const logger = require('morgan');
const bodyParser = require('body-parser');
const passport = require('passport');
const {
  FORCE_UPDATE_DB, AMQP_URL, AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK,
} = require('./config/configs');
const amqpAPI = require('./amqp/amqpAPI');

// Set up the express app
const app = express();
const routes = require('./routes/index');
const { sleep } = require('./utils/utils');
// Log requests to the console.
app.use(logger('dev'));

// Parse incoming requests data (https://github.com/expressjs/body-parser)
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

// Authentication
require('./auth/passport');

app.use(passport.initialize());

// Require our routes into the application.
app.use('/', routes);

// AMQP Connection
amqpAPI.connect(AMQP_URL, [AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK]);

const db = require('./models/index');

db.sequelize.sync({ force: FORCE_UPDATE_DB }).then(() => {
  app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}/`);
  });
});

module.exports = app;

(async () => {
  await sleep(2000);
  amqpAPI.publishMessage(AMQP_QUEUE_REQUEST_STOCK, { cenas: 'hello world' });
  amqpAPI.consumeMessage(AMQP_QUEUE_REQUEST_STOCK, amqpAPI.parseMessage);
})();
