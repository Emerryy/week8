using System;
using System.Collections.Generic;
using System.Text;

namespace PetInfoClient.Models
{
    public class APIUser
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }

    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
