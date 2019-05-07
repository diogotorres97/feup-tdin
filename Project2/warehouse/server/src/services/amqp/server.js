const amqpAPI = require('./api');
const { sleep } = require('../../utils/utils');
const { requestsController } = require('../../controllers');

const {
  AMQP_URL, AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK,
} = require('../../config/configs');

function requestStock(msg) {
  const message = amqpAPI.parseMessage(msg);
  requestsController.create(message.title, message.quantity);
}

async function start() {
  amqpAPI.connect(AMQP_URL, [AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK]);
  await sleep(1000);
  amqpAPI.consumeMessage(AMQP_QUEUE_REQUEST_STOCK, requestStock);
}

module.exports = {
  start,
};
