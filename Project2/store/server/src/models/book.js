module.exports = (sequelize, DataTypes) => {
  const Book = sequelize.define('Book', {
    uuid: {
      type: DataTypes.UUID,
      defaultValue: DataTypes.UUIDV4,
      primaryKey: true
    },
    title: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    author: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    price: {
      type: DataTypes.FLOAT,
      allowNull: false,
      defaultValue: 0
    },
    stock: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0
    }
  });

  Book.associate = (models) => {
    // Book.belongsToMany(models.Order);
    // Book.belongsToMany(models.Sell);
  };

  return Book;
};
