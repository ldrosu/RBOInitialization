using System;
using System.Text.Json;
using RBOLib;

namespace LocalApp
{
    class Program
    {
        static void Main(string[] args)
        {           
            try
            {           
                var RBO = new RBOInit();
                RBO.AddRule("Id", SourceTypeEnum.Sequence);
                RBO.AddRule("Name", SourceTypeEnum.Array, "Paul", "John");
                RBO.AddRule("Pets.Length", SourceTypeEnum.Value, 3);
                RBO.AddRule("Pets[].Name", SourceTypeEnum.Array, "Rocky", "Coco");
                RBO.AddRule("Species", SourceTypeEnum.Array, "Dog", "Cat", "Fish");
                RBO.AddRule("Age", SourceTypeEnum.Random, 1, 10);
                RBO.AddRule("IsTrained", SourceTypeEnum.Array, true, false);
                var owners = RBO.CreateArray<Owner>(2);
                
                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                var json = JsonSerializer.Serialize(owners, options);
                Console.WriteLine(json);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
