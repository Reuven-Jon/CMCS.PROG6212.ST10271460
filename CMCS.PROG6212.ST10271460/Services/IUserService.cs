using CMCS.PROG6212.ST10271460.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.PROG6212.ST10271460.Services
{
    public interface IUserService
    {
        Task<bool> ValidateUserCredentials(string username, string password);
        Task<User?> GetUserByIdAsync(int id);
        User Authenticate(string username, string password);
    }
}

