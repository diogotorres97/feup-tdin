const { Order } = require('../models');

const create = async (bookId, quantity, state) => Order.create({
  quantity,
  state,
  bookId,
});

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
