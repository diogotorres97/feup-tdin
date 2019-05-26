const booksController = require('./books');
const clientsController = require('./clients');
const receiveStockController = require('./receiveStock');
const ordersController = require('./orders');
const sellsController = require('./sells');
const statisticsController = require('./statistics');

module.exports = {
  booksController,
  clientsController,
  receiveStockController,
  ordersController,
  sellsController,
  statisticsController,
};
