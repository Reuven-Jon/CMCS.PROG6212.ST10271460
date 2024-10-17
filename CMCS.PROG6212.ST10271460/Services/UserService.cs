<<<<<<< HEAD
﻿using System.Threading.Tasks;
using CMCS.PROG6212.ST10271460.Models;
=======
﻿using CMCS.PROG6212.ST10271460.Models;
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7

namespace CMCS.PROG6212.ST10271460.Services
{
    public class UserService : IUserService
    {
<<<<<<< HEAD
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
            return Task.FromResult(_context.Users.FirstOrDefault(u => u.UserID == id));
        }

    }
}




=======
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
>>>>>>> e67e039fed6ea280849229b3d400860b8a52c9b7
