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
    public class UpdateParameterCommandHandler : ICommandHandler<UpdateParameterCommand>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;

        public UpdateParameterCommandHandler(ApiDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task HandleAsync(UpdateParameterCommand cmd, CancellationToken ct)
        {
            var parameters = _context.Set<ParameterEntity>();
            var parameter = await parameters.SingleOrDefaultAsync(p => p.ExternalId == cmd.ExternalId, ct);
            
            if (parameter == null)
            {
                throw new NotFoundException($"Rule '{cmd.ExternalId}' not found");
            }
            
            if (parameter.Rule.ExternalId != cmd.RuleId)
            {
                throw new ConflictException($"Rule group '{parameter.Rule.ExternalId}' cannot be changed to '{cmd.RuleId}'");
            }
            
            if(parameter.Index == cmd.Index &&
                parameter.Value == cmd.Value)
            {
                //No change
                return;
            }
            if (parameter.Index != cmd.Index)
            {
                if (await parameters.Where(r => r.Index == cmd.Index).Select(r => r).CountAsync() > 0)
                {
                    throw new ConflictException($"Parameter with index '{cmd.Index}' aready exists");
                }
                parameter.Index = cmd.Index;
            }

            parameter.Value = cmd.Value;
            await _context.SaveChangesAsync(ct);
        }
    }
}
