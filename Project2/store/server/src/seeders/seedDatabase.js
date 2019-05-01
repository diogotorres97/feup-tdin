const { User } = require('../models');

const {
  clientsController,
  booksController,
  ordersController,
  sellsController,
  notificationsController,
} = require('../controllers');

const initializeUsers = async () => {
  await Promise.all([
    User.create({ email: 'john@store.com', password: 'john' }),
    User.create({ email: 'jane@store.com', password: 'jane' }),
    User.create({ email: 'test@test.com', password: 'test' }),
  ]);
};

const initializeClients = async () => {
};

const initializeBooks = async () => {

};

const initializeOrders = async () => {

};

const initializeSells = async () => {
};

const initializeNotifications = async () => {
};

const initializeDatabase = async () => {
  await initializeUsers();
  await initializeClients();
  await initializeBooks();
};

module.exports = {
  initializeDatabase,
};
