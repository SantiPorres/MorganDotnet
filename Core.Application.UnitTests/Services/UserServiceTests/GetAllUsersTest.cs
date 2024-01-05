using Application.DTOs.AccountDTOs;
using Application.DTOs.UserDTOs;
using Application.Filters;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Services.UserServices;
using AutoMapper;
using Domain.CustomEntities;
using Domain.Entities;
using Domain.Options;
using FluentValidation;
using Microsoft.Extensions.Options;
using Moq;

namespace Core.Application.UnitTests.Services.UserServiceTests
{
    public class GetAllUsersTest
    {
        private readonly Mock<IValidator<User>> _mockUserValidator = new Mock<IValidator<User>>();
        private readonly Mock<IValidator<RegisterUserDTO>> _mockRegisterUserDTOValidator = new Mock<IValidator<RegisterUserDTO>>();

        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IPasswordHasher> _mockPasswordHasher = new Mock<IPasswordHasher>();

        private readonly Mock<IOptions<PaginationOptions>> _mockPaginationOptions = new Mock<IOptions<PaginationOptions>>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        private readonly UserService _userService;

        public GetAllUsersTest()
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
        public async Task GetAllUsers_ReturnPagedList()
        {
            // Arrange
            IEnumerable<User> users = [
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "test@email.com",
                    Username = "Test",
                    Password = "password"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "test2@email.com",
                    Username = "Test2",
                    Password = "password"
                },
            ];
            IEnumerable<UserDTO> usersDto = [
                new UserDTO
                {
                    Id = (users.ToList())[0].Id,
                    Email = (users.ToList())[0].Email,
                    Username = (users.ToList())[0].Username
                },
                new UserDTO
                {
                    Id = (users.ToList())[1].Id,
                    Email = (users.ToList())[1].Email,
                    Username = (users.ToList())[1].Username
                },
            ];
            PaginationQueryParameters filters = new PaginationQueryParameters { PageNumber = 1, PageSize = 10 };
            PagedList<UserDTO> expectedPagedList = PagedList<UserDTO>.Create(
                usersDto,
                1,
                10
            );

            _mockUnitOfWork.Setup(uow => uow.Users.GetAllAsync())
                .ReturnsAsync(users);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<UserDTO>>(users))
                .Returns(usersDto);

            // Act
            var result = await _userService.GetAllUsers(filters);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PagedList<UserDTO>>(result);
            Assert.Equal(expectedPagedList, result);
        }
    }
}
