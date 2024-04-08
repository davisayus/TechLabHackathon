using LT.TechLabHackathon.DataAccess.SqlServerContext;
using LT.TechLabHackathon.Domain.Contracts;
using LT.TechLabHackathon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.DataAccess.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SqlContext _context;
        public AuthRepository(SqlContext context)
        {
            _context = context;        
        }

        public async Task<AuthUserKey> AddUserDynamicKey(AuthUserKey userToken)
        {
            var result = await _context.AuthUserKeys.AddAsync(userToken);
            _ = await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> GetUserByEmail(string userEMail)
        {
            var result = await _context.Users.Where(u => u.Email.ToLower() == userEMail.ToLower())
                            .FirstOrDefaultAsync();

            return result ?? new User();
        }

        public async Task<AuthUserKey> GetUserDynamicKey(int userId)
        {
            var result = await _context.AuthUserKeys.Where(k => k.UserId == userId).FirstOrDefaultAsync();
            return result ?? new AuthUserKey();
        }

        public async Task<bool> UpdatePasswordUser(int userId, string password)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserId != userId)
                return false;

            user.Password = password;
            user.StatusId = 1;
            user.UpdatedAt = DateTime.Now;

            _context.Users.Update(user);
            var recordsUpdated = await _context.SaveChangesAsync();
            return (recordsUpdated != 0);
        }

        public async Task<bool> UpdateUserDynamicKey(AuthUserKey userKey)
        {
            _context.AuthUserKeys.Update(userKey);
            var recordsUpdated = await _context.SaveChangesAsync();
            return (recordsUpdated != 0);
        }

    }
}
