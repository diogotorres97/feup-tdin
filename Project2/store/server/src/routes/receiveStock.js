const router = require('express').Router();
const { receiveStockController } = require('../controllers');

router.get('/receiveStock', async (req, res) => {
  try {
    const requests = await requestsController.list();
    res.status(201).send(requests);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put('/receiveStock/:receiveStockId/receiveStock', async (req, res) => {
  const { receiveStockId } = req.params;

  try {
    const receiveStock = await receiveStockController.receiveStock(receiveStockId);
    res.status(201).send(receiveStock);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;
