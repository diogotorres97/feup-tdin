const { User } = require('../models');

const {
  booksController,
} = require('../controllers');

const initializeUsers = async () => {
  await Promise.all([
    User.create({ email: 'employee@store.com', password: 'employee', role: 'EMPLOYEE' }),
    User.create({ email: 'admin@store.com', password: 'admin', role: 'ADMIN' }),
  ]);
};

const initializeBooks = async () => {
  await Promise.all([
    booksController.create('The Hunger Games', 'Suzanne Collins', 2.97, 100),
    booksController.create('Harry Potter and the Order of the Phoenix', ' J.K. Rowling', 5.79, 100),
    booksController.create('To Kill a Mockingbird', 'Harper Lee', 8.99, 100),
    booksController.create('Pride and Prejudice', ' Jane Austen', 7.99, 100),
    booksController.create('Twilight', 'Stephenie Meyer', 9.99, 100),
  ]);
};

const initializeDatabase = async () => {
  await initializeUsers();
  await initializeBooks();
};

module.exports = {
  initializeDatabase,
};
