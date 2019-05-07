const { ReceiveStock, Book, Order } = require('../models');
const {
  PUSHER_CHANNEL_STORE,
} = require('../config/configs');
const { messageType, orderState } = require('../enums');

const create = async (bookTitle, quantity) => {
  const book = await Book.findOne({
    where: {
      title: bookTitle,
    },
  });

  if (!book) throw new Error('Book not found');

  // Update orders to dispatch should occur at (today plus 2 days)
  const orders = Order.findAll({
    where: {
      bookId: book.id,
      state: 'WAITING',
    },
  });

  // for await(let order of orders) {
  //   await order.update({
  //     state: orderState.dispatch,
  //     stateDate: Date.now()
  //   });
  // }

  return ReceiveStock.create(quantity, { bookId: book.id });

  // Put here the pusher msg
};

const list = async () => ReceiveStock.findAll();

const receiveStock = async (receiveStockId) => {
  const receiveStock = await ReceiveStock.findByPk(receiveStockId);

  if (!receiveStock) {
    throw new Error('ReceiveStock not found');
  }

  if (receiveStock.processedDate) {
    throw new Error('ReceiveStock already processed');
  }

  const book = await receiveStock.getBook();
  console.log(book);

  // update the stock
  await book.update({
    stock: book.stock + receiveStock.quantity,
  });

  // Update orders to dispatched at (today)

  // Send an email to notify the clients

  // update the receiveStock's processedDate
  return receiveStock.update({
    processedDate: new Date(),
  });
};

module.exports = {
  create,
  list,
  receiveStock,
};
