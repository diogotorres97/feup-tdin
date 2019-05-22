const { PUSHER_CHANNEL_STORE } = require('./../config/configs');
const { messageType } = require('../enums');
const { sendNotificationMessage } = require('../services/websockets/pusher');

module.exports = (sequelize, DataTypes) => {
  const ReceiveStock = sequelize.define('ReceiveStock', {
    quantity: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    processedDate: {
      type: DataTypes.DATE,
    },
    ordersId: {
      type: DataTypes.JSONB,
    },
  }, {});

  ReceiveStock.associate = (models) => {
    ReceiveStock.belongsTo(models.Book, {
      foreignKey: 'bookId',
    });
  };

  ReceiveStock.afterCreate(async (receiveStock) => {
    const book = await receiveStock.getBook();
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.createReceiveStock, {...receiveStock.dataValues, book});
  });

  ReceiveStock.afterUpdate(async (receiveStock) => {
    const book = await receiveStock.getBook();
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.updateReceiveStock, {...receiveStock.dataValues, book});
  });

  return ReceiveStock;
};
