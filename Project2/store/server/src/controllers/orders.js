const { Order, Book, Client } = require('../models');

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
    const state = 'WAITING'; // TODO: Refactor ENUMS

    // Make a request to warehouse
    // quantity + 10
    // Make an order
    return Order.create({
      quantity,
      state,
      bookId,
      clientId,
    });

    // Send email
  }

  const state = 'DELIVERED';
  const nextDay = new Date().setDate(Date.now().getDate() + 1); // TODO: test this
  const order = Order.create({
    quantity,
    state,
    stateDate: nextDay,
    bookId,
    clientId,
  });

  book.update({
    stock: book.stock - quantity,
  });

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
