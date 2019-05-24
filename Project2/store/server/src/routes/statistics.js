const router = require('express').Router();
const { statisticsController } = require('../controllers');

router.get('/statistics/topBooks', async (req, res) => {
  try {
    const topBooks = await statisticsController.getTopBooks();
    res.status(200).send(topBooks);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/statistics/totalSales', async (_, res) => {
  try {
    const totalSales = await statisticsController.getTotalSales();
    res.status(200).send(totalSales);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;
