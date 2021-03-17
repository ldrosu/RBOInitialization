using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Groups.Commands
{
    public class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand>
    {
        private readonly ApiDbContext _context;

        public DeleteGroupCommandHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(DeleteGroupCommand cmd, CancellationToken ct)
        {
            var groups = _context.Set<GroupEntity>();

            //If group does not exist throw exception
            //A rule must belong to a group

            var group = await groups.Where(g => g.ExternalId == cmd.GroupId).Select(g => g).FirstOrDefaultAsync();
            if (group == null)
            {
                throw new NotFoundException($"Group '{cmd.GroupId}' not found");
            }
            groups.Remove(group);
            await _context.SaveChangesAsync();
        }
    }
}
