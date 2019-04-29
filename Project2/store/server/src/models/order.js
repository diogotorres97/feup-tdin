module.exports = (sequelize, DataTypes) => {
  const Order = sequelize.define('Order', {
    uuid: {
      type: DataTypes.UUID,
      defaultValue: DataTypes.UUIDV4,
      primaryKey: true
    },
    quantity: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    },
    totalPrice: {
      type: DataTypes.FLOAT,
      allowNull: false,
      defaultValue: 0
    },
    state: {
      type: DataTypes.ENUM({
        values: [
          'WAITING', 
          'DELIVERED',
          'DISPATCH'
        ]
      })
    },
    stateDate: {
      type: DataTypes.DATE,
      allowNull: true,
    }
  });

  Order.associate = (models) => {
    Order.belongsTo(models.Book);
    Order.belongsTo(models.Client);
  };

  Order.beforeCreate(async (order) => {
    console.log(order);
    //Update total Price
    order.totalPrice = order.quantity * 1;
  
    //order.getBook() ??
  });

  return Order;
};

