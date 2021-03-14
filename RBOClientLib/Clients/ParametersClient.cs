using RBOClientLib.DTOs.Parameters;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RBOClientLib.Clients
{
    public class ParametersClient
    {
        HttpClient client;
        string controllerPath;

        public ParametersClient(HttpClient client)
        {
            this.client = client;
            controllerPath = "Parameters";
        }
        public async Task<Guid> CreatAsync(Guid ruleId, int index, string value)
        {
            CreateParameterRequest request = new CreateParameterRequest
            {
                Index = index,
                Value = value,
                RuleId = ruleId
            };
            HttpResponseMessage response = await client.PostAsJsonAsync(
                controllerPath, request);
            if (response.IsSuccessStatusCode)
            {
                CreateParameterResponse rule = await response.Content.ReadAsAsync<CreateParameterResponse>();
                return rule.Id;
            }
            else
            {
                throw new InvalidOperationException($"Parameter creation at index '{index}' for rule '{ruleId}' failed.");
            }
        }
    }
}
