using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using SimpleSoft.Mediator;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Rules.Commands
{
    public class CreateRuleCommandHandler : ICommandHandler<CreateRuleCommand, CreateRuleResult>
    {
        private readonly ApiDbContext _context;

        public CreateRuleCommandHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CreateRuleResult> HandleAsync(CreateRuleCommand cmd, CancellationToken ct)
        {
            await _context.semaphore.WaitAsync();
            try
            {
                var rules = _context.Set<RuleEntity>();
                var groups = _context.Set<GroupEntity>();

                //If group does not exist throw exception
                //A rule must belong to a group

                var group = await groups.Where(g => g.ExternalId == cmd.GroupId).Select(g => g).FirstOrDefaultAsync();
                if (group == null)
                {
                    throw new NotFoundException($"Group '{cmd.GroupId}' not found");
                }

                //If Index already present throw exception
                //Two rules of the same group cannot have the same index

                var rulesDuplicateIndex = await rules.Where(r => r.Group.ExternalId == cmd.GroupId && r.Index == cmd.Index).Select(r => r).ToArrayAsync();
                if (rulesDuplicateIndex.Length > 0)
                {
                    throw new ConflictException($"Rule with index '{cmd.Index}' already exists.");
                }

                var externalId = Guid.NewGuid();
                RuleEntity createdRule = new RuleEntity
                {
                    ExternalId = externalId,
                    Index = cmd.Index,
                    Pattern = cmd.Pattern,
                    SourceType = cmd.SourceType,
                    DestinationType = cmd.DestinationType,
                    Group = group
                };

                await rules.AddAsync(createdRule, ct);

                await _context.SaveChangesAsync();

                return new CreateRuleResult
                {
                    Id = externalId
                };
            }
            finally
            {
                _context.semaphore.Release();
            }
        }
    }
}
