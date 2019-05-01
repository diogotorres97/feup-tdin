const router = require('express').Router();

/*+++++++++++++++++++++++++++++++++++++++++++++
 Routes
 ++++++++++++++++++++++++++++++++++++++++++++++*/

const passport = require('passport');
const auth = require('./auth');
const todos = require('./todos');
const todoItems = require('./todoItems');
const books = require('./books');
const clients = require('./clients');
const notifications = require('./notifications');
const orders = require('./orders');
const sells = require('./sells');


router.use('/api/', auth);
router.use('/api/', passport.authenticate('jwt', { session: false }));
router.use('/api/', todos);
router.use('/api/', todoItems);
router.use('/api/', books);
router.use('/api/', clients);
router.use('/api/', notifications);
router.use('/api/', orders);
router.use('/api/', sells);
router.get('/api', (req, res) => res.status(200).send({
  message: 'Welcome to the Todos API!',
}));


module.exports = router;
