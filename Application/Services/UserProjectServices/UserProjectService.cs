using Application.Interfaces.UserProjectInterfaces;
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
                await _userProjectValidator.ValidateAndThrowAsync( userProject );
                UserProject newUserProject = await _userProjectRepository.InsertUserProjectRelation( userProject );
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
