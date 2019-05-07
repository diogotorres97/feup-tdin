module.exports = (sequelize, DataTypes) => {
  const Notification = sequelize.define('Notification', {
    type: {
      type: DataTypes.ENUM('request_stock', 'receive_stock'),
      allowNull: false,
    },
    data: {
      type: DataTypes.JSONB,
      allowNull: false,
    },
    readDate: {
      type: DataTypes.DATE,
    },
  }, {});

  return Notification;
};
