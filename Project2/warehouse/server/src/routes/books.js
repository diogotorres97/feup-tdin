const router = require('express').Router();
const { booksController } = require('../controllers');

router.post('/books', async (req, res) => {
  const {
    title, author, price, stock,
  } = req.body;

  try {
    const book = await booksController.create(title, author, price, stock);
    res.status(201).send(book);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/books', async (_, res) => {
  try {
    const books = await booksController.list();
    res.status(201).send(books);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/books/:bookId', async (req, res) => {
  const { bookId } = req.params;

  try {
    const book = await booksController.retrieve(bookId);
    res.status(201).send(book);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put('/books/:bookId', async (req, res) => {
  const { bookId } = req.params;
  const { stock } = req.body;

  try {
    const book = await booksController.update(bookId, stock);
    res.status(201).send(book);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;
