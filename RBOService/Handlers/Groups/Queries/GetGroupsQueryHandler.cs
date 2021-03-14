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
    public class GetGroupsQueryHandler : IQueryHandler<GetGroupsQuery, IEnumerable<Group>>
    {
        private readonly IQueryable<GroupEntity> _groups;

        public GetGroupsQueryHandler(ApiDbContext context)
        {
            _groups = context.Set<GroupEntity>();
        }

        public async Task<IEnumerable<Group>> HandleAsync(GetGroupsQuery query, CancellationToken ct)
        {
            List<GroupEntity> groupsTasks = await _groups.ToListAsync(cancellationToken: ct);
            IEnumerable<Group> groups = groupsTasks.Select(t => new Group
                                                    {
                                                        Id = t.ExternalId,
                                                        Name = t.Name,
                                                    });
            return groups;
        }
    }
}
