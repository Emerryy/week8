using System;
using System.Collections.Generic;
using System.Text;

namespace PetInfoClient.Models
{
    class Owner
    {
        public int OwnerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public override string ToString()
        {
            return $"{OwnerId} - {FullName} - {PhoneNumber} - {EmailAddress}";
        }
    }
}
