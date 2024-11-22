using System.Threading.Tasks;
using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Models;

namespace CMCS.PROG6212.ST10271460.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> ValidateUserCredentials(string username, string password)
        {
            // Basic example; You should use a proper hashed password check
            var user = _context.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
            return Task.FromResult(user != null);
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            return Task.FromResult<User?>(_context.Users.FirstOrDefault(u => u.Id == id));
        }

        public User Authenticate(string username, string password)
        {
            // Basic example; You should use a proper hashed password check
            var user = _context.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
            return user ?? throw new InvalidOperationException("User not found");
        }
    }


}


