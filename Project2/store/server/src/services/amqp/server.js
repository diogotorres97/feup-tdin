const amqpAPI = require('./api');
const { sleep } = require('../../utils/utils');
const { receiveStockController } = require('../../controllers');
const { messageType } = require('../../enums');
const { sendNotificationMessage } = require('../websockets/pusher');
const { PUSHER_CHANNEL_STORE } = require('../../config/configs');

const {
  AMQP_URL, AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK,
} = require('../../config/configs');

async function start() {
  amqpAPI.connect(AMQP_URL, [AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK]);
  await sleep(1000);
  amqpAPI.consumeMessage(AMQP_QUEUE_RECEIVE_STOCK, receiveStock);
}

function receiveStock(msg) {
  const message = amqpAPI.parseMessage(msg);
  receiveStockController.create(message.title, message.quantity);
  sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.receiveStock, message);
}

module.exports = {
  start,
};
