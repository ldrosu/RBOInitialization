using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.Parameters
{
    public class GetParameterByIdQuery: Query<Parameter>
    {
        public Guid ParameterId { get; set; }
    }
}
