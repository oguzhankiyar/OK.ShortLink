using OK.ShortLink.Common.Models;
using System.Collections.Generic;

namespace OK.ShortLink.Core.Managers
{
    public interface IUserManager
    {
        List<UserModel> GetUsers(int pageSize = 15, int pageNumber = 1);

        UserModel GetUserById(int id);

        UserModel LoginUser(string username, string password);

        UserModel CreateUser(int userId, string username, string password, bool isActive);

        bool EditUserPassword(int userId, int id, string password);

        bool EditUserActivation(int userId, int id, bool isActive);

        bool EditUser(int userId, int id, string password, bool isActive);

        bool DeleteUser(int userId, int id);
    }
}