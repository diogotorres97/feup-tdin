const { Request, Book } = require('../models');
const {
  AMQP_QUEUE_RECEIVE_STOCK,
} = require('../config/configs');
const { messageType } = require('../enums');
const { amqpAPI } = require('../services/amqp');

const create = async (bookTitle, quantity) => {
  const book = await Book.findOne({
    where: {
      title: bookTitle,
    },
  });

  if (!book) throw new Error('Book not found');

  const request = await Request.create({ quantity, ...{ bookId: book.id } });

  return {...request.dataValues, book};
};

const list = async () => Request.findAll({
  include: [
    { model: Book },
  ],
});

const sendStock = async (requestId) => {
  const request = await Request.findByPk(requestId, {
    include: [
      { model: Book },
    ],
  });

  if (!request) {
    throw new Error('Request not found');
  }

  if (request.processedDate) {
    throw new Error('Request already processed');
  }

  const book = await request.getBook();

  // Send the stock to the store
  amqpAPI.publishMessage(AMQP_QUEUE_RECEIVE_STOCK,
    amqpAPI.createMessage(
      messageType.sendStock,
      {
        title: book.title,
        quantity: request.quantity,
      },
    ));

  // update the stock
  await book.update({
    stock: book.stock - request.quantity,
  });

  // update the request's processedDate
  return request.update({
    processedDate: new Date(),
  });
};

module.exports = {
  create,
  list,
  sendStock,
};
