using CampaignPubTest.MockData.DataMock;
using Core.Models;
using Data.Repositories;
using Data.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CampaignPubTest.DataTests.RepositoriesTests
{
    public class ProductTypeRepositoryTest
    {
        [Fact]
        public void GetproductTypeInListIdsTest()
        {
            // arrange
            var context = CreateDbContext();

            var repository = new ProductTypeRepository(context.Object);

            // act
            var results = repository.GetAll();

            var count = results.Count();

            // assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);

            Assert.False(count > 3);
        }


        private Mock<MyDataBaseContext> CreateDbContext()
        {
            var productTypes = ProductTypeMockData.GetAllProductTypes().AsQueryable();

            var dbSet = new Mock<DbSet<ProductType>>();

            dbSet.As<IQueryable<ProductType>>().Setup(m => m.Provider).Returns(productTypes.Provider);
            dbSet.As<IQueryable<ProductType>>().Setup(m => m.Expression).Returns(productTypes.Expression);
            dbSet.As<IQueryable<ProductType>>().Setup(m => m.ElementType).Returns(productTypes.ElementType);
            dbSet.As<IQueryable<ProductType>>().Setup(m => m.GetEnumerator()).Returns(productTypes.GetEnumerator());

            var context = new Mock<MyDataBaseContext>();
          //  context.Setup(c => c.ProductTypes);
                //.Returns(dbSet.Object);
            return context;
        }
    }
}
