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
    public class CreateParameterCommandHandler : ICommandHandler<CreateParameterCommand, CreateParameterResult>
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;

        public CreateParameterCommandHandler(ApiDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CreateParameterResult> HandleAsync(CreateParameterCommand cmd, CancellationToken ct)
        {
            await _context.semaphore.WaitAsync();
            try
            {
                var parameters = _context.Set<ParameterEntity>();
                var rules = _context.Set<RuleEntity>();

                //If group does not exist throw exception
                //A rule must belong to a group

                var rule = await rules.Where(g => g.ExternalId == cmd.RuleId).Select(g => g).FirstOrDefaultAsync();
                if (rule == null)
                {
                    throw new NotFoundException($"Rule '{cmd.RuleId}' not found");
                }

                //If Index already present throw exception
                //Two rules of the same group cannot have the same index

                var parametersDuplicateIndex = await parameters.Where(p => p.Rule.ExternalId == cmd.RuleId && p.Index == cmd.Index).Select(p => p).ToArrayAsync();
                if (parametersDuplicateIndex.Length > 0)
                {
                    throw new ConflictException($"Parameter with index '{cmd.Index}' already exists.");
                }

                var externalId = Guid.NewGuid();
                ParameterEntity createdParameter = new ParameterEntity
                {
                    ExternalId = externalId,
                    Index = cmd.Index,
                    Value = cmd.Value,
                    Rule = rule
                };

                await parameters.AddAsync(createdParameter, ct);

                await _context.SaveChangesAsync();

                return new CreateParameterResult
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
