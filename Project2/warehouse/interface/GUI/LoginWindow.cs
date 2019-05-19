using System;
using System.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace @interface
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void txtBoxEmail_TextChanged(object sender, EventArgs e)
        {
            enableBtnLogin();
        }

        private void txtBoxPassword_TextChanged(object sender, EventArgs e)
        {
            enableBtnLogin();
        }

        private void enableBtnLogin()
        {
            if (txtBoxEmail.Text.Length > 0 && txtBoxPassword.Text.Length > 0)
                btnLogin.Enabled = true;
            else
                btnLogin.Enabled = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!CheckEmail())
                return;
            Login();
            Hide();
            WarehouseWindow form = new WarehouseWindow();
            form.Closed += (s, args) => Close();
            form.Show();
        }

        private void Login()
        {
            string email = txtBoxEmail.Text;
            string password = txtBoxPassword.Text;
            dynamic body = new ExpandoObject();
            body.email = email;
            body.password = password;
            string credentials = JsonConvert.SerializeObject(body);

            IRestResponse response = Utils.ExecuteAuthRequest(Utils.Login, credentials);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    JToken obj = JToken.Parse(response.Content);
                    Utils.Token = obj.Value<string>("token");
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckEmail()
        {
            if (!IsValidEmail(txtBoxEmail.Text))
            {
                lblErrorEmail.Visible = true;
                return false;
            }

            lblErrorEmail.Visible = false;
            return true;
        }
    }
}