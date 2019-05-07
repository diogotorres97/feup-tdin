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

const start = () => {
  mailtrapTransport.use('compile', hbs({
    viewEngine: {
      partialsDir: templatePath,
    },
    viewPath: templatePath,
    extName: '.hbs',
  }));
};

async function sendEmail(from, to, subject, template, context) {
  try {
    const mailOptions = {
      from, // sender address, gmail overrides this
      to, // list of receivers
      subject, // Subject line
      template, // html template
      context, // object with variables to template
    };

    return mailtrapTransport.sendMail(mailOptions);
  } catch (e) {
    console.log(e);
  }
}

module.exports = {
  start,
  sendEmail,
};