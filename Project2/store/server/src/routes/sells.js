const router = require('express').Router();
const { sellsController } = require('../controllers');

router.post('/sells', async (req, res) => {
  const { quantity, bookId } = req.body;
  const clientId = req.user.id;

  try {
    const sell = await sellsController.create(quantity, bookId, clientId);
    res.status(201).send(sell);
  } catch (error) {
    res.status(400).send(error);
  }
});

router.get('/sells', async (_, res) => {
  try {
    const sells = await sellsController.list();
    res.status(201).send(sells);
  } catch (error) {
    res.status(400).send(error);
  }
});

router.get('/sells/:sellId', async (req, res) => {
  const { sellId } = req.params;

  try {
    const sell = await sellsController.retrieve(sellId);
    res.status(201).send(sell);
  } catch (error) {
    res.status(400).send(error);
  }
});

module.exports = router;
