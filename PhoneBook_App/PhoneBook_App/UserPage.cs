using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Lets add our http libraries
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;


namespace PhoneBook_App
{
    public partial class userPage : Form
    {
        private String tag = "User Page";
        private String user_id;
        //private Authentication authentication;
        List<Person> personList = new List<Person>();
        int index;
        String edit_user_id;
        private Form form;

        public userPage(String user_id, Form form)
        {
            this.user_id = user_id;
            this.form = form;
            InitializeComponent();
        }

        private void UserPage_Load(object sender, EventArgs e)
        {
            this.Text = tag;
            fillListView();
            edit_btn.Click += new EventHandler(edit_btn_Click);
            panel2.Visible = false;
            panel1.Visible = false;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void fillListView()
        {
            listView1.Items.Clear();
            personList = getList();

            if (personList.Count() > 0)
            {
                for (int i = 0; i < personList.Count(); i++)
                {
                    ListViewItem item = new ListViewItem(personList[i].name);
                    //item.SubItems.Add(personList[i].name);
                    item.SubItems.Add(personList[i].email);
                    item.SubItems.Add(personList[i].phone);
                    item.SubItems.Add(personList[i].address);
                    listView1.Items.Add(item);
                }
            }
        }

        List<Person> getList()
        {
            List<Person> personList = new List<Person>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/phonebook/tutorial/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("getList.php?user=" + user_id).Result;

                string res = "";

                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                }

                //If there is error it will stop here
                JObject obj = JObject.Parse(res);
                if (obj["exists"].ToString() == "False")
                {
                    return personList;
                }

                JArray o = JArray.Parse(obj["items"].ToString());

                for (int i = 0; i < o.Count(); i++)
                {
                    JObject person = JObject.Parse(o[i].ToString());
                    personList.Add(new Person(
                        person["id"].ToString(),
                        person["user"].ToString(),
                        person["name"].ToString(),
                        person["email"].ToString(),
                        person["phone"].ToString(),
                        person["address"].ToString(),
                        person["created_at"].ToString(),
                        person["updated_at"].ToString()));

                    /*MessageBox.Show("Name is: " + person["name"].ToString());
                    MessageBox.Show("Phone is: " + person["phone"].ToString());
                    MessageBox.Show("Address is: " + person["address"].ToString());
                    MessageBox.Show("Email is: " + person["email"].ToString());*/
                }
                return personList;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void add_new_Click(object sender, EventArgs e)
        {
            this.Text = "Add New";
            addNew.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = tag;
            addNew.Visible = false;
        }

        private void add_record_Click(object sender, EventArgs e)
        {
            String name = personUserName.Text;
            String email = personEmailAddress.Text;
            String phone = personPhoneNumber.Text;
            String address = personAddressField.Text;

            /*MessageBox.Show("Name: " + name
                + "\nEmail: " + email
                + "\nPhone: " + phone
                + "\nAddress: " + address);*/


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/phonebook/tutorial/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("addNew.php?user=" + user_id
                    +  "&name=" + name
                    +  "&email=" + email
                    +  "&phone=" + phone
                    +  "&address=" + address).Result;

                string res = "";

                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                }

                //If there is error it will stop here
                JObject obj = JObject.Parse(res);
                if (obj["error_msg"].ToString() == "False")
                {
                    MessageBox.Show("New Record Succesfully Added");
                    fillListView();
                    addNew.Visible = false;
                }
                else
                {
                    MessageBox.Show("There is an error!");
                }

                this.Text = tag;
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addNew.Visible = true;
        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                index = listView1.Items.IndexOf(listView1.SelectedItems[0]);

                edit_user_id = personList[index].id;
                String name = personList[index].name;
                String phone = personList[index].phone;
                String email = personList[index].email;
                String address = personList[index].address;

                edit_person_name.Text = name;
                edit_phone_number.Text = phone;
                edit_email_address.Text = email;
                edit_address.Text = address;
            }

            panel1.Visible = true;
            panel2.Visible = true;
        }

        private void edit_back_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            this.Text = "Edit Record";
        }

        private void edit_Click(object sender, EventArgs e)
        {
            //Person person = personList
            String name = edit_person_name.Text;
            String phone = edit_phone_number.Text;
            String email = edit_email_address.Text;
            String address = edit_address.Text;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/phonebook/tutorial/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //MessageBox.Show("User_id is: " + edit_user_id);
                var response = client.GetAsync("updateUser.php?id=" + edit_user_id
                    + "&name=" + name
                    + "&email=" + email
                    + "&phone=" + phone
                    + "&address=" + address).Result;

                string res = "";

                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                }

                //If there is error it will stop here
                JObject obj = JObject.Parse(res);
                if (obj["error_msg"].ToString() == "False")
                {
                    MessageBox.Show("Record Succesfully Updated");
                    fillListView();
                    panel1.Visible = false;
                    panel2.Visible = false;
                }
                else
                {
                    MessageBox.Show("There is an error!\n" + res);
                }

                this.Text = tag;
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            form.Show();
        }
    }
}
