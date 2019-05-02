const amqpAPI = require('./api');
const { sleep } = require('../utils/utils');

const {
  AMQP_URL, AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK,
} = require('../config/configs');

async function start() {
  amqpAPI.connect(AMQP_URL, [AMQP_QUEUE_REQUEST_STOCK, AMQP_QUEUE_RECEIVE_STOCK]);
  await sleep(1000);
  amqpAPI.consumeMessage(AMQP_QUEUE_RECEIVE_STOCK, amqpAPI.parseMessage);
}

module.exports = {
  start,
};
