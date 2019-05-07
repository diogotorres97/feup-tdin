const { Notification } = require('../models');

const create = async (type, data) => Notification.create(type, data);
const list = async () => Notification.findAll();

const retrieve = async (notificationId) => {
  const notification = await Notification.findByPk(notificationId);

  if (!notification) {
    throw new Error('Notification not found');
  }

  return notification;
};

const update = async (notificationId) => {
  const notification = await Notification.findByPk(notificationId);

  if (!notification) {
    throw new Error('Notification not found');
  }

  if (notification.readDate) {
    throw new Error('Notification already seen');
  }

  return notification.update({
    readDate: new Date(),
  });
};

module.exports = {
  create,
  list,
  retrieve,
  update,
};
