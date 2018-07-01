using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook_App
{
    public partial class LoginForm : Form
    {
        Authentication user = new Authentication();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            login_btn.Click += new EventHandler(login_btn_Click);
            //Define click handler programmatically, you don't need to write this
            //If you aldready double click the button in the form page and
            //created the handle automatically
            username.Text = "Enter Email Address";
        }

        private void loginbutton_layout_Paint(object sender, PaintEventArgs e)
        {

        }

        //Link To Go Register Page
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
            tableLayoutPanel5.Visible = true;
        }

        //Back Button To Show Login Page Again
        private void back_btn_Click(object sender, EventArgs e)
        {
            tableLayoutPanel5.Visible = false;
            panel1.Visible = false;
        }

        //Register Button
        private void register_btn_Click(object sender, EventArgs e)
        {
            String name = register_username_txtBox.Text;
            String email = register_email_txtBox.Text;
            String password = register_password_txtBox.Text;
            int result = user.registerUser(name, email, password);

            if(result > 0)
            {
                MessageBox.Show("Succesfully Registered: " + user.user_id);
                userPage userPage = new userPage(user.user_id,this);
                userPage.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Could Not Registered\n Email Address is Already Taken");
            }
        }

        //To Login as a User
        private void login_btn_Click(object sender, EventArgs e)
        {
            String email = username_txtbox.Text;
            String password = password_txtbox.Text;
            int result = user.loginUser(email, password);

            if(result > 0)
            {
                MessageBox.Show("Succesfully Logged In: " + user.user_id);
                userPage userPage = new userPage(user.user_id,this);
                userPage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Email Address or Password is Wrong");
            }
        }
    }
}
