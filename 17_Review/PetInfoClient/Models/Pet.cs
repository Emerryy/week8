using System;
using System.Collections.Generic;
using System.Text;

namespace PetInfoClient.Models
{
    public class Pet
    {
        private string name;

        public int Id { get; set; }
        public string Name
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
        public string Type { get; set; }
        public virtual string Breed { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsSpayedOrNeutered { get; set; }
        public DateTime LastVetVisit { get; set; }

        public Pet(string name)
        {
            Name = name;
        }
        public Pet()
        {

        }

        public override string ToString()
        {
            return $"{Id} - {Name} - {Type} - {Breed}";
        }
    }
}


