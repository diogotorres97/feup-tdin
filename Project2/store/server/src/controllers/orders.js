const { Order, Book, Client } = require('../models');
const { amqpAPI } = require('../amqp');
const {
  AMQP_QUEUE_REQUEST_STOCK,
} = require('./../config/configs');
const { orderState, messageType } = require('../enums');
const { emailServer } = require('../email');

const create = async (quantity, bookId, clientId) => {
  const info = await emailServer.sendEmail(
    'from@domain.com',
    'diogo.rey97@gmail.com',
    'Test',
    'order',
    {
      name: 'Name',
      email: 'tariqul.islam.rony@gmail.com',
      address: '52, Kadamtola Shubag dhaka',
    },
  );
  if (info.rejected.length > 0) throw new Error('Email Not Sent');

  return {};
  const book = Book.findByPk(bookId);
  if (!book) {
    throw new Error('Book not found');
  }

  const client = Client.findByPk(clientId);
  if (!client) {
    throw new Error('Client not found');
  }

  const orderData = { quantity, bookId, clientId };
  let order = null;

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
    order = Order.create({
      ...orderData,
      state: orderState.waiting,
    });
  } else {
    const nextDay = new Date().setDate(Date.now().getDate() + 1); // TODO: test this
    order = Order.create({
      ...orderData,
      state: orderState.delivered,
      stateDate: nextDay,
    });

    book.update({
      stock: book.stock - quantity,
    });
  }

  // Send email
  // const info = await emailAPI.sendEmail(
  //   'from@domain.com',
  //   'to@domain.com',
  //   'Test',
  //   'order',
  //    {
  //     name: 'Name',
  //     email: 'tariqul.islam.rony@gmail.com',
  //     address: '52, Kadamtola Shubag dhaka',
  //   });

  // if (info.rejected.length > 0) throw new Error('Email Not Sent');

  return order;
};

const list = async () => Order.findAll();

const retrieve = async orderId => Order.findByPk(orderId);

const update = async (orderId, state, stateDate) => {
  const order = await Order.findByPk(orderId);

  if (!order) {
    throw new Error('Order not found');
  }

  return order.update({
    state: state || order.state,
    stateDate: stateDate || order.stateDate,
  });
};

module.exports = {
  create,
  list,
  retrieve,
  update,
};
