const {
  Sell, Book, Client, Order,
} = require('../models');

const create = async (quantity, bookId, clientId) => {
  const book = Book.findByPk(bookId);
  if (!book) {
    throw new Error('Book not found');
  }

  const client = Client.findByPk(clientId);
  if (!client) {
    throw new Error('Client not found');
  }

  if (book.stock < quantity) {
    // TODO: Allow sell the stock and make an order of the rest?
    const state = 'WAITING'; // TODO: Refactor ENUMS

    // Make a request to warehouse
    // Make an order
    return Order.create({
      quantity,
      state,
      bookId,
      clientId,
    });

    // Send email
  }

  const sell = Sell.create({
    quantity,
    bookId,
    clientId,
  });

  book.update({
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
