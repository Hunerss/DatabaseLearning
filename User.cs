using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePerp
{
    internal class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }

        public User(int id, string login, string password, DateTime birthday)
        {
            Id = id;
            Login = login;
            Password = password;
            Birthday = birthday;
        }

        public User()
        {

        }
    }
}
