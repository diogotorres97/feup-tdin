const router = require('express').Router();

/*+++++++++++++++++++++++++++++++++++++++++++++
 Routes
 ++++++++++++++++++++++++++++++++++++++++++++++*/

const passport = require('passport');
const auth = require('./auth');
const books = require('./books');
const requests = require('./requests');

router.use('/', auth);
router.use('/api/', passport.authenticate('jwt', { session: false }));
router.use('/api/', books);
router.use('/api/', requests);

module.exports = router;
