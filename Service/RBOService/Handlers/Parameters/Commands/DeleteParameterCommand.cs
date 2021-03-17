using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Parameters.Commands
{
    public class DeleteParameterCommand: Command
    {
        public Guid ParameterId { get; set; }
    }
}
