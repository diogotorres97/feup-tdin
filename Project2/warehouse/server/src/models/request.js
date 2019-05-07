module.exports = (sequelize, DataTypes) => {
  const Request = sequelize.define('Request', {
    quantity: {
      type: DataTypes.INTEGER,
      allowNull: false,
      defaultValue: 0,
    },
    processedDate: {
      type: DataTypes.DATE,
    },
  }, {});

  Request.associate = (models) => {
    Request.belongsTo(models.Book, {
      foreignKey: 'bookId',
    });
  };

  return Request;
};
