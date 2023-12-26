using Application.Interfaces.UserProjectInterfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Services.UserProjectServices
{
    public class UserProjectService
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
            await _userProjectValidator.ValidateAndThrowAsync( userProject );
            return await _userProjectRepository.InsertUserProjectRelation( userProject );
        }
    }
}
