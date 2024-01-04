//using Application.DTOs.AccountDTOs;
//using Application.DTOs.UserDTOs;
//using Application.Filters;
//using Application.Interfaces;
//using Application.Interfaces.IServices;
//using Application.Services.UserServices;
//using AutoMapper;
//using Domain.CustomEntities;
//using Domain.Entities;
//using Domain.Options;
//using FluentValidation;
//using Microsoft.Extensions.Options;
//using Moq;

//namespace Core.Application.UnitTests.Services
//{
//    public class UserServiceTest
//    {
//        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
//        private readonly Mock<IValidator<User>> _mockUserValidator = new Mock<IValidator<User>>();
//        private readonly Mock<IValidator<RegisterUserDTO>> _mockRegisterUserDTOValidator = new Mock<IValidator<RegisterUserDTO>>();
//        private readonly Mock<IPasswordHasher> _mockPasswordHasher = new Mock<IPasswordHasher>();
//        private readonly Mock<IOptions<PaginationOptions>> _mockPaginationOptions = new Mock<IOptions<PaginationOptions>>();
//        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

//        private readonly UserService _userService;

//        public UserServiceTest()
//        {
//            _userService = new UserService(
//                _mockUserValidator.Object,
//                _mockRegisterUserDTOValidator.Object,
//                _mockPaginationOptions.Object,
//                _mockMapper.Object,
//                _mockPasswordHasher.Object,
//                _mockUnitOfWork.Object
//            );
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task GetAllUsers_ReturnPagedList ()
//        {
//            // Arrange
//            IEnumerable<User> users = [
//                new User
//                {
//                    Id = Guid.NewGuid(),
//                    Email = "test@email.com",
//                    Username = "Test",
//                    Password = "password"
//                },
//                new User
//                {
//                    Id = Guid.NewGuid(),
//                    Email = "test2@email.com",
//                    Username = "Test2",
//                    Password = "password"
//                },
//            ];
//            IEnumerable<UserDTO> usersDto = [
//                new UserDTO
//                {
//                    Id = (users.ToList())[0].Id,
//                    Email = (users.ToList())[0].Email,
//                    Username = (users.ToList())[0].Username
//                },
//                new UserDTO
//                {
//                    Id = (users.ToList())[1].Id,
//                    Email = (users.ToList())[1].Email,
//                    Username = (users.ToList())[1].Username
//                },
//            ];
//            PaginationQueryParameters filters = new PaginationQueryParameters { PageNumber = 1, PageSize = 10 };
//            PagedList<UserDTO> expectedPagedList = PagedList<UserDTO>.Create(
//                usersDto,
//                1,
//                10
//            );

//            _mockUnitOfWork.Setup(uow => uow.Users.GetAllAsync())
//                .ReturnsAsync(users);
//            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<UserDTO>>(users))
//                .Returns(usersDto);

//            // Act
//            var result = await _userService.GetAllUsers(filters);

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsType<PagedList<UserDTO>>(result);
//            Assert.Equal(expectedPagedList, result);
//        }

//        [Fact]
//        public async void GetUserById_ReturnUserNavigationDTO()
//        {
//            // Arrange
//            Guid userId = Guid.NewGuid();

//            ICollection<UserProject> userProjects = [
//                new UserProject
//                {
//                    UserId = userId,
//                    ProjectId = Guid.NewGuid(),
//                    Role = Domain.Enums.UserRole.ProjectOwner
//                },
//                new UserProject
//                {
//                    UserId = userId,
//                    ProjectId = Guid.NewGuid(),
//                    Role = Domain.Enums.UserRole.ProjectAdmin
//                },
//            ];

//            User user = new User
//            {
//                Id = userId,
//                Email = "test@email.com",
//                Username = "test",
//                Password = "password",
//                UserProjects = userProjects
//            };

//            UserNavigationDTO userDto = new UserNavigationDTO
//            {
//                Id = user.Id,
//                Email = user.Email,
//                Username = user.Username,
//                UserProjects = user.UserProjects
//            };

//            _mockUnitOfWork.Setup(uow => uow.Users.GetWithNavigationAsync(userId))
//                .ReturnsAsync(user);
//            _mockMapper.Setup(mapper => mapper.Map<UserNavigationDTO>(user))
//                .Returns(userDto);

//            // Act
//            var result = await _userService.GetUserById(userId);

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsType<UserNavigationDTO>(result);
//            Assert.Equal(userDto, result);
//        }

//        //[Fact]
//        //public void AddUser_ReturnUserDTO()
//        //{
//        //    RegisterUserDTO registerUserDto = new RegisterUserDTO
//        //    {
//        //        Email = "test@email.com",
//        //        Username = "test",
//        //        Password = "password"
//        //    };

//        //    _mockRegisterUserDTOValidator.Setup(v => v.ValidateAndThrowAsync(registerUserDto))
//        //        .Verifiable();
//        //}
//    }
//}
