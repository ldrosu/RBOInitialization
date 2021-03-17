using SimpleSoft.Mediator;
using System;

namespace RBOService.Handlers.Groups
{
    public class UpdateGroupCommand : Command
    {
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
    }
}
