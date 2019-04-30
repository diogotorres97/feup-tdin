const { Client, Order, Sell } = require('../models');

const create = async (name, address, email) => Client.create({
  name,
  address,
  email,
});

const list = async () => Client.findAll();

const retrieve = async clientId => Client.findByPk(clientId);

const update = async (clientId, address) => {
  const client = await Client.findByPk(clientId);

  if (!client) {
    throw new Error('Client not found');
  }

  return Client.update({
    address: address || client.address,
  });
};

// TODO: add retrieve orders

module.exports = {
  create,
  list,
  retrieve,
  update,
};
