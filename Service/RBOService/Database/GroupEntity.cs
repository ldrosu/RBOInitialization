using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Database
{
    public class GroupEntity
    {
        public long Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public List<RuleEntity> Rules { get; set; } = new List<RuleEntity>();
    }
}
