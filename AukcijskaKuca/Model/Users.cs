﻿namespace AukcijskaKuca.Model
{
    public class Users
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        //private int userType;

        //public int UserType
        //{
        //    get { return userType; }
        //    set { userType = value; }
        //}

    }
}
