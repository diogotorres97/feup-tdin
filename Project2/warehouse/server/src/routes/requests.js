const router = require('express').Router();
const { requestsController } = require('../controllers');

router.get('/requests', async (_, res) => {
  try {
    const requests = await requestsController.list();
    res.status(200).send(requests);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put('/requests/:requestId/sendStock', async (req, res) => {
  const { requestId } = req.params;

  try {
    const request = await requestsController.sendStock(requestId);
    res.status(200).send(request);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;
