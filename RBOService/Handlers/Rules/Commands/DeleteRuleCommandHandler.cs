using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using SimpleSoft.Mediator;

namespace RBOService.Handlers.Rules.Commands
{
    
    public class DeleteRuleCommandHandler : ICommandHandler<DeleteRuleCommand>
    {
        private readonly ApiDbContext _context;

        public DeleteRuleCommandHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(DeleteRuleCommand cmd, CancellationToken ct)
        {
            var rules = _context.Set<RuleEntity>();

            //If group does not exist throw exception
            //A rule must belong to a group

            var rule = await rules.Where(r => r.ExternalId == cmd.RuleId).Select(g => g).FirstOrDefaultAsync();
            if (rule == null)
            {
                throw new NotFoundException($"Rule '{cmd.RuleId}' not found");
            }            
            rules.Remove(rule);
            await _context.SaveChangesAsync();
        }
    }
}
