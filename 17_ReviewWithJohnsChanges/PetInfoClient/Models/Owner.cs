using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetInfoClient.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{OwnerId} - {FullName} - {Phone} - {Email}";
        }
    }
}
