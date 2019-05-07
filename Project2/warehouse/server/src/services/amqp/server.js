const amqpAPI = require('./api');
const { sleep } = require('../../utils/utils');
const {
  AMQP_URL, AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK,
} = require('../../config/configs');


const requestStock = (msg) => {
  const {payload} = amqpAPI.parseMessage(msg);
  const requestsController = require('../../controllers/requests');
  requestsController.create(payload.title, payload.quantity);
}

async function start() {
  amqpAPI.connect(AMQP_URL, [AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK]);
  await sleep(1000);
  amqpAPI.consumeMessage(AMQP_QUEUE_REQUEST_STOCK, requestStock);
}

module.exports = {
  start,
};
