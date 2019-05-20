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
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.createReceiveStock, receiveStock);
  });

  ReceiveStock.afterUpdate(async (receiveStock) => {
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.updateReceiveStock, receiveStock);
  });

  return ReceiveStock;
};
