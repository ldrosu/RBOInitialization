using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RBOClientLib.DTOs.Rules;

namespace RBOClientLib.Clients
{
    class RulesClient
    {
        HttpClient client;
        string controllerPath;
        public RulesClient(HttpClient client)
        {
            this.client = client;
            controllerPath = "Rules";
        }
        public async Task<Guid> CreatAsync(Guid groupId, int index, string pattern, SourceTypeEnum sourceType, DestinationTypeEnum destinationType = DestinationTypeEnum.Any)
        {
            CreateRuleRequest request = new CreateRuleRequest
            {
                Index = index,
                Pattern = pattern,
                SourceType = sourceType,
                DestinationType = destinationType,
                GroupId = groupId
            };
            HttpResponseMessage response = await client.PostAsJsonAsync(
                controllerPath, request);
            if (response.IsSuccessStatusCode)
            {
                CreateRuleResponse rule = await response.Content.ReadAsAsync<CreateRuleResponse>();
                return rule.Id;
            }
            else
            {
                throw new UnauthorizedAccessException($"Unauthorized rule creation at index '{index}' for group '{groupId}'");
            }

        }
    }
}
