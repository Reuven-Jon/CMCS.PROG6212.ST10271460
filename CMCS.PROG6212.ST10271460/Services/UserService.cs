using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Name = "lecturer", Password = "password", Role = Role.Lecturer },
            new User { Name = "coordinator", Password = "password", Role = Role.Coordinator }
        };

        public User Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Name == username && u.Password == password)
                   ?? throw new InvalidOperationException("User not found");
        }
    }
}
