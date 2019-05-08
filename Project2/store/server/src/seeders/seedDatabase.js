const { User } = require('../models');

const {
  clientsController,
  booksController,
  ordersController,
  sellsController,
} = require('../controllers');

const initializeUsers = async () => {
  await Promise.all([
    User.create({ email: 'employee@store.com', password: 'employee', role: 'EMPLOYEE' }),
    User.create({ email: 'admin@store.com', password: 'admin', role: 'ADMIN' }),
    User.create({ email: 'john@store.com', password: 'john' }),
    User.create({ email: 'jane@store.com', password: 'jane' }),
    User.create({ email: 'test@test.com', password: 'test' }),
  ]);
};

// TODO: associate userId
const initializeClients = async () => {
  await Promise.all([
    clientsController.create('john', 'porto', 'john@store.com'),
    clientsController.create('jane', 'porto', 'jane@store.com'),
    clientsController.create('random', 'porto', 'random@store.com'),
  ]);
};

const initializeBooks = async () => {
  await Promise.all([
    booksController.create('The Hunger Games', 'Suzanne Collins', 2.97, 10),
    booksController.create('Harry Potter and the Order of the Phoenix', 'J.K. Rowling', 5.79, 3),
    booksController.create('To Kill a Mockingbird', 'Harper Lee', 8.99, 1),
    booksController.create('Pride and Prejudice', 'Jane Austen', 7.99, 50),
    booksController.create('Twilight', 'Stephenie Meyer', 9.99, 5),
  ]);
};

const initializeOrders = async () => {

};

const initializeSells = async () => {
};


const initializeDatabase = async () => {
  await initializeUsers();
  await initializeClients();
  await initializeBooks();
};

module.exports = {
  initializeDatabase,
};
