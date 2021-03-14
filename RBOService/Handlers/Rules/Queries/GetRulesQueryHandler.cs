using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Rules.Queries
{
    public class GetRulesQueryHandler : IQueryHandler<GetRulesQuery, IEnumerable<Rule>>
    {
        private readonly IQueryable<RuleEntity> _rules;

        public GetRulesQueryHandler(ApiDbContext context)
        {
            _rules = context.Set<RuleEntity>();
        }
        public async Task<IEnumerable<Rule>> HandleAsync(GetRulesQuery query, CancellationToken ct)
        {
            return await _rules.Select(r => new Rule
            {
                Id = r.ExternalId,
                Index = r.Index,
                Pattern = r.Pattern,
                SourceType = r.SourceType,
                DestinationType = r.DestinationType,
                GroupId = r.Group.ExternalId
            }).ToListAsync(ct);
        }
    }
}
