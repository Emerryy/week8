﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class Users
    {

        public int UserId { get; set; }
        public string Username { get; set; }


        public override string ToString()
        {
            return $" User ID: {UserId} - Username: {Username} ";
        }

    }
}
