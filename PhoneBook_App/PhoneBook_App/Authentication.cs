using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Lets add our http libraries
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace PhoneBook_App
{
    class Authentication
    {
        public String user_id { get; set; }

        //To Check if email & password is true in the database
        public int loginUser(String email, String password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/phonebook/tutorial/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("login.php?email=" +
                         email + "&password=" +
                         password).Result;

                string res = "";

                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                }

                JObject o = JObject.Parse(res);
                String error = o["exists"].ToString();

                if (error.Equals("False"))
                {
                    return 0;
                }
                else
                {
                    user_id = o["uid"].ToString();
                    return 1;
                }
                return 0;
            }
        }

        //To Register a New User
        public int registerUser(String name, String email, String password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/phonebook/tutorial/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("registeruser.php?name=" +
                         name + "&email=" +
                         email + "&password=" +
                         password).Result;

                string res = "";

                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                }

                JObject o = JObject.Parse(res);
                String error = o["error_msg"].ToString();

                if (error.Equals("True"))
                {
                    return 0;
                }
                else
                {
                    JObject user = JObject.Parse(o["user"].ToString());
                    user_id = user["id"].ToString();
                    return 1;
                }
                return 0;
            }
        }

    }
}
