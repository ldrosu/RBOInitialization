using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Groups.Commands
{
    public class DeleteGroupCommand : Command
    {
        public Guid GroupId { get; set; }
    }
}
