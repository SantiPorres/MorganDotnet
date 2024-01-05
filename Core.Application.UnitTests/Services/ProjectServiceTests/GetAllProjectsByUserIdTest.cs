//using Application.DTOs.ProjectDTOs;
//using Application.DTOs.UserDTOs;
//using Application.Filters;
//using Application.Interfaces;
//using Application.Interfaces.IServices;
//using Application.Services.ProjectServices;
//using AutoMapper;
//using Domain.CustomEntities;
//using Domain.Entities;
//using Domain.Options;
//using FluentValidation;
//using FluentValidation.Results;
//using Microsoft.Extensions.Options;
//using Moq;

//namespace Core.Application.UnitTests.Services.ProjectServiceTests
//{
//    public class GetAllProjectsByUserIdTest
//    {
//        private readonly Mock<IValidator<Project>> _mockProjectValidator = new Mock<IValidator<Project>>();
//        private readonly Mock<IValidator<CreateProjectDTO>> _mockCreateProjectDTOValidator = new Mock<IValidator<CreateProjectDTO>>();
//        private readonly Mock<IValidator<PaginationQueryParameters>> _mockPaginationQueryParametersValidator = new Mock<IValidator<PaginationQueryParameters>>();

//        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
//        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
//        private readonly Mock<IUserProjectService> _mockUserProjectService = new Mock<IUserProjectService>();

//        private readonly Mock<IOptions<PaginationOptions>> _mockPaginationOptions = new Mock<IOptions<PaginationOptions>>();
//        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

//        private readonly ProjectService _projectService;

//        public GetAllProjectsByUserIdTest()
//        {
//            _projectService = new ProjectService(
//                _mockProjectValidator.Object,
//                _mockCreateProjectDTOValidator.Object,
//                _mockPaginationQueryParametersValidator.Object,

//                _mockUnitOfWork.Object,
//                _mockUserService.Object,
//                _mockUserProjectService.Object,

//                _mockPaginationOptions.Object,
//                _mockMapper.Object
//            );
//        }

//        [Fact]
//        public async void GetAllProjectsByUserId_ReturnPagedListOfProjectDTO()
//        {
//            // Arrange
//            Mock<IProjectService> _mockProjectService = new Mock<IProjectService>();

//            Guid userId = Guid.NewGuid();
//            Guid projectId1 = Guid.NewGuid(); 
//            Guid projectId2 = Guid.NewGuid();

//            PaginationQueryParameters filters = new PaginationQueryParameters
//            {
//                PageSize = 10,
//                PageNumber = 1
//            };

//            ProjectDTO projectDto1 = new ProjectDTO
//            {
//                Id = projectId1,
//                Name = "Project 1",
//                Description = null
//            };

//            ProjectDTO projectDto2 = new ProjectDTO
//            {
//                Id = projectId2,
//                Name = "Project 2",
//                Description = "The description"
//            };

//            ICollection<UserProject> userProjects = [
//                new UserProject
//                {
//                    Id = Guid.NewGuid(),
//                    UserId = userId,
//                    ProjectId = projectId1,
//                    Role = Domain.Enums.UserRole.ProjectOwner
//                },
//                new UserProject
//                {
//                    Id = Guid.NewGuid(),
//                    UserId = userId,
//                    ProjectId = projectId2,
//                    Role = Domain.Enums.UserRole.ProjectAdmin
//                },
//            ];

//            ICollection<ProjectDTO> projectsDTOs = [
//                new ProjectDTO
//                {
//                    Id = projectId1,
//                    Name = "Test Project",
//                    Description = null
//                },
//                new ProjectDTO
//                {
//                    Id = projectId2,
//                    Name = "Test Project 2",
//                    Description = null
//                }
//            ];

//            UserDTO userDto = new UserDTO
//            {
//                Id = userId,
//                Email = "test@email.com",
//                Username = "test",
//                UserProjects = userProjects
//            };

//            PagedList<ProjectDTO> expectedPagedList = PagedList<ProjectDTO>.Create(
//                projectsDTOs,
//                (int)filters.PageNumber,
//                (int)filters.PageSize
//            );

//            _mockPaginationQueryParametersValidator.Setup(
//                v => v.ValidateAsync(filters, It.IsAny<CancellationToken>())
//            ).ReturnsAsync(new ValidationResult());

//            _mockUserService.Setup(
//                s => s.GetUserById(userId, true)
//            ).ReturnsAsync(userDto);

//            _mockProjectService.Setup(
//                s => s.GetProjectById(projectId1, false)
//            ).ReturnsAsync(projectDto1);

//            _mockProjectService.Setup(
//                s => s.GetProjectById(projectId2, false)
//            ).ReturnsAsync(projectDto2);

//            //_mockProjectService.Setup(
//            //    s => s.GetProjectById(It.IsAny<Guid>(), false)
//            //).Returns<Guid, bool>((projectId, _) => Task.FromResult(
//            //    new Project
//            //    {
//            //        Id = projectId,
//            //        Name = projectId.ToString(),
//            //        Description = null
//            //    }
//            //));

//            // Act
//            var result = await _projectService.GetAllProjectsByUserId(filters, userId);

//            // Assert
//            Assert.NotNull(result);
//            Assert.NotEmpty(result);
//        }
//    }
//}
