using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Groups
{
    public class GetGroupByIdQuery: Query<Group>
    {
        public Guid GroupId { get; set; }
    }
}
