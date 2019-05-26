const { PUSHER_CHANNEL_STORE } = require('./../config/configs');
const { messageType } = require('../enums');
const { sendNotificationMessage } = require('../services/websockets/pusher');

module.exports = (sequelize, DataTypes) => {
  const Client = sequelize.define('Client', {
    name: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    address: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    email: {
      type: DataTypes.STRING,
      allowNull: false,
      unique: true,
      validate: {
        isEmail: true,
      },
    },
  });

  Client.associate = (models) => {
    Client.hasMany(models.Order, {
      foreignKey: 'clientId',
    });
    Client.hasMany(models.Sell, {
      foreignKey: 'clientId',
    });
  };

  Client.afterCreate(async (client) => {
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.createClient, client);
  });

  Client.afterUpdate(async (client) => {
    sendNotificationMessage(PUSHER_CHANNEL_STORE, messageType.updateClient, client);
  });

  return Client;
};
