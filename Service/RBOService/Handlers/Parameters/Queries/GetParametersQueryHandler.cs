using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Parameters.Queries
{
    public class GetParametersQueryHandler : IQueryHandler<GetParametersQuery, IEnumerable<Parameter>>
    {
        private readonly IQueryable<ParameterEntity> _rules;

        public GetParametersQueryHandler(ApiDbContext context)
        {
            _rules = context.Set<ParameterEntity>();
        }
        public async Task<IEnumerable<Parameter>> HandleAsync(GetParametersQuery query, CancellationToken ct)
        {
            return await _rules.Select(r => new Parameter
            {
                Id = r.ExternalId,
                Index = r.Index,
                Value = r.Value,               
                RuleId = r.Rule.ExternalId
            }).ToListAsync(ct);
        }
    }
}
