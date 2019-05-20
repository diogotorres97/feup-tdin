const { PUSHER_CHANNEL_STORE } = require('./../config/configs');
const { messageType } = require('../enums');
const { sendNotificationMessage } = require('../services/websockets/pusher');

module.exports = (sequelize, DataTypes) => {
  const Sell = sequelize.define('Sell', {
    uuid: {
      type: DataTypes.UUID,
      defaultValue: DataTypes.UUIDV4,
      primaryKey: true,
    },
    quantity: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    totalPrice: {
      type: DataTypes.DECIMAL(10, 2),
      allowNull: false,
      defaultValue: 0,
    },
  });

  Sell.associate = (models) => {
    Sell.belongsTo(models.Book, {
      foreignKey: 'bookId',
    });
    Sell.belongsTo(models.Client, {
      foreignKey: 'clientId',
    });
  };

  Sell.beforeCreate(async (sell) => {
    // Update total Price
    const book = await sell.getBook();
    sell.totalPrice = sell.quantity * book.price;
  });

  Sell.afterCreate(async (sell) => {
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.createSell, sell);
  });

  Sell.afterUpdate(async (sell) => {
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.updateSell, sell);
  });

  return Sell;
};
