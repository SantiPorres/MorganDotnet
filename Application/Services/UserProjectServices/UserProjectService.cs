using Application.Interfaces.UserProjectInterfaces;
using Domain.CustomExceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.Services.UserProjectServices
{
    public class UserProjectService : IUserProjectService
    {
        private readonly IUserProjectRepository _userProjectRepository;
        private readonly IValidator<UserProject> _userProjectValidator;

        public UserProjectService(
            IUserProjectRepository userProjectRepository, 
            IValidator<UserProject> userProjectValidator
        )
        {
            _userProjectRepository = userProjectRepository;
            _userProjectValidator = userProjectValidator;
        }

        public async Task<bool> CreateRelation(UserProject userProject)
        {
            try
            {
                //var validation = await _userProjectValidator.ValidateAsync( userProject );
                //if (!validation.IsValid)
                //{
                //    return false;
                //}
                UserProject newUserProject = await _userProjectRepository.InsertUserProjectRelation( userProject );
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> VerifyRelation(Guid projectId, Guid userId)
        {
            try
            {
                IEnumerable<UserProject> relations = await _userProjectRepository.GetRelations(projectId, userId);
                if (relations.Any())
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
