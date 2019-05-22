const {
  ReceiveStock, Book, Order, Client,
} = require('../models');
const { orderState } = require('../enums');
const { emailServer } = require('../services/email');

const create = async (bookTitle, quantity) => {
  const book = await Book.findOne({
    where: {
      title: bookTitle,
    },
  });

  if (!book) throw new Error('Book not found');

  // Update orders to dispatch should occur at (today plus 2 days)
  const twoDaysAfter = new Date();
  twoDaysAfter.setDate(new Date().getDate() + 2);

  const orders = await Order.findAll({
    where: {
      bookId: book.id,
      state: 'WAITING',
    },
  });

  let stockLeft = quantity;
  // eslint-disable-next-line
  let ordersId = [];
  for await (const order of orders) {
    if (stockLeft > order.quantity) {
      await order.update({
        state: orderState.dispatch,
        stateDate: twoDaysAfter,
      });

      stockLeft -= order.quantity;
      ordersId.push(order.uuid);
    }
  }

  const receiveStock = await ReceiveStock.create({ quantity, ordersId, ...{ bookId: book.id } });
  receiveStock.setDataValue('book', book);
  return receiveStock;
};

const list = async () => ReceiveStock.findAll({
  include: [
    { model: Book },
  ],
});

const receiveStock = async (receiveStockId) => {
  const receivedStock = await ReceiveStock.findByPk(receiveStockId,{
    include: [
      { model: Book },
    ],
  });

  if (!receivedStock) {
    throw new Error('ReceiveStock not found');
  }

  if (receivedStock.processedDate) {
    throw new Error('ReceiveStock already processed');
  }

  const book = await receivedStock.getBook();

  let stockLeft = receivedStock.quantity;
  for await (const orderId of receivedStock.ordersId) {
    const order = await Order.findByPk(orderId);
    // Update orders to dispatched at (today)
    await order.update({
      state: orderState.delivered,
      stateDate: Date.now(),
    });

    // Update the stock left from that receivedStock
    stockLeft -= order.quantity;

    const client = await Client.findByPk(order.clientId);
    if (!client) {
      throw new Error('Client not found');
    }

    // Send an email to notify the clients
    const info = await emailServer.sendEmail(
      null,
      client.email,
      `Order #${order.uuid} Updated`,
      'order',
      {
        book,
        client,
        order,
        orderState: orderState.toString(order.state, order.stateDate),
      },
    );

    if (info.rejected.length > 0) throw new Error('Email Not Sent');
  }

  // update the stock
  await book.update({
    stock: book.stock + stockLeft,
  });

  // update the receiveStock's processedDate
  return receivedStock.update({
    processedDate: new Date(),
  });
};

module.exports = {
  create,
  list,
  receiveStock,
};
