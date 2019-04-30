const router = require('express').Router();

/*+++++++++++++++++++++++++++++++++++++++++++++
 Routes
 ++++++++++++++++++++++++++++++++++++++++++++++*/

const passport = require('passport');
const auth = require('./auth');
const books = require('./books');
const notifications = require('./notifications');


router.use('/', auth);
router.use('/api/', passport.authenticate('jwt', { session: false }));
router.use('/api/', books);
router.use('/api/', notifications);
router.get('/api', (req, res) => res.status(200).send({
  message: 'Welcome to the Todos API!',
}));


module.exports = router;
