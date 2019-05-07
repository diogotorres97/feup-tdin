const router = require('express').Router();
const { notificationController } = require('../controllers');

router.get('/notifications', async (req, res) => {
  try {
    const notifications = await notificationController.list();
    res.status(201).send(notifications);
  } catch (error) {
    res.status(400).send(error);
  }
});

router.put('/notifications/:notificationId', async (req, res) => {
  const { notificationId } = req.params;

  try {
    const notification = await notificationController.update(notificationId);
    res.status(201).send(notification);
  } catch (error) {
    res.status(400).send(error);
  }
});

module.exports = router;
