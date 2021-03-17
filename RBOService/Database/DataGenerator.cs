using RBOService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Database
{
    public class DataGenerator
    {
        public static void Initialize(ApiDbContext context)
        {
            var users = context.Set<UserEntity>();
            if (users.Any())
            {
                return;
            }
            var basicUser = new UserEntity
            {
                Id = 1,
                Username = "user",
                Password = "pass",
                
            };
            users.Add(basicUser);
            
            context.SaveChanges();
        }

    }
}
