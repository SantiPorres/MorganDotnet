using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Services.UserServices;
using AutoMapper;
using Domain.Entities;
using Domain.Options;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Moq;

namespace Core.Application.UnitTests.Services.UserServiceTests
{
    public class AddUserTest
    {
        private readonly Mock<IValidator<User>> _mockUserValidator = new Mock<IValidator<User>>();
        private readonly Mock<IValidator<RegisterUserDTO>> _mockRegisterUserDTOValidator = new Mock<IValidator<RegisterUserDTO>>();

        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IPasswordHasher> _mockPasswordHasher = new Mock<IPasswordHasher>();

        private readonly Mock<IOptions<PaginationOptions>> _mockPaginationOptions = new Mock<IOptions<PaginationOptions>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        private readonly UserService _userService;

        public AddUserTest()
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
        public async void AddUser_ReturnUserDTO()
        {
            // Arrange
            string hashedPassword = "a1r3W31szC/GfsGXWoeA+A==;cbBro7fnClZTSF19z2qYCoXN3GHpUSH+jpDIVsDHxqg=";

            RegisterUserDTO registerUserDto = new RegisterUserDTO
            {
                Email = "test@email.com",
                Username = "test",
                Password = hashedPassword
            };

            IEnumerable<User> expectedEmailExists = [];

            User user = new User
            {
                Email = registerUserDto.Email,
                Username = registerUserDto.Username,
                Password = registerUserDto.Password
            };

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };

            UserDTO expectedUserDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                NavigatingUserProjects = false
            };

            ValidationResult validationResult = new ValidationResult();

            _mockRegisterUserDTOValidator.Setup(
                v => v.ValidateAsync(registerUserDto, It.IsAny<CancellationToken>())
            )
                .ReturnsAsync(validationResult);

            _mockUnitOfWork.Setup(uow => uow.Users.FindAsync(user => user.Email == registerUserDto.Email))
                .ReturnsAsync(expectedEmailExists);

            _mockMapper.Setup(m => m.Map<User>(registerUserDto))
                .Returns(user);

            _mockUserValidator.Setup(
                v => v.ValidateAsync(user, It.IsAny<CancellationToken>())
            )
                .ReturnsAsync(validationResult);

            _mockPasswordHasher.Setup(p => p.Hash(user.Password))
                .Returns(hashedPassword);

            _mockUnitOfWork.Setup(uow => uow.Users.AddAndGetAsync(user))
                .ReturnsAsync(newUser);

            _mockUnitOfWork.Setup(uow => uow.Complete())
                .Verifiable();

            _mockMapper.Setup(m => m.Map<UserDTO>(newUser))
                .Returns(expectedUserDto);

            // Act
            var result = await _userService.AddUser(registerUserDto);

            // Assert
            Assert.Equal(expectedUserDto, result);
            _mockRegisterUserDTOValidator.Verify(
                v => v.ValidateAsync(registerUserDto, It.IsAny<CancellationToken>()), Times.Once
            );
            _mockUnitOfWork.Verify(
                uow => uow.Users.FindAsync(user => user.Email == registerUserDto.Email), Times.Once
            );
            _mockMapper.Verify(m => m.Map<User>(registerUserDto), Times.Once);
            _mockUserValidator.Verify(
                v => v.ValidateAsync(user, It.IsAny<CancellationToken>()), Times.Once
            );
            _mockPasswordHasher.Verify(p => p.Hash(user.Password), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Users.AddAndGetAsync(user), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.Complete(), Times.Once);
            _mockMapper.Verify(m => m.Map<UserDTO>(newUser), Times.Once);
        }
    }
}
