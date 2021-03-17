using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace RBOService.Test
{
    [CollectionDefinition("RBO Integration Tests")]
    public class RBOTestCollection : ICollectionFixture<WebApplicationFactory<RBOService.Startup>>
    {
        //No code
    }
}
