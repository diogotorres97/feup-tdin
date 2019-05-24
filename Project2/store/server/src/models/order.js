const { PUSHER_CHANNEL_STORE } = require('./../config/configs');
const { messageType } = require('../enums');
const { sendNotificationMessage } = require('../services/websockets/pusher');

module.exports = (sequelize, DataTypes) => {
  const Order = sequelize.define('Order', {
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
    state: {
      type: DataTypes.ENUM({
        values: [
          'WAITING',
          'DELIVERED',
          'DISPATCH',
        ],
      }),
    },
    stateDate: {
      type: DataTypes.DATE,
      allowNull: true,
    },
  });

  Order.associate = (models) => {
    Order.belongsTo(models.Book, {
      foreignKey: 'bookId',
    });
    Order.belongsTo(models.Client, {
      foreignKey: 'clientId',
    });
  };

  Order.beforeCreate(async (order) => {
    // Update total Price
    const book = await order.getBook();
    order.totalPrice = order.quantity * book.price;
  });

  Order.afterCreate(async (order) => {
    const book = await order.getBook();
    const client = await order.getClient();
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.createOrder, { ...order.dataValues, Book: book, Client: client });
  });

  Order.afterUpdate(async (order) => {
    const book = await order.getBook();
    const client = await order.getClient();
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.updateOrder, { ...order.dataValues, Book: book, Client: client });
  });

  return Order;
};
