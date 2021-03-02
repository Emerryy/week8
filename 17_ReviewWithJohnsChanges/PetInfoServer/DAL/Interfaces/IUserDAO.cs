

using PetInfoServer.Models;

namespace PetInfoServer.DAL
{
    public interface IUserDAO
    {
        User GetUser(string username);
        User AddUser(string username, string password);
    }
}
