using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook_App
{
    class Person
    {
        public String id { get; set; }
        public String user { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public String address { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }

        public Person(String id, String user, String name, String email, String phone, String address, String created_at, String updated_at)
        {
            this.id = id;
            this.user = user;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.created_at = created_at;
            this.updated_at = updated_at;
        }
    }
}
