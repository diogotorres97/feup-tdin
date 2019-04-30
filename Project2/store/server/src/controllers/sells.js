const { Sell } = require('../models');

const create = async (bookId, quantity, state) => Sell.create({
  quantity,
  state,
  bookId,
});

const list = async () => Sell.findAll();

const retrieve = async sellId => Sell.findByPk(sellId);

const update = async (sellId, state, stateDate) => {
  const sell = await Sell.findByPk(sellId);

  if (!sell) {
    throw new Error('Sell not found');
  }

  return sell.update({
    state: state || sell.state,
    stateDate: stateDate || sell.stateDate,
  });
};

module.exports = {
  create,
  list,
  retrieve,
  update,
};
