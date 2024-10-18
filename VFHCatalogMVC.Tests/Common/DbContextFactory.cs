using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Infrastructure;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// tworzenie DbContext z bazą i memory
    /// </summary>
    public class DbContextFactory
    {
        public static Mock<Context> Create()
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var mock = new Mock<Context>(options);

            var context = mock.Object;

            context.Database.EnsureCreated();

            context.SaveChanges();

            return mock;

        }
    }
}
