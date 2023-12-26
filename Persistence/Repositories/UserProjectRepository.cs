using Application.Interfaces.UserProjectInterfaces;
using Domain.CustomExceptions;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertUserProjectRelation(UserProject userProject)
        {
            try
            {
                await _dbContext.AddAsync(userProject);
                await _dbContext.SaveChangesAsync();
                return true;
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
    }
}
