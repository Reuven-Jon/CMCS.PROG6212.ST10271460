using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}