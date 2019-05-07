const Pusher = require('pusher');
require('dotenv').config();

const environment = process.env;

const pusher = new Pusher({
  appId: environment.PUSHER_APP_ID,
  key: environment.PUSHER_APP_KEY,
  secret: environment.PUSHER_APP_SECRET,
  cluster: environment.PUSHER_APP_CLUSTER,
  encrypted: true,
});

function sendNotificationMessage(channel, type, data) {
  pusher.trigger(channel, type, data);
}

module.exports = {
  sendNotificationMessage,
};
