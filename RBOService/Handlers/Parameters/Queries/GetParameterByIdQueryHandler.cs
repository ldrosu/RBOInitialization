using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Parameters
{
    public class GetParameterByIdQueryHandler : IQueryHandler<GetParameterByIdQuery, Parameter>
    {
        private readonly IQueryable<ParameterEntity> _parameters;

        public GetParameterByIdQueryHandler(ApiDbContext context)
        {
            _parameters = context.Set<ParameterEntity>();
        }

        public async Task<Parameter> HandleAsync(GetParameterByIdQuery query, CancellationToken ct)
        {
            var parameter = await _parameters.SingleOrDefaultAsync(p => p.ExternalId == query.ParameterId, ct);

            if (parameter == null)
            {
                throw new InvalidOperationException($"Parameter '{query.ParameterId}' not found");
            }

            return new Parameter
            {
                Id = parameter.ExternalId,
                Index = parameter.Index,
                Value = parameter.Value,
                RuleId = parameter.Rule.ExternalId
            };
        }
    }
}
