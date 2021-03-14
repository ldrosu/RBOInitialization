using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
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
    public class CreateRBArrayCommandHandler: ICommandHandler<CreateRBArrayCommand, CreateRBArrayResult>
    {
        private readonly ApiDbContext _context;

        public CreateRBArrayCommandHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CreateRBArrayResult> HandleAsync(CreateRBArrayCommand cmd, CancellationToken ct)
        {            
            List<InitRule> initRules = await GetInitRules(_context, cmd.GroupId);

            var initializedArray = RBArrayInitializer.Initialize(cmd.ArrayTemplate, cmd.Length, initRules);
            return new CreateRBArrayResult
            {
                InitializedArray = initializedArray
            };
        }
        
    }
}
