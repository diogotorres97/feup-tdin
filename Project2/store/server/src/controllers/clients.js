const {
  Client, Order, Sell, Book, User,
} = require('../models');

const create = async (name, address, email) => Client.create({
  name,
  address,
  email,
});

const list = async () => Client.findAll();

const retrieve = async clientId => Client.findByPk(clientId, {
  include: [
    { model: Order, include: Book },
    { model: Sell, include: Book },
  ],
});

const retrieveByUserId = async userId => Client.findOne({
  where: {
    userId,
  },
  include: [
    { model: Order, include: Book },
    { model: Sell, include: Book },
  ],
});

const update = async (clientId, address) => {
  const client = await Client.findByPk(clientId);

  if (!client) {
    throw new Error('Client not found');
  }

  return client.update({
    address: address || client.address,
  });
};

const associateUserToClient = async (userId, clientId) => {
  const client = await Client.findByPk(clientId);

  if (!client) {
    throw new Error('Client not found');
  }

  if (client.userId) {
    throw new Error('Client already have a user associated');
  }

  return client.update({ userId });
};

const getClientId = async (req) => {
  let clientId;
  if (req.user.role === 'EMPLOYEE') {
    clientId = req.body.clientId;
  } else {
    const client = await retrieveByUserId(req.user.id);
    if (!client) throw new Error("User don't have a client associated");
    clientId = client.id;
  }
  return clientId;
};

module.exports = {
  create,
  list,
  retrieve,
  update,
  retrieveByUserId,
  associateUserToClient,
  getClientId,
};
