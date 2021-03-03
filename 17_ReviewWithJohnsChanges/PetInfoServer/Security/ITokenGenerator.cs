using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetInfoServer.Security
{
    public interface ITokenGenerator
    {
        string GenerateToken(int userId, string username, string role);
    }
}
