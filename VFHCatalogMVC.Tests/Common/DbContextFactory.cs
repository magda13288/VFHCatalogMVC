using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// create DbContext with database and memory
    /// </summary>
    public class DbContextFactory
    {
        public static Mock<Context> Create()
        {
            var sessionProviderMock = new Mock<ICurrentSessionProvider>();
            sessionProviderMock.Setup(x => x.GetUserId()).Returns("mockedUserId");

            //create database in memory
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var mock = new Mock<Context>(options, sessionProviderMock.Object) { CallBase = true }; //call base construct

            var context = mock.Object;

            //assures us that the database has been created
            context.Database.EnsureCreated();

            context.SaveChanges();

            return mock;

        }

        public static void Destroy(Context context)
        {
            context.Database.EnsureDeleted();
            //cleaning context
            context.Dispose();
        }
    }
}
