const amqpAPI = require('./amqpAPI');
const { sleep } = require('../../utils/utils');
const { notificationsController } = require('../../controllers');
const { notificationType } = require('../../enums');

const {
  AMQP_URL, AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK,
} = require('../../config/configs');

async function start() {
  amqpAPI.connect(AMQP_URL, [AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK]);
  await sleep(1000);
  amqpAPI.consumeMessage(AMQP_QUEUE_REQUEST_STOCK, requestStock);
}

function requestStock(msg) {
  const message = amqpAPI.parseMessage(msg);
  notificationsController.create(notificationType.requestStock, message);
  
}

module.exports = {
  start,
};
