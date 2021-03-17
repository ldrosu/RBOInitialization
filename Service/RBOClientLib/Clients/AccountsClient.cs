using RBOClientLib.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RBOClientLib.Clients
{
    class AccountsClient
    {
        HttpClient client;
        string controllerPath;

        public AccountsClient(HttpClient client)
        {
            this.client = client;
            controllerPath = "Accounts";
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            string path = $"{controllerPath}/login";
            HttpResponseMessage response = await client.PostAsJsonAsync(
                path, new LoginRequest { UserName = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                LoginResponse login = await response.Content.ReadAsAsync<LoginResponse>();

                return login.AccessToken;
            }
            else
            {
                throw new UnauthorizedAccessException($"Invalid username '{username}' or password");
            }
        }
    }
}
