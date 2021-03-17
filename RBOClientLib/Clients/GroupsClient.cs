using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RBOClientLib.DTOs.Groups;

namespace RBOClientLib.Clients
{
    class GroupsClient
    {
        HttpClient client;
        string controllerPath;

        public GroupsClient(HttpClient client)
        {
            this.client = client;
            controllerPath = "Groups";
        }
       
        public async Task<(Guid, bool)> CreatAsync(string name)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                controllerPath, new CreateGroupRequest { Name = name });
            
            if (response.IsSuccessStatusCode || response.StatusCode==HttpStatusCode.Conflict)
            {
                CreateGroupResponse group = await response.Content.ReadAsAsync<CreateGroupResponse>();
                bool IsNewGroup = response.IsSuccessStatusCode;
                return (group.Id, IsNewGroup);
            }
            else
            {
                throw new UnauthorizedAccessException($"Unouthorized group creation '{name}'");
            }      
        }
    }
}
