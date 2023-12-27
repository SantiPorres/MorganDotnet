#region Usings

// Application
using Application.DTOs.ProjectDTOs;
using Application.Interfaces.ProjectInterfaces;
using Application.Interfaces.UserInterfaces;
using Application.Interfaces.UserProjectInterfaces;
// External libraries
using AutoMapper;
using FluentValidation;
// Domain
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Enums;

#endregion

namespace Application.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IValidator<Project> _projectValidator;
        private readonly IValidator<CreateProjectDTO> _createProjectDTOValidator;
        private readonly IUserRepository _userRepository;
        private readonly IUserProjectService _userProjectService;
        private readonly IMapper _mapper;

        public ProjectService(
            IProjectRepository projectRepository,
            IValidator<Project> projectValidator,
            IValidator<CreateProjectDTO> createProjectDTOValidator,
            IMapper mapper,
            IUserProjectService userProjectService,
            IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _createProjectDTOValidator = createProjectDTOValidator;
            _projectValidator = projectValidator;
            _mapper = mapper;
            _userProjectService = userProjectService;
            _userRepository = userRepository;
        }

        public async Task<ProjectDTO> CreateProject(int userId, CreateProjectDTO body)
        {
            try
            {
                User user = await _userRepository.GetOneById(userId) ?? throw new KeyNotFoundException("User not found");
                await _createProjectDTOValidator.ValidateAndThrowAsync( body );
                Project project = _mapper.Map<Project>( body );
                project.OwnerId = userId;
                await _projectValidator.ValidateAndThrowAsync( project );
                Project newProject = await _projectRepository.InsertProject(project);
                UserProject userProject = new UserProject(
                    newProject.OwnerId,
                    newProject.Id,
                    UserRole.ProjectAdmin
                );
                if (!await _userProjectService.CreateRelation(userProject))
                {
                    await _projectRepository.DeleteProject(newProject);
                    throw new BusinessException("Project creation failed");
                }
                ProjectDTO dto = _mapper.Map<ProjectDTO>(newProject);
                return dto;
            }
            catch ( Exception ex ) when ( 
                ex is FluentValidation.ValidationException
            )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
