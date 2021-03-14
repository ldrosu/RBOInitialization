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
       
        public async Task<Guid> CreatAsync(string name)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                controllerPath, new CreateGroupRequest { Name = name });
            if (response.IsSuccessStatusCode)
            {
                CreateGroupResponse group = await response.Content.ReadAsAsync<CreateGroupResponse>();
                return group.Id;
            }
            else
            {
                throw new InvalidOperationException($"Group named '{name}' already exists");
            }      
        }
    }
}
