//#region Usings

//// Application
//using Application.DTOs.ProjectDTOs;
//using Application.Filters;
//using Application.Interfaces.ProjectInterfaces;
//using Application.Interfaces.UserInterfaces;
//using Application.Interfaces.UserProjectInterfaces;
//// External libraries
//using AutoMapper;
//using Domain.CustomEntities;
//// Domain
//using Domain.CustomExceptions;
//using Domain.Entities;
//using Domain.Enums;
//using Domain.Options;
//using FluentValidation;
//using Microsoft.Extensions.Options;
//using System.Collections.ObjectModel;

//#endregion

//namespace Application.Services.ProjectServices
//{
//    public class ProjectService : IProjectService
//    {
//        private readonly IProjectRepository _projectRepository;
//        private readonly IValidator<Project> _projectValidator;
//        private readonly IValidator<CreateProjectDTO> _createProjectDTOValidator;
//        private readonly IUserRepository _userRepository;
//        private readonly IUserProjectService _userProjectService;
//        private readonly IOptions<PaginationOptions> _paginationOptions;
//        private readonly IMapper _mapper;

//        public ProjectService(
//            IProjectRepository projectRepository,
//            IValidator<Project> projectValidator,
//            IValidator<CreateProjectDTO> createProjectDTOValidator,
//            IOptions<PaginationOptions> paginationOptions,
//            IMapper mapper,
//            IUserProjectService userProjectService,
//            IUserRepository userRepository)
//        {
//            _projectRepository = projectRepository;
//            _createProjectDTOValidator = createProjectDTOValidator;
//            _projectValidator = projectValidator;
//            _paginationOptions = paginationOptions;
//            _mapper = mapper;
//            _userProjectService = userProjectService;
//            _userRepository = userRepository;
//        }

//        public async Task<PagedList<ProjectDTO>> GetProjectsByUserId(PaginationQueryParameters filters, Guid userId)
//        {
//            try
//            {
//                User user = await _userRepository.GetOneById(
//                    userId,
//                    navigateUserProjects: true
//                ) ?? throw new KeyNotFoundException();
//                ICollection<UserProject>? userProjectRelations = user.UserProjects;
//                if (userProjectRelations is null || userProjectRelations.Count == 0)
//                    return PagedList<ProjectDTO>.CreateEmpty();
//                ICollection<ProjectDTO> userProjectsDtos = new Collection<ProjectDTO>();
//                foreach (UserProject relation in userProjectRelations)
//                {
//                    Project project = await _projectRepository.GetProjectById(
//                        relation.ProjectId,
//                        navigateProjectUsers: false
//                    ) ?? throw new DataAccessException();
//                    ProjectDTO projectDto = _mapper.Map<ProjectDTO>(project);
//                    userProjectsDtos.Add(projectDto);
//                }
//                return PagedList<ProjectDTO>.Create(
//                    userProjectsDtos,
//                    filters.PageNumber ?? _paginationOptions.Value.DefaultPageNumber,
//                    filters.PageSize ?? _paginationOptions.Value.DefaultPageSize
//                );
//            }
//            catch (Exception ex)
//            {
//                throw new BusinessException(ex.Message);
//            }
//        }

//        public async Task<ProjectNavigationDTO> GetProjectById(Guid projectId)
//        {
//            try
//            {
//                Project project = await _projectRepository.GetProjectById(
//                    projectId,
//                    navigateProjectUsers: true
//                ) ?? throw new KeyNotFoundException();
//                ProjectNavigationDTO dto = _mapper.Map<ProjectNavigationDTO>(project);
//                return dto;
//            }
//            catch (KeyNotFoundException)
//            {
//                throw;
//            }
//            catch (Exception ex)
//            {
//                throw new BusinessException(ex.Message);
//            }
//        }

//        public async Task<ProjectDTO> CreateProject(Guid userId, CreateProjectDTO body)
//        {
//            try
//            {
//                User user = await _userRepository.GetOneById(
//                    userId,
//                    navigateUserProjects: false
//                ) ?? throw new KeyNotFoundException("User not found");
//                await _createProjectDTOValidator.ValidateAndThrowAsync(body);
//                Project project = _mapper.Map<Project>(body);
//                await _projectValidator.ValidateAndThrowAsync(project);
//                Project newProject = await _projectRepository.InsertProject(project);
//                UserProject userProject = new UserProject
//                {
//                    UserId = userId,
//                    ProjectId = newProject.Id,
//                    Role = UserRole.ProjectOwner
//                };
//                //try
//                //{
//                //    await _userProjectService.CreateRelation(userProject);
//                //}
//                //catch (Exception ex)
//                //{
//                //    await _projectRepository.DeleteProject(newProject);
//                //    throw new BusinessException(ex.Message);
//                //}
//                if (!await _userProjectService.CreateRelation(userProject))
//                {
//                    await _projectRepository.DeleteProject(newProject);
//                    throw new BusinessException("Project creation failed");
//                }
//                ProjectDTO dto = _mapper.Map<ProjectDTO>(newProject);
//                return dto;
//            }
//            catch (Exception ex) when (
//                ex is FluentValidation.ValidationException
//            )
//            {
//                throw;
//            }
//            catch (Exception ex)
//            {
//                throw new BusinessException(ex.Message);
//            }
//        }
//    }
//}
