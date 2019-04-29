module.exports = (sequelize, DataTypes) => {
  const Notification = sequelize.define('Notification', {
    // description: {
    //   type: DataTypes.STRING,
    //   allowNull: false,
    //   validate: { len: [2, 200] },
    // },
    // readDate: {
    //   type: DataTypes.DATE,
    // },
  }, {});

  // ASSOCIATIONS
  Notification.associate = (models) => {
    Notification.belongsTo(models.User);
  };

  return Notification;
};
