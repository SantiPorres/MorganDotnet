using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Data.Common;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                IEnumerable<User> users = await _dbContext.Users.ToListAsync();
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

        public async Task<User?> GetOneById(int id)
        {
            try
            {
                User? user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
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
                User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
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
