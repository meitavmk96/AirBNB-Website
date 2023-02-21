using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Server.Models
{
    public class User
    {
        //fileds
        string firstName;
        string lastName;
        string email;
        string password;
        bool isActive;
        bool isAdmin;


        //properties
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin=value; }



        //method
        public bool Insert()
        {
            DBservices dbs = new DBservices();
            List<User> UserList = dbs.ReadUsers();

            this.email = this.email.ToLower();

            foreach (User user in UserList) //בדיקה האם המייל כבר רשום
            {

                if (this.email == user.email)
                {
                    return false;
                }
            }

            dbs.InsertUser(this);
            return true;
        }
        public int Update()
        {
            this.email = this.email.ToLower();
            DBservices dbs = new DBservices();   
            return dbs.UpdateUser(this);
        }

        public List<User> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadUsers();
        }

        public User Login(string email, string password)
        {
            DBservices dbs = new DBservices();
            List<User> UserList = dbs.ReadUsers();

            User EmptyUser = new User();
            email = email.ToLower();

            foreach (User user in UserList) //בדיקה האם המייל כבר רשום
            {

                if (email == user.email && password == user.password)
                {
                    return user;
                }
            }
            return EmptyUser;
        }

        public bool Delete()
        {
            DBservices dbs = new DBservices();
            this.email = this.email.ToLower();
            dbs.DeleteUser(this);
            return true;
        }
    }
}
