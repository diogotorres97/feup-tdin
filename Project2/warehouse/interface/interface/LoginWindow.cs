using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            if (!checkEmail())
                return;
            login();
            this.Hide();
            WarehouseWindow form = new WarehouseWindow();
            form.Closed += (s, args) => this.Close();
            form.Show();


        }

        private void login()
        {
            string email = txtBoxEmail.Text;
            string password = txtBoxPassword.Text;
            string parameters = "email=" + email + "&password=" + password;

            IRestResponse response = Utils.executeAuthRequest(Utils.login, "", Method.POST, parameters);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    JToken obj = JToken.Parse(response.Content);
                    Utils.token = obj.Value<string>("token");

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

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool checkEmail()
        {
            if (!IsValidEmail(txtBoxEmail.Text))
            {
                lblErrorEmail.Visible = true;
                return false;
            }
            else
            {
                lblErrorEmail.Visible = false;
                return true;
            }
        }

    }
}
