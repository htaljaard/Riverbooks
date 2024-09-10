
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users.Data
{
    internal class EFApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UsersDBContext _context;
        public EFApplicationUserRepository(UsersDBContext context)
        {
            _context = context;
        }
        public Task<ApplicationUser> GetUserWithCartByEmailAsync(string emailAddress)
        {
            return _context.ApplicationUsers.Include(u => u.CartItems).SingleAsync(u => u.Email == emailAddress);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}