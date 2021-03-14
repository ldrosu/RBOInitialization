using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Parameters.Commands
{
    
    public class DeleteParameterCommandHandler : ICommandHandler<DeleteParameterCommand>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;

        public DeleteParameterCommandHandler(ApiDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task HandleAsync(DeleteParameterCommand cmd, CancellationToken ct)
        {
            var rules = _context.Set<RuleEntity>();

            //If group does not exist throw exception
            //A rule must belong to a group

            var rule = await rules.Where(r => r.ExternalId == cmd.ParameterId).Select(g => g).FirstOrDefaultAsync();
            if (rule == null)
            {
                throw new NotFoundException($"Rule '{cmd.ParameterId}' not found");
            }            
            rules.Remove(rule);
            await _context.SaveChangesAsync();
        }
    }
}
