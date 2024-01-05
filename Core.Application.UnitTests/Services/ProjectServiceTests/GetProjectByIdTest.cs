using Application.DTOs.ProjectDTOs;
using Application.Filters;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Services.ProjectServices;
using AutoMapper;
using Domain.Entities;
using Domain.Options;
using FluentValidation;
using Microsoft.Extensions.Options;
using Moq;

namespace Core.Application.UnitTests.Services.ProjectServiceTests
{
    public class GetProjectByIdTest
    {
        private readonly Mock<IValidator<Project>> _mockProjectValidator = new Mock<IValidator<Project>>();
        private readonly Mock<IValidator<CreateProjectDTO>> _mockCreateProjectDTOValidator = new Mock<IValidator<CreateProjectDTO>>();
        private readonly Mock<IValidator<PaginationQueryParameters>> _mockPaginationQueryParametersValidator = new Mock<IValidator<PaginationQueryParameters>>();

        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IUserProjectService> _mockUserProjectService = new Mock<IUserProjectService>();

        private readonly Mock<IOptions<PaginationOptions>> _mockPaginationOptions = new Mock<IOptions<PaginationOptions>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        private readonly ProjectService _projectService;

        public GetProjectByIdTest()
        {
            _projectService = new ProjectService(
                _mockProjectValidator.Object,
                _mockCreateProjectDTOValidator.Object,
                _mockPaginationQueryParametersValidator.Object,

                _mockUnitOfWork.Object,
                _mockUserService.Object,
                _mockUserProjectService.Object,

                _mockPaginationOptions.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async void GetProjectById_NavigableTrue_WhenProjectFound_ShouldReturnDTO()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();
            ICollection<UserProject> projectUsers = [
                new UserProject
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    UserId = Guid.NewGuid(),
                    Role = Domain.Enums.UserRole.ProjectOwner
                }
            ];
            var project = new Project
            {
                Id = projectId,
                Name = "College project",
                Description = "The desc",
                ProjectUsers = projectUsers,
            };
            var expectedDto = new ProjectDTO
            {
                Id = projectId,
                Name = project.Name,
                Description = project.Description,
                ProjectUsers = project.ProjectUsers,
            };

            _mockUnitOfWork.Setup(uow => uow.Projects.GetWithNavigationAsync(projectId))
                .ReturnsAsync(project);
            _mockMapper.Setup(mapper => mapper.Map<ProjectDTO>(project))
                .Returns(expectedDto);

            // Act
            var result = await _projectService.GetProjectById(projectId, navigable: true);

            // Assert
            Assert.Equal(expectedDto, result);
            _mockUnitOfWork.Verify(uow => uow.Projects.GetWithNavigationAsync(projectId), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<ProjectDTO>(project), Times.Once);
        }

        [Fact]
        public async void GetProjectById_NavigableFalse_WhenProjectFound_ShouldReturnDTO()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();
            var project = new Project
            {
                Id = projectId,
                Name = "College project",
                Description = "The desc"
            };
            var expectedDto = new ProjectDTO
            {
                Id = projectId,
                Name = project.Name,
                Description = project.Description
            };

            _mockUnitOfWork.Setup(uow => uow.Projects.GetAsync(projectId))
                .ReturnsAsync(project);
            _mockMapper.Setup(mapper => mapper.Map<ProjectDTO>(project))
                .Returns(expectedDto);

            // Act
            var result = await _projectService.GetProjectById(projectId, navigable: false);

            // Assert
            Assert.Equal(expectedDto, result);
            _mockUnitOfWork.Verify(uow => uow.Projects.GetAsync(projectId), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<ProjectDTO>(project), Times.Once);
        }

        [Fact]
        public async void GetProjectById_NavigableTrue_WhenProjectNotFound_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.Projects.GetWithNavigationAsync(projectId))
                .ThrowsAsync(new KeyNotFoundException());

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _projectService.GetProjectById(projectId, navigable: true));
            _mockUnitOfWork.Verify(uow => uow.Projects.GetWithNavigationAsync(projectId), Times.Once);
        }

        [Fact]
        public async void GetProjectById_NavigableFalse_WhenProjectNotFound_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.Projects.GetAsync(projectId))
                .ThrowsAsync(new KeyNotFoundException());

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _projectService.GetProjectById(projectId, navigable: false));
            _mockUnitOfWork.Verify(uow => uow.Projects.GetAsync(projectId), Times.Once);
        }
    }
}
