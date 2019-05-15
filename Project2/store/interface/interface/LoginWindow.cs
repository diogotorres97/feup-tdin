﻿using Newtonsoft.Json;
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
            StoreWindow form = new StoreWindow();
            form.Closed += (s, args) => this.Close();
            form.Show();
            

        }

        private bool login()
        {
            string email = txtBoxEmail.Text;
            string password = txtBoxPassword.Text;
            string parameters = "email=" + email + "&password=" + password;
            
            IRestResponse response = Utils.executeAuthRequest(Utils.login, "", Method.POST, parameters);
            if ((response.Content.StartsWith("{") && response.Content.EndsWith("}")) || //For object
                (response.Content.StartsWith("[") && response.Content.EndsWith("]"))) //For array
            {
                try {
                    JToken obj = JToken.Parse(response.Content);
                    Utils.token = obj.Value<string>("token");
                    
                    return true;
                } catch (JsonReaderException jex) {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                } catch (Exception ex) //some other exception
                  {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            } else {
                MessageBox.Show(response.Content, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
           
        }

        private bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }

        private bool checkEmail()
        {
            if (!IsValidEmail(txtBoxEmail.Text)) {
                lblErrorEmail.Visible = true;
                return false;
            } else {
                lblErrorEmail.Visible = false;
                return true;
            }
        }
    }
}
