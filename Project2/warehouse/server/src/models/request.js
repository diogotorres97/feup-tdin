const { messageType } = require('../enums');
const { sendNotificationMessage } = require('../services/websockets/pusher');
const { PUSHER_CHANNEL_WAREHOUSE } = require('../config/configs');

module.exports = (sequelize, DataTypes) => {
  const Request = sequelize.define('Request', {
    quantity: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    processedDate: {
      type: DataTypes.DATE,
    },
  }, {});

  Request.associate = (models) => {
    Request.belongsTo(models.Book, {
      foreignKey: 'bookId',
    });
  };

  Request.afterCreate(async (request) => {
    const book = await request.getBook();
    sendNotificationMessage(PUSHER_CHANNEL_WAREHOUSE, messageType.createRequest, { ...request.dataValues, book });
  });

  Request.afterUpdate(async (request) => {
    const book = await request.getBook();
    sendNotificationMessage(PUSHER_CHANNEL_WAREHOUSE, messageType.updateRequest, { ...request.dataValues, book });
  });

  return Request;
};
