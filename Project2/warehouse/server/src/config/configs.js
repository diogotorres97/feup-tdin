module.exports = {
  JWT_SECRET: 'top_secret',
  FORCE_UPDATE_DB: true,
  AMQP_URL: 'amqp://rabbitmq',
  AMQP_QUEUE_REQUEST_STOCK: 'request_stock',
  AMQP_QUEUE_RECEIVE_STOCK: 'receive_stock',
  PUSHER_CHANNEL_WAREHOUSE: 'warehouse',
};
