using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.CustomExceptions;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services.UserProjectServices
{
    public class UserProjectService : IUserProjectService
    {
        private readonly IValidator<UserProject> _userProjectValidator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserProjectService(
            IValidator<UserProject> userProjectValidator,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _userProjectValidator = userProjectValidator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserProjectDTO> CreateRelation(UserProject userProject)
        {
            try
            {
                ValidationResult validation = await _userProjectValidator.ValidateAsync(userProject);
                if (validation.IsValid == false)
                {
                    throw new FluentValidation.ValidationException(
                        validation.Errors    
                    );
                }
                UserProject newUserProject = await _unitOfWork.UsersProjects.AddAndGetAsync(userProject);
                await _unitOfWork.Complete();
                UserProjectDTO userProjectDto = _mapper.Map<UserProjectDTO>(newUserProject);
                return userProjectDto;
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is FluentValidation.ValidationException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<IEnumerable<UserProjectDTO>> GetAllRelations(Guid projectId, Guid userId)
        {
            try
            {
                IEnumerable<UserProject> relations = await _unitOfWork.UsersProjects.FindAsync(
                    up => up.ProjectId == projectId && up.UserId == userId
                );
                IEnumerable<UserProjectDTO> relationsDto = _mapper.Map<IEnumerable<UserProjectDTO>>(relations);
                return relationsDto;
            }
            catch (Exception ex) when (
                ex is DataAccessException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }
    }
}
