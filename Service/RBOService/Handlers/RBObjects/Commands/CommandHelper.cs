using Microsoft.EntityFrameworkCore;
using RBOService.Database;
using RBOService.Exceptions;
using RBOService.Initializations;
using RBOService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBOService.Handlers.RBObjects.Commands
{
    public static class CommandHelper
    {
        public static async Task<List<InitRule>> GetInitRules(ApiDbContext context, Guid groupId)
        {            
            var groups = context.Set<GroupEntity>();
            List<InitRule> initRules = new List<InitRule>();
            var group = await groups.Where(g => g.ExternalId == groupId).Select(g => g).FirstOrDefaultAsync();
            if (group == null)
                throw new NotFoundException($"Group '{groupId}' not found");
            List<RuleEntity> orderedRules = group.Rules.OrderBy(r => r.Index).ToList<RuleEntity>();
            foreach (RuleEntity ruleEntity in orderedRules)
            {
                InitRule initRule = new InitRule
                {
                    Pattern = RegexUtil.ToRegex(ruleEntity.Pattern),
                    SourceType = ruleEntity.SourceType,
                    DestinationType = ruleEntity.DestinationType
                };
                initRule.Parameters = ruleEntity.Parameters.OrderBy(p => p.Index).Select(p => p.Value).ToList();
                initRules.Add(initRule);
            }
            return initRules;
        }
    }
}
