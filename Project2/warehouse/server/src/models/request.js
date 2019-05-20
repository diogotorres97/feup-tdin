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
    sendNotificationMessage(PUSHER_CHANNEL_WAREHOUSE, messageType.createRequest, request);
  });

  Request.afterUpdate(async (request) => {
    sendNotificationMessage(PUSHER_CHANNEL_WAREHOUSE, messageType.updateRequest, request);
  });

  return Request;
};
