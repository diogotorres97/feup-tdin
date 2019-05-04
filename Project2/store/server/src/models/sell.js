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

  return Sell;
};
