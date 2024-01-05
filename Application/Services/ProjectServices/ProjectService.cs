// Application
using Application.DTOs.ProjectDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Application.Interfaces;
using Application.Interfaces.IServices;

// External libraries
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
// Domain
using Domain.CustomEntities;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.Options;

namespace Application.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IValidator<Project> _projectValidator;
        private readonly IValidator<CreateProjectDTO> _createProjectDTOValidator;
        private readonly IValidator<PaginationQueryParameters> _paginationQueryParametersValidator;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IUserProjectService _userProjectService;

        private readonly IOptions<PaginationOptions> _paginationOptions;
        private readonly IMapper _mapper;

        public ProjectService(
            IValidator<Project> projectValidator,
            IValidator<CreateProjectDTO> createProjectDTOValidator,
            IValidator<PaginationQueryParameters> paginationQueryParametersValidator,

            IUnitOfWork unitOfWork,
            IUserService userService,
            IUserProjectService userProjectService,

            IOptions<PaginationOptions> paginationOptions,
            IMapper mapper
        )
        {
            _projectValidator = projectValidator;
            _createProjectDTOValidator = createProjectDTOValidator;
            _paginationQueryParametersValidator = paginationQueryParametersValidator;

            _unitOfWork = unitOfWork;
            _userService = userService;
            _userProjectService = userProjectService;

            _paginationOptions = paginationOptions;
            _mapper = mapper;
        }

        public async Task<PagedList<ProjectDTO>> GetAllProjectsByUserId(PaginationQueryParameters filters, Guid userId)
        {
            try
            {
                ValidationResult validationResult = await _paginationQueryParametersValidator.ValidateAsync(
                    filters
                );
                if (validationResult.IsValid == false)
                    throw new FluentValidation.ValidationException(
                        validationResult.Errors
                    );
                UserDTO userDto = await _userService.GetUserById(userId, true);
                ICollection<UserProject>? userProjectRelations = userDto.UserProjects;
                if (userProjectRelations is null || userProjectRelations.Count == 0)
                    return PagedList<ProjectDTO>.CreateEmpty();
                ICollection<ProjectDTO> projectsDTOs = new Collection<ProjectDTO>();
                foreach (UserProject relation in userProjectRelations)
                {
                    ProjectDTO projectDto = await GetProjectById(
                        relation.ProjectId,
                        false
                    );
                    projectsDTOs.Add(projectDto);
                }
                return PagedList<ProjectDTO>.Create(
                    projectsDTOs,
                    filters.PageNumber ?? _paginationOptions.Value.DefaultPageNumber,
                    filters.PageSize ?? _paginationOptions.Value.DefaultPageSize
                );
            }
            catch (Exception ex) when (
                ex is KeyNotFoundException
                || ex is FluentValidation.ValidationException
                || ex is DataAccessException
                || ex is BusinessException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<ProjectDTO> GetProjectById(Guid projectId, bool? navigable = true)
        {
            try
            {
                Project project;
                if (navigable == true)
                    project = await _unitOfWork.Projects.GetWithNavigationAsync(projectId);
                else
                    project = await _unitOfWork.Projects.GetAsync(projectId);
                ProjectDTO dto = _mapper.Map<ProjectDTO>(project);
                return dto;
            }
            catch (Exception ex) when (
                ex is KeyNotFoundException
                || ex is DataAccessException
                || ex is BusinessException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<ProjectDTO> CreateProject(Guid userId, CreateProjectDTO body)
        {
            try
            {
                UserDTO user = await _userService.GetUserById(userId, false);
                await _createProjectDTOValidator.ValidateAndThrowAsync(body);
                Project project = _mapper.Map<Project>(body);
                await _projectValidator.ValidateAndThrowAsync(project);
                Project newProject = await _unitOfWork.Projects.AddAndGetAsync(project);
                UserProject userProject = new UserProject
                {
                    UserId = user.Id,
                    ProjectId = newProject.Id,
                    Role = UserRole.ProjectAdmin
                };
                await _userProjectService.CreateRelation(userProject);
                await _unitOfWork.Complete();
                ProjectDTO dto = _mapper.Map<ProjectDTO>(newProject);
                return dto;
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }
    }
}
