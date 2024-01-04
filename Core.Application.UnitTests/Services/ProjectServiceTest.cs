//using Application.DTOs.ProjectDTOs;
//using Application.Interfaces.IRepositories;
//using Application.Interfaces.IServices;
//using Application.Interfaces.ProjectInterfaces;
//using Application.Interfaces.UserInterfaces;
//using Application.Interfaces.UserProjectInterfaces;
//using Application.Services.ProjectServices;
//using AutoMapper;
//using Domain.Entities;
//using Domain.Options;
//using FluentValidation;
//using Microsoft.Extensions.Options;
//using Moq;

//namespace Core.Application.UnitTests.Services
//{
//    public class ProjectServiceTest
//    {
//        private readonly Mock<IProjectRepository> _mockProjectRepository = new Mock<IProjectRepository>();
//        private readonly Mock<IValidator<Project>> _mockProjectValidator = new Mock<IValidator<Project>>();
//        private readonly Mock<IValidator<CreateProjectDTO>> _mockCreateProjectDTOValidator = new Mock<IValidator<CreateProjectDTO>>();
//        private readonly Mock<IOptions<PaginationOptions>> _mockPaginationOptions = new Mock<IOptions<PaginationOptions>>();
//        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
//        private readonly Mock<IUserProjectService> _mockUserProjectService = new Mock<IUserProjectService>();
//        private readonly Mock<IUserRepository> _mockUserRepository = new Mock<IUserRepository>();

//        private readonly ProjectService _projectService;

//        public ProjectServiceTest()
//        {
//            _projectService = new ProjectService(
//                _mockProjectRepository.Object,
//                _mockProjectValidator.Object,
//                _mockCreateProjectDTOValidator.Object,
//                _mockPaginationOptions.Object,
//                _mockMapper.Object,
//                _mockUserProjectService.Object,
//                _mockUserRepository.Object
//            );
//        }

//        [Fact]
//        public async void GetProjectById_WhenProjectFound_ShouldReturnDTO()
//        {
//            // Arrange
//            Guid projectId = Guid.NewGuid();
//            ICollection<UserProject> projectUsers = [
//                new UserProject
//                {
//                    Id = Guid.NewGuid(),
//                    ProjectId = projectId,
//                    UserId = Guid.NewGuid(),
//                    Role = Domain.Enums.UserRole.ProjectOwner
//                }
//            ];
//            var project = new Project
//            {
//                Id = projectId,
//                Name = "College project",
//                Description = "The desc",
//                ProjectUsers = projectUsers,
//            };
//            var expectedDto = new ProjectDTO
//            {
//                Id = projectId,
//                Name = project.Name,
//                Description = project.Description,
//                ProjectUsers = project.ProjectUsers,
//            };

//            _mockProjectRepository.Setup(repo => repo.GetProjectById(projectId, true))
//                .ReturnsAsync(project);
//            _mockMapper.Setup(mapper => mapper.Map<ProjectDTO>(project))
//                .Returns(expectedDto);

//            // Act
//            var result = await _projectService.GetProjectById(projectId);

//            // Assert
//            Assert.Equal(expectedDto, result);
//            _mockProjectRepository.Verify(repo => repo.GetProjectById(projectId, true), Times.Once);
//            _mockMapper.Verify(mapper => mapper.Map<ProjectDTO>(project), Times.Once);
//        }

//        [Fact]
//        public async void GetProjectById_WhenProjectNotFound_ShouldThrowKeyNotFoundException()
//        {
//            // Arrange
//            Guid projectId = Guid.NewGuid();

//            _mockProjectRepository.Setup(repo => repo.GetProjectById(projectId, true))
//                .ReturnsAsync(null as Project);

//            // Act & Assert
//            await Assert.ThrowsAsync<KeyNotFoundException>(() => _projectService.GetProjectById(projectId));
//            _mockProjectRepository.Verify(repo => repo.GetProjectById(projectId, true), Times.Once);
//        }

//    }
//}
