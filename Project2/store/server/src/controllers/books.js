const { Book } = require('../models');

const create = async (title, author, price, stock) => Book.create({
  title,
  author,
  price,
  stock,
});

const list = async () => Book.findAll();

const retrieve = async bookId => Book.findByPk(bookId);

const update = async (bookId, stock) => {
  const book = await Book.findByPk(bookId);

  if (!book) {
    throw new Error('Book not found');
  }

  return book.update({
    stock: stock || book.stock,
  });
};

module.exports = {
  create,
  list,
  retrieve,
  update,
};
