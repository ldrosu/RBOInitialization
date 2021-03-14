using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Rules.Queries
{
    public class GetRuleByIdQueryHandler : IQueryHandler<GetRuleByIdQuery, Rule>
    {
        private readonly IQueryable<RuleEntity> _rules;
        private readonly IQueryable<GroupEntity> _groups;

        public GetRuleByIdQueryHandler(ApiDbContext context)
        {
            _rules = context.Set<RuleEntity>();
            _groups = context.Set<GroupEntity>();
        }

        public async Task<Rule> HandleAsync(GetRuleByIdQuery query, CancellationToken ct)
        {
            var rule = await _rules.Where(p => p.ExternalId == query.RuleId).Select(r=>r).FirstOrDefaultAsync();
            
            if (rule == null)
            {
                throw new NotFoundException($"Rule '{query.RuleId}' not found");
            }

            return new Rule
            {
                Id = rule.ExternalId,
                Index = rule.Index,
                Pattern = rule.Pattern,
                SourceType = rule.SourceType,
                DestinationType = rule.DestinationType,
                GroupId = rule.Group.ExternalId
            };
        }
    }
}
