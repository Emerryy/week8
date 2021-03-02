using System;
using System.Collections.Generic;
using System.Text;

namespace PetInfoClient.Models
{
    public class Owner
    {
        private string name;
        public int Id { get; set; }

        public string FullName
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    name = "default";
                }
                else
                {
                    name = value;
                }
            }
        }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }


        public override string ToString()
        {
            return $"{Id} - {FullName} - {PhoneNumber} - {Email}";
        }

    }
}
