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
  }, {});

  ReceiveStock.associate = (models) => {
    ReceiveStock.belongsTo(models.Book, {
      foreignKey: 'bookId',
    });
  };

  return ReceiveStock;
};
