const router = require('express').Router();
const { sellsController } = require('../controllers');

router.post('/sells', async (req, res) => {
  const { quantity, bookId, clientId } = req.body;

  try {
    const sell = await sellsController.create(quantity, bookId, clientId);
    res.status(201).send(sell);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/sells', async (_, res) => {
  try {
    const sells = await sellsController.list();
    res.status(200).send(sells);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/sells/:sellId', async (req, res) => {
  const { sellId } = req.params;

  try {
    const sell = await sellsController.retrieve(sellId);
    res.status(200).send(sell);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;
