using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.CustomExceptions;
using Domain.Entities;
using FluentValidation;

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
                //var validation = await _userProjectValidator.ValidateAsync( userProject );
                //if (!validation.IsValid)
                //{
                //    return false;
                //}
                UserProject newUserProject = await _unitOfWork.UsersProjects.AddAndGetAsync(userProject);
                await _unitOfWork.Complete();
                UserProjectDTO userProjectDto = _mapper.Map<UserProjectDTO>(newUserProject);
                return userProjectDto;
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        //public async Task<bool> VerifyRelation(Guid projectId, Guid userId)
        //{
        //    try
        //    {
        //        IEnumerable<UserProject> relations = await _userProjectRepository.GetRelations(projectId, userId);
        //        if (relations.Any())
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new BusinessException(ex.Message);
        //    }
        //}
    }
}
