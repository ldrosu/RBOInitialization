using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Rules.Commands
{
    public class UpdateRuleCommandHandler : ICommandHandler<UpdateRuleCommand>
    {
        private readonly ApiDbContext _context;

        public UpdateRuleCommandHandler(ApiDbContext context)
        {
            _context = context;
        }
        public async Task HandleAsync(UpdateRuleCommand cmd, CancellationToken ct)
        {
            var rules = _context.Set<RuleEntity>();
            var rule = await rules.SingleOrDefaultAsync(p => p.ExternalId == cmd.ExternalId, ct);
            
            if (rule == null)
            {
                throw new NotFoundException($"Rule '{cmd.ExternalId}' not found");
            }
            
            if (rule.Group.ExternalId != cmd.GroupId)
            {
                throw new ConflictException($"Rule group '{rule.Group.ExternalId}' cannot be changed to '{cmd.GroupId}'");
            }
            
            if(rule.Index == cmd.Index &&
                rule.Pattern == cmd.Pattern &&
                rule.SourceType == cmd.SourceType &&
                rule.DestinationType == cmd.DestinationType)
            {
                //No change
                return;
            }
            if (rule.Index != cmd.Index)
            {
                if (await rules.Where(r => r.Index == cmd.Index).Select(r => r).CountAsync() > 0)
                {
                    throw new ConflictException($"Rule with index '{cmd.Index}' aready exists");
                }
                rule.Index = cmd.Index;
            }
           
            rule.Pattern = cmd.Pattern;
            rule.SourceType = cmd.SourceType;
            rule.DestinationType = cmd.DestinationType;
            await _context.SaveChangesAsync(ct);
        }
    }
}
