using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VFHCatalogMVC.Infrastructure;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// baza dla wszystkich testów, wszystkich commandów
    /// </summary>
    public class CommandTestBase : IDisposable
    {
        protected readonly Context _context;
        protected readonly Mock<Context> _contextMock;

        public CommandTestBase()
        {
            _contextMock = DbContextFactory.Create();
            _context = _contextMock.Object;
        }
        public void Dispose()
        {
            DbContextFactory.Destroy(_context);
        }
    }
}
