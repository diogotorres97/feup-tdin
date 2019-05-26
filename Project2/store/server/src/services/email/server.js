const hbs = require('nodemailer-express-handlebars');
const nodemailer = require('nodemailer');
require('dotenv').config();

const environment = process.env;
const templatePath = 'src/services/email/templates';

const gmailTransport = nodemailer.createTransport({
  service: environment.GMAIL_SERVICE_NAME,
  host: environment.GMAIL_SERVICE_HOST,
  secure: environment.GMAIL_SERVICE_SECURE,
  port: environment.GMAIL_SERVICE_PORT,
  auth: {
    user: environment.GMAIL_USER_NAME,
    pass: environment.GMAIL_USER_PASSWORD,
  },
});

const mailtrapTransport = nodemailer.createTransport({
  host: environment.MAILTRAP_SERVICE_HOST,
  port: environment.MAILTRAP_SERVICE_PORT,
  auth: {
    user: environment.MAILTRAP_USER_NAME,
    pass: environment.MAILTRAP_USER_PASSWORD,
  },
});

let counter = 0; // variable to send 2 messages each 10 sec
const { sleep, AsyncQueue } = require('../../utils/utils');

const emailsQueue = new AsyncQueue();

function pushEmail(from, to, subject, template, context) {
  const mailOptions = {
    from, // sender address, gmail overrides this
    to, // list of receivers
    subject, // Subject line
    template, // html template
    context, // object with variables to template
  };

  emailsQueue.push(mailOptions);
}

async function sendEmail(mailOptions) {
  try {
    return mailtrapTransport.sendMail(mailOptions);
  } catch (e) {
    console.log(e);
  }
}

async function run() {
  while (true) {
    const mailOptions = await emailsQueue.pop();
    if (counter === 2) {
      await sleep(10000);
      counter = 0;
    }
    await sendEmail(mailOptions);
    counter++;
  }
}

const start = () => {
  mailtrapTransport.use('compile', hbs({
    viewEngine: {
      partialsDir: templatePath,
    },
    viewPath: templatePath,
    extName: '.hbs',
  }));
  (async () => {
    run();
  })();
};

module.exports = {
  start,
  pushEmail,
};
