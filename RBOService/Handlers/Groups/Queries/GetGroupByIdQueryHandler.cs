using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using SimpleSoft.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Handlers.Groups
{
    public class GetGroupByIdQueryHandler : IQueryHandler<GetGroupByIdQuery, Group>
    {
        private readonly IQueryable<GroupEntity> _groups;

        public GetGroupByIdQueryHandler(ApiDbContext context)
        {
            _groups = context.Set<GroupEntity>();
        }

        public async Task<Group> HandleAsync(GetGroupByIdQuery query, CancellationToken ct)
        {
            var group = await _groups.SingleOrDefaultAsync(p => p.ExternalId == query.GroupId, ct);

            if (group == null)
            {
                throw new NotFoundException(query.GroupId.ToString());
            }

            return new Group
            {
                Id = group.ExternalId,
                Name = group.Name
                
            };
        }
    }
}
