using AutoMapper;
using Catalog.API.BL.Mappings;
using Catalog.API.BL.Services;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.Tests.Shared.Services;
using FluentAssertions;
using Moq;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Tests.Services
{
    public class UsersServiceTest
    {
        private readonly Mock<IUsersRepository> _repositoryStub = new();
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public UsersServiceTest()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public async Task AddUserAsync_WithExistingUser_ReturnsBadRequestServiceResult()
        {
            // Arrange
            var userModel = UsersServiceTestData.CreateAppUserModel();
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var expectedServiceResult = new ServiceResult<User>(ServiceResultType.BadRequest);

            _repositoryStub
                .Setup(t => t.GetUserByIdAsync(new Guid(userModel.Id), true))
                .ReturnsAsync(userEntity);

            var usersService = new UsersService(_repositoryStub.Object, _mapper);

            // Act
            var creationgResult = await usersService.AddUserAsync(userModel);

            // Assert
            creationgResult.Result.Should().Be(ServiceResultType.BadRequest);
        }

        [Fact]
        public async Task AddUserAsync_WithUnexistingUser_ReturnsSuccessfulServiceResultWithCreatedUser()
        {
            // Arrange
            var userModel = UsersServiceTestData.CreateAppUserModel();
            var userEntity = UsersServiceTestData.CreateUserEntity();
            var expectedServiceResult = new ServiceResult<User>(ServiceResultType.Success,
                userEntity);

            _repositoryStub
                .Setup(t => t.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(expectedServiceResult);

            var usersService = new UsersService(_repositoryStub.Object, _mapper);

            // Act
            var creationResult = await usersService.AddUserAsync(userModel);

            // Assert
            creationResult.Data.Id.Should().NotBeEmpty();
            creationResult.Result.Should().Be(ServiceResultType.Success);

            _repositoryStub.Verify(x => x.AddAsync(It.IsAny<User>()));
        }

        [Fact]
        public async Task GetUserByIdAsync_WithUnexistingUser_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _repositoryStub
                .Setup(t => t.GetUserByIdAsync(It.IsAny<Guid>(), false))
                .ReturnsAsync((User)null);

            var usersService = new UsersService(_repositoryStub.Object, _mapper);

            // Act
            var user = await usersService.GetUserByIdAsync(userId);

            // Assert
            user.Should().BeNull();

            _repositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), false));
        }

        [Fact]
        public async Task GetProductByIdAsync_WithUnexistingUser_ReturnsExpectedProductDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = UsersServiceTestData.CreateUserEntity();
            var expectedUserDto = UsersServiceTestData.CreateUserDto();

            _repositoryStub
                .Setup(t => t.GetUserByIdAsync(It.IsAny<Guid>(), false))
                .ReturnsAsync(expectedUser);

            var usersService = new UsersService(_repositoryStub.Object, _mapper);

            // Act
            var user = await usersService.GetUserByIdAsync(userId);

            // Assert
            user.Should().BeEquivalentTo(expectedUserDto);

            _repositoryStub.Verify(x => x.GetUserByIdAsync(It.IsAny<Guid>(), false));
        }
    }
}
