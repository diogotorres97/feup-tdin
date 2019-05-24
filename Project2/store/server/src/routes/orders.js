const router = require('express').Router();
const { ordersController, clientsController } = require('../controllers');

router.post('/orders', async (req, res) => {
  const { quantity, bookId } = req.body;

  try {
    const clientId = await clientsController.getClientId(req);
    const order = await ordersController.create(quantity, bookId, clientId);
    res.status(201).send(order);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/orders', async (_, res) => {
  try {
    const orders = await ordersController.list();
    res.status(200).send(orders);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/orders/:orderId', async (req, res) => {
  const { orderId } = req.params;

  try {
    const order = await ordersController.retrieve(orderId);
    res.status(200).send(order);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put('/orders/:orderId', async (req, res) => {
  const { orderId } = req.params;
  const { state, stateDate } = req.body;

  try {
    const order = await ordersController.update(orderId, state, stateDate);
    res.status(200).send(order);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;
