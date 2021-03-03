using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetInfoServer.Security.Models;

namespace PetInfoServer.Security
{
    public interface IPasswordHasher
    {
        PasswordHash ComputeHash(string plainTextPassword);
        bool VerifyHashMatch(string existingHashedPassword, string plainTextPassword, string salt);
    }
}
