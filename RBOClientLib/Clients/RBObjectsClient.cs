using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RBOClientLib.Clients
{
    class RBObjectsClient
    {

        HttpClient client;
        string controllerPath;

        public RBObjectsClient(HttpClient client)
        {
            this.client = client;
            controllerPath = "RBObjects";
        }

        public async Task<T> CreatAsync<T>(Guid groupId, T rBObject)
        {
            string path = $"{controllerPath}/{groupId}";     
            HttpResponseMessage response = await client.PostAsJsonAsync(
                path, rBObject);
            if (response.IsSuccessStatusCode)
            {
                T InitializedObject = await response.Content.ReadAsAsync<T>();
                return InitializedObject;
            }
            else
            {
                throw new InvalidOperationException($"Invalid operation");
            }

        }
        public async Task<T[]> CreatArrayAsync<T>(Guid groupId, T[] rBArray, int length)
        {
            string path = $"{controllerPath}/array/{groupId}?length={length}";
            HttpResponseMessage response = await client.PostAsJsonAsync(
                path, rBArray);
            if (response.IsSuccessStatusCode)
            {
                T[] InitializedArray = await response.Content.ReadAsAsync<T[]>();
                return InitializedArray;
            }
            else
            {
                throw new InvalidOperationException($"Invalid operation");
            }

        }
    }
}

