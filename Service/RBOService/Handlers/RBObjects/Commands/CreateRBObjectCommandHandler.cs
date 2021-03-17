using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Initializations;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static RBOService.Handlers.RBObjects.Commands.CommandHelper;


namespace RBOService.Handlers.RBObjects
{
    public class CreateRBObjectCommandHandler : ICommandHandler<CreateRBObjectCommand, CreateRBObjectResult>
    {
        private readonly ApiDbContext _context;

        public CreateRBObjectCommandHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CreateRBObjectResult> HandleAsync(CreateRBObjectCommand cmd, CancellationToken ct)
        {
            List<InitRule> initRules = await GetInitRules(_context, cmd.GroupId);
            var initializedObject = RBObjectInitializer.Initialize(cmd.ObjectTemplate, initRules);
           
            return new CreateRBObjectResult
            {
                InitializedObject = initializedObject
            };
        }
    }
}
