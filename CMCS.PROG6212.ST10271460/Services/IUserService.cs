using CMCS.PROG6212.ST10271460.Models;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
=======
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7

namespace CMCS.PROG6212.ST10271460.Services
{
    public interface IUserService
    {
<<<<<<< HEAD
        Task<bool> ValidateUserCredentials(string username, string password);
        Task<User> GetUserByIdAsync(int id);
    }
}
=======
        User Authenticate(string username, string password);
    }
}
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
