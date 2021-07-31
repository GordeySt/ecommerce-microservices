using AutoMapper;
using FluentAssertions;
using Identity.Application.ApplicationUsers.Queries.GetUsersByTokenInfo;
using Identity.Application.Common.Interfaces;
using Identity.Application.Common.Mappings;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence;
using Identity.Tests.UnitTests.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Services.Common.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Tests.UnitTests.ApplicationUsers.Queries
{
    public class GetCurrentUserQueryTests
    {
        private readonly Mock<IApplicationDbContext> _dbContextStub = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceStub = new();
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public async Task ShouldNotReturnUserIfInvalidToken()
        {
            /*var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            var context = new ApplicationDbContext(options);*/
            var expectedUser = TestData.CreateAppUser();


            _dbContextStub
                .Setup(t => t.Users)
                .Returns(GetQueryableMockDbSet(expectedUser));

            var query = new GetCurrentUserQuery();

            var getCurrentUserHandler = new GetCurrentUserQueryHandler(_currentUserServiceStub.Object,
                _dbContextStub.Object, _mapper);

            var result = await getCurrentUserHandler.Handle(query, default);

            result.Result.Should().Be(ServiceResultType.Success);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet.Object;
        }
    }
}
