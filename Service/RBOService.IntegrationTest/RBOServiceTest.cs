using System;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using RBOService.Controllers.Groups;
using RBOClientLib;
using System.Collections.Generic;
using System.Linq;

namespace RBOService.Test
{
    [Collection("RBO Integration Tests")]
    public class RBOServiceTest
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public RBOServiceTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "<Pending>")]
        public void TestCreateObject()
        {
            var client = _factory.CreateClient();
            RBOInitClient RBO = new RBOInitClient(client);
            RBO.Login("user", "pass");
            RBO.AddRule("Id", SourceTypeEnum.Sequence);
            RBO.AddRule("Name", SourceTypeEnum.Array, "Paul", "John");
            RBO.AddRule("Pets.Length", SourceTypeEnum.Value, 5);
            RBO.AddRule("Pets[].Name", SourceTypeEnum.Array, "Rocky", "Coco");
            RBO.AddRule("Species", SourceTypeEnum.Array, "Dog", "Cat", "Fish");
            RBO.AddRule("Age", SourceTypeEnum.Random, 1, 10);
            RBO.AddRule("IsTrained", SourceTypeEnum.Array, true, false);

            var owner = RBO.Create<Owner>();
            Assert.True(new List<string> { "Paul", "John" }.Contains(owner.Name));
            Assert.Equal(5, owner.Pets.Length);
            for (var i = 0; i < owner.Pets.Length; i++)
            {
                var pet = owner.Pets[i];
                Assert.True(new List<string> { "Rocky", "Coco" }.Contains(pet.Name));
                Assert.True(new List<string> { "Dog", "Cat", "Fish" }.Contains(pet.Species));
                Assert.InRange<int>(pet.Age, 1, 10);
            }
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "<Pending>")]
        public void TestCreateArray()
        {
            var client = _factory.CreateClient();
            RBOInitClient RBO = new RBOInitClient(client);
            RBO.Login("user", "pass");
            RBO.AddRule("Id", SourceTypeEnum.Sequence);
            RBO.AddRule("Name", SourceTypeEnum.Array, "Paul", "John");
            RBO.AddRule("Pets.Length", SourceTypeEnum.Value, 5);
            RBO.AddRule("Pets[].Name", SourceTypeEnum.Array, "Rocky", "Coco");
            RBO.AddRule("Species", SourceTypeEnum.Array, "Dog", "Cat", "Fish");
            RBO.AddRule("Age", SourceTypeEnum.Random, 1, 10);
            RBO.AddRule("IsTrained", SourceTypeEnum.Array, true, false);

            var owners = RBO.CreateArray<Owner>(10);
            Assert.Equal(10, owners.Length);
            for (int j = 0; j < owners.Length; j++)
            {
                var owner = owners[j];
                Assert.True(new List<string> { "Paul", "John" }.Contains(owner.Name));
                Assert.Equal(5, owner.Pets.Length);
                for (var i = 0; i < owner.Pets.Length; i++)
                {
                    var pet = owner.Pets[i];
                    Assert.True(new List<string> { "Rocky", "Coco" }.Contains(pet.Name));
                    Assert.True(new List<string> { "Dog", "Cat", "Fish" }.Contains(pet.Species));
                    Assert.InRange<int>(pet.Age, 1, 10);
                }
            }
        }        
    }

    class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public bool IsTrained { get; set; }
    }

    class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Pet[] Pets { get; set; }
    }
}
