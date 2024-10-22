using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Infrastructure;
using Xunit;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// odpowiada za to, aby xUnit był w stanie dzielic ten kontekst, który stworzymy z innymi testami i klasami. Tworzymy konteskt raz i wszystkie testy korzystają z niego 
    /// </summary>
    public class QueryTestFixtures : IDisposable
    {
        public Context Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixtures()
        {
            Context = DbContextFactory.Create().Object;
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(Context);
        }
        //xUnit ma wiedzieć, że ma współdzielić tą bazę/kolekję pomiędzy róznymi klasami testowymi, nie będzie tworzona na nowo, wszystko będzie na commandach(dodawanie,usuwanie, edycja)
        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixtures>
        { }
    }
}
