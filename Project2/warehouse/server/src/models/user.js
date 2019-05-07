const bcrypt = require('bcrypt');

const SALT_WORK_FACTOR = 10;

module.exports = (sequelize, DataTypes) => {
  const User = sequelize.define('User', {
    email: {
      type: DataTypes.STRING,
      unique: true,
      allowNull: false,
    },
    password: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    role: {
      type: DataTypes.ENUM('EMPLOYEE', 'ADMIN'),
      allowNull: false,
      defaultValue: 'EMPLOYEE',
    },
  },
  {});

  // eslint-disable-next-line
  User.prototype.isValidPassword = async function (password) {
    const compare = await bcrypt.compare(password, this.password);
    return compare;
  };

  User.beforeCreate(async (user) => {
    /* eslint-disable */
    user.password = await bcrypt.hash(user.password, SALT_WORK_FACTOR);
  });

  return User;
};
