using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Groups
{
    public class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, CreateGroupResult>
    {
        private readonly ApiDbContext _context;

        public CreateGroupCommandHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<CreateGroupResult> HandleAsync(CreateGroupCommand cmd, CancellationToken ct)
        {
            var groups = _context.Set<GroupEntity>();
            var group = await groups.Where(g => g.Name == cmd.Name).Select(g => g).FirstOrDefaultAsync();
            if (group == null)
            {
                var externalId = Guid.NewGuid();
                await groups.AddAsync(new GroupEntity
                {
                    ExternalId = externalId,
                    Name = cmd.Name,

                }, ct);

                await _context.SaveChangesAsync(ct);

                return new CreateGroupResult
                {
                    Id = externalId,
                    IsNew = true
                };
            }
            else
            {
                return new CreateGroupResult
                {
                    Id = group.ExternalId,
                    IsNew = false
                };
            }
        }
    }    
}
