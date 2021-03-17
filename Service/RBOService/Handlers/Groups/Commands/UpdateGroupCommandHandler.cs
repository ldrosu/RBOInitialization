using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using SimpleSoft.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Groups
{
    public class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand>
    {
        private readonly ApiDbContext _context;

        public UpdateGroupCommandHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(UpdateGroupCommand cmd, CancellationToken ct)
        {
            var groups = _context.Set<GroupEntity>();
            var group = await groups.SingleOrDefaultAsync(p => p.ExternalId == cmd.ExternalId, ct);
            if (group == null)
            {
                throw new NotFoundException(cmd.ExternalId.ToString());
            }
            group.Name = cmd.Name;

            await _context.SaveChangesAsync(ct);
        }
    }
}
