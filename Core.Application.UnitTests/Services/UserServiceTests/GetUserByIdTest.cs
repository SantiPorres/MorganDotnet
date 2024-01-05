using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Services.UserServices;
using AutoMapper;
using Domain.Entities;
using Domain.Options;
using FluentValidation;
using Microsoft.Extensions.Options;
using Moq;

namespace Core.Application.UnitTests.Services.UserServiceTests
{
    public class GetUserByIdTest
    {
        private readonly Mock<IValidator<User>> _mockUserValidator = new Mock<IValidator<User>>();
        private readonly Mock<IValidator<RegisterUserDTO>> _mockRegisterUserDTOValidator = new Mock<IValidator<RegisterUserDTO>>();

        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IPasswordHasher> _mockPasswordHasher = new Mock<IPasswordHasher>();

        private readonly Mock<IOptions<PaginationOptions>> _mockPaginationOptions = new Mock<IOptions<PaginationOptions>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        private readonly UserService _userService;

        public GetUserByIdTest()
        {
            _userService = new UserService(
                _mockUserValidator.Object,
                _mockRegisterUserDTOValidator.Object,
                _mockUnitOfWork.Object,
                _mockPasswordHasher.Object,
                _mockPaginationOptions.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async void GetUserById_NavigableTrue_ReturnsUserDTOWithUserProjects()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            ICollection<UserProject> userProjects = [
                new UserProject
                {
                    UserId = userId,
                    ProjectId = Guid.NewGuid(),
                    Role = Domain.Enums.UserRole.ProjectOwner
                },
                new UserProject
                {
                    UserId = userId,
                    ProjectId = Guid.NewGuid(),
                    Role = Domain.Enums.UserRole.ProjectAdmin
                },
            ];

            User user = new User
            {
                Id = userId,
                Email = "test@email.com",
                Username = "test",
                Password = "password",
                UserProjects = userProjects
            };

            UserDTO expectedUserDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                NavigatingUserProjects = true,
                UserProjects = user.UserProjects
            };

            _mockUnitOfWork.Setup(uow => uow.Users.GetWithNavigationAsync(userId))
                .ReturnsAsync(user);
            _mockMapper.Setup(mapper => mapper.Map<UserDTO>(user))
                .Returns(expectedUserDto);

            // Act
            var result = await _userService.GetUserById(userId, true);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDTO>(result);
            Assert.Equal(expectedUserDto, result);
            _mockUnitOfWork.Verify(uow => uow.Users.GetWithNavigationAsync(userId), Times.Once);
            _mockMapper.Verify(m => m.Map<UserDTO>(user), Times.Once);
        }

        [Fact]
        public async void GetUserById_NavigableFalse_ReturnsUserDTOWithoutUserProjects()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            User user = new User
            {
                Id = userId,
                Email = "test@email.com",
                Username = "test",
                Password = "password"
            };

            UserDTO expectedUserDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                NavigatingUserProjects = false
            };

            _mockUnitOfWork.Setup(uow => uow.Users.GetAsync(userId))
                .ReturnsAsync(user);
            _mockMapper.Setup(mapper => mapper.Map<UserDTO>(user))
                .Returns(expectedUserDto);

            // Act
            var result = await _userService.GetUserById(userId, false);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDTO>(result);
            Assert.Equal(expectedUserDto, result);
            _mockUnitOfWork.Verify(uow => uow.Users.GetAsync(userId), Times.Once);
            _mockMapper.Verify(m => m.Map<UserDTO>(user), Times.Once);
        }
    }
}
