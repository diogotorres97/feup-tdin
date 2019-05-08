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

  return Client;
};
