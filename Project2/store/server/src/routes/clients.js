const router = require('express').Router();
const { clientsController } = require('../controllers');

router.post('/clients', async (req, res) => {
  const { name, address, email } = req.body;

  try {
    const client = await clientsController.create(name, address, email);
    res.status(201).send(client);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.post('/clients/association', async (req, res) => {
  const { name, address, email } = req.body;

  try {
    let client = await clientsController.getClientByEmail(email);
    if(!client)
      client = await clientsController.create(name, address, email);
    
      const clientAssociatedToUser = await clientsController.associateUserToClient(req.user.id, client.id);
    res.status(201).send(clientAssociatedToUser);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/clients/association', async (req, res) => {
  try {
    const client = await clientsController.retrieveByUserId(req.user.id);
    res.status(200).send(client);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/clients', async (_, res) => {
  try {
    const clients = await clientsController.list();
    res.status(200).send(clients);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/clients/:clientId', async (req, res) => {
  const { clientId } = req.params;

  try {
    const client = await clientsController.retrieve(clientId);
    res.status(200).send(client);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.put('/clients/:clientId', async (req, res) => {
  const { clientId } = req.params;
  const { address } = req.body;

  try {
    const client = await clientsController.update(clientId, address);
    res.status(200).send(client);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;
