const amqpAPI = require('./api');
const { sleep } = require('../../utils/utils');
const {
  AMQP_URL, AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK,
} = require('../../config/configs');

const receiveStock = (msg) => {
  const { payload } = amqpAPI.parseMessage(msg);
  const receiveStockController = require('../../controllers/receiveStock');
  receiveStockController.create(payload.title, payload.quantity);
};

async function start() {
  amqpAPI.connect(AMQP_URL, [AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK]);
  await sleep(1000);
  amqpAPI.consumeMessage(AMQP_QUEUE_RECEIVE_STOCK, receiveStock);
}

module.exports = {
  start,
};
