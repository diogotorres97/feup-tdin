const {
  Sell, Book, Client, Order,
} = require('../models');
const amqpAPI = require('../amqp/amqpAPI');
const {
  AMQP_QUEUE_REQUEST_STOCK,
} = require('./../config/configs');

const create = async (quantity, bookId, clientId) => {
  const book = await Book.findByPk(bookId);
  if (!book) {
    throw new Error('Book not found');
  }
  const client = await Client.findByPk(clientId);
  if (!client) {
    throw new Error('Client not found');
  }

  const sellData = { quantity, bookId, clientId };

  if (book.stock < quantity) {
    // Make a request to warehouse
    amqpAPI.publishMessage(AMQP_QUEUE_REQUEST_STOCK,
      amqpAPI.createMessage(
        messageType.requestStock,
        {
          title: book.title,
          quantity: quantity + 10,
        },
      ));

    // Make an order
    const order = await Order.create({
      ...sellData,
      state: orderState.waiting,
    });

    // Send an e-mail

    // const info = await emailServer.sendEmail(
    //   null,
    //   client.email,
    //   `Order #${order.uuid} confirmed`,
    //   'order',
    //   {
    //     book,
    //     client,
    //     order,
    //     orderState: orderState.toString(order.state, order.stateDate),
    //   },
    // );

    // if (info.rejected.length > 0) throw new Error('Email Not Sent');

    return order;
  }

  const sell = await Sell.create(orderData);

  await book.update({
    stock: book.stock - quantity,
  });

  // print a receipt

  return sell;
};

const list = async () => Sell.findAll();

const retrieve = async sellId => Sell.findByPk(sellId);

module.exports = {
  create,
  list,
  retrieve,
};
