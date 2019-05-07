const router = require('express').Router();

/*+++++++++++++++++++++++++++++++++++++++++++++
 Routes
 ++++++++++++++++++++++++++++++++++++++++++++++*/

const passport = require('passport');
const auth = require('./auth');
const books = require('./books');
const clients = require('./clients');
const receiveStock = require('./receiveStock');
const orders = require('./orders');
const sells = require('./sells');


router.use('/api/', auth);
router.use('/api/', passport.authenticate('jwt', { session: false }));
router.use('/api/', books);
router.use('/api/', clients);
router.use('/api/', receiveStock);
router.use('/api/', orders);
router.use('/api/', sells);
router.get('/api', (req, res) => res.status(200).send({
  message: 'Welcome to the Todos API!',
}));


module.exports = router;
