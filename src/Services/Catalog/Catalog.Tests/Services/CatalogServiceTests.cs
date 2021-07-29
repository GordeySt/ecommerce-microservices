using AutoMapper;
using Catalog.API.BL.Services;
using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Enums;
using Catalog.API.DAL.Interfaces;
using Catalog.API.PL.Controllers;
using Catalog.API.PL.Models.DTOs.Products;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Common.Enums;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Tests.Services
{
    public class CatalogServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryStub = new();
        private readonly Mock<IMapper> _mapperStub = new();
        private readonly Random _rand = new();

        [Fact]
        public async Task AddProductAsync_WithExistingProduct_ReturnsSuccessfulResultWithCreatedProduct()
        {
            // Arrange
            var productToCreate = CreateCreateProductDto();
            var productEntity = CreateProductEntity();

            var catalogService = new CatalogService(_repositoryStub.Object, _mapperStub.Object);

            // Act
            var creationResult = await catalogService.AddProductAsync(productToCreate);

            // Assert
            productToCreate.Should().BeEquivalentTo(creationResult.Data);
            creationResult.Data.Id.Should().NotBeEmpty();
            creationResult.Result.Should().Be(ServiceResultType.Success);
        }

        private CreateProductDto CreateCreateProductDto() => new()
        {
            Category = "TestCategory",
            AgeRating = AgeRating.AboveThree,
            Count = 10,
            Description = "TestDescription",
            Name = "TestName",
            Summary = "TestSummary",
            Price = 10
        };

        private Product CreateProductEntity() => new()
        {
            Id = Guid.NewGuid(),
            Category = "TestCategory",
            AgeRating = AgeRating.AboveThree,
            Count = 10,
            Description = "TestDescription",
            Name = "TestName",
            Summary = "TestSummary",
            Price = 10,
            AverageRating = 0,
            TotalRating = 0
        };
    }
}
