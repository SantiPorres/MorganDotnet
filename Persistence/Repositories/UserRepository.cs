using Application.Interfaces.UserInterfaces;
using Domain.CustomExceptions;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.ComponentModel.Design;
using System.Data.Common;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers(bool navigateUserProjects)
        {
            try
            {
                IEnumerable<User> users;
                if (navigateUserProjects)
                {
                    users = await _dbContext.Users
                        .Include(p => p.UserProjects)
                        .ToListAsync();
                } else
                {
                    users = await _dbContext.Users
                        .ToListAsync();
                }
                return users;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException($"SqlException: {ex.Message}", ex.Errors);
            }
            catch (Exception ex) when (ex is Exception || ex is DbException)
            {
                throw new DataAccessException($"An error occurred: {ex.Message}");
            }
        }

        public async Task<User?> GetOneById(Guid userId, bool navigateUserProjects)
        {
            try
            {
                User? user;
                if (navigateUserProjects)
                {
                    user = await _dbContext.Users
                        .Include(p => p.UserProjects)
                        .SingleOrDefaultAsync(u => u.Id == userId);
                } else
                {
                    user = await _dbContext.Users
                        .SingleOrDefaultAsync(u => u.Id == userId);
                }
                return user;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException($"SqlException: {ex.Message}", ex.Errors);
            }
            catch (Exception ex) when (ex is Exception || ex is DbException)
            {
                throw new DataAccessException($"An error occurred: {ex.Message}");
            }
        }

        public async Task<User?> GetOneByEmail(string email)
        {
            try
            {
                User? user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
                return user;
            }
            catch(SqlException ex)
            {
                throw new DataAccessException($"SqlException: {ex.Message}", ex.Errors);
            }
            catch (Exception ex) when (ex is Exception || ex is DbException)
            {
                throw new DataAccessException($"An error occurred: {ex.Message}");
            }
        }

        public async Task<User> Insert(User user)
        {
            try
            {
                var newUser = await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return newUser.Entity;
            }
            catch (Exception ex) when (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
            {
                throw new DataAccessException($"DbUpdateException or DbUpdateConcurrencyException: {ex.Message}");
            }
            catch (SqlException ex)
            {
                throw new DataAccessException($"SqlException: {ex.Message}", ex.Errors);
            }
            catch(Exception ex)
            {
                throw new DataAccessException($"An error occurred: {ex.Message}");
            }
        }
    }
}
