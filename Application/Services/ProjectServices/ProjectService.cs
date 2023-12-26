#region Usings

// Application
using Application.DTOs.ProjectDTOs;
using Application.Interfaces.ProjectInterfaces;
using Application.Wrappers;

// Domain
using Domain.CustomExceptions;
using Domain.Entities;

// External libraries
using AutoMapper;
using FluentValidation;
using Application.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Http;

#endregion

namespace Application.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IValidator<Project> _projectValidator;
        private readonly IValidator<CreateProjectDTO> _createProjectDTOValidator;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public ProjectService(
            IProjectRepository projectRepository,
            IValidator<Project> projectValidator,
            IValidator<CreateProjectDTO> createProjectDTOValidator,
            ITokenService tokenService,
            IMapper mapper
        )
        {
            _projectRepository = projectRepository;
            _createProjectDTOValidator = createProjectDTOValidator;
            _projectValidator = projectValidator;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<Response<ProjectDTO>> CreateProject(int user_id, CreateProjectDTO body)
        {
            try
            {
                await _createProjectDTOValidator.ValidateAndThrowAsync( body );
                Project project = _mapper.Map<Project>( body );
                project.ProjectOwnerId = user_id;
                await _projectValidator.ValidateAndThrowAsync( project );
                var newProject = await _projectRepository.InsertProject(project);
                ProjectDTO dto = _mapper.Map<ProjectDTO>(newProject);
                return new Response<ProjectDTO>(dto);
            }
            catch ( Exception ex ) when ( ex is DataAccessException || ex is FluentValidation.ValidationException )
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
