const router = require('express').Router();
const { receiveStockController } = require('../controllers');

router.get('/receiveStock', async (_, res) => {
  try {
    const receivedStock = await receiveStockController.list();
    res.status(200).send(receivedStock);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put('/receiveStock/:receiveStockId/receiveStock', async (req, res) => {
  const { receiveStockId } = req.params;

  try {
    const receiveStock = await receiveStockController.receiveStock(receiveStockId);
    res.status(200).send(receiveStock);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;
