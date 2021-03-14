using RBOClientLib.Clients;
using RBOClientLib.DTOs.Rules;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RBOClientLib
{
    public class RBOInitClient
    {
        class AddRuleParameters
        {
            //rule index is encoded in the array index
            public string Pattern { get; set; }
            public SourceTypeEnum SourceType { get; set; }
            public DestinationTypeEnum DestinationType { get; set; }
            public object[] Parameters { get; set; }
        }
        List<AddRuleParameters> addRuleParameters = new List<AddRuleParameters>();

        HttpClient client;
        GroupsClient groups;
        RulesClient rules;
        ParametersClient parameters;
        RBObjectsClient rBObjects;
        string groupName;

        public RBOInitClient(Uri uri, [CallerMemberName] string groupName = "Test")
        {
            client = new HttpClient();
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            SetClients(groupName);
        }

        public RBOInitClient(HttpClient client, [CallerMemberName] string groupName = "Test")
        {
            this.client = client;
            SetClients(groupName);
        }

        public void SetClients(string groupName)
        {
            groups = new GroupsClient(client);
            rules = new RulesClient(client);
            parameters = new ParametersClient(client);
            rBObjects = new RBObjectsClient(client);
            this.groupName = groupName;
        }

        private async Task<Guid> CreateGroupAsync(string name)
        {
            return await groups.CreatAsync(name);
        }

        private async Task<Guid> CreateRuleAsync(Guid groupId, int index, string pattern, SourceTypeEnum sourceType, DestinationTypeEnum destinationType = DestinationTypeEnum.Any)
        {
            return await rules.CreatAsync(groupId, index, pattern, sourceType, destinationType);
        }

        private async Task<Guid> CreateParameterAsync(Guid ruleId, int index, string value)
        {
            return await parameters.CreatAsync(ruleId, index, value);
        }
                
        public void AddRule(string pattern, DestinationTypeEnum destinationType, SourceTypeEnum sourceType, params object[] parameters)
        {
            addRuleParameters.Add(
                new AddRuleParameters
                {
                    Pattern = pattern,
                    DestinationType = destinationType,
                    SourceType = sourceType,
                    Parameters = parameters
                });

        }

        public void AddRule(string pattern, SourceTypeEnum sourceType, params object[] parameters)
        {
            AddRule(pattern, DestinationTypeEnum.Any, sourceType, parameters);
        }
      
        private Guid SendAllRules()
        {
            //Group
            Task<Guid> task = CreateGroupAsync(groupName);
            task.Wait();
            Guid groupId = task.Result;

            //Rules
            Task<Guid>[] ruleTasks = new Task<Guid>[addRuleParameters.Count];
            for (int i = 0; i < addRuleParameters.Count; i++)
            {
                var rule = addRuleParameters[i];
                ruleTasks[i] = CreateRuleAsync(groupId, i, rule.Pattern, rule.SourceType, rule.DestinationType);
                //ruleTasks[i].Wait();
            }
            Task.WaitAll(ruleTasks);
            //Parameters
            Task<Guid>[][] parameterTasks = new Task<Guid>[addRuleParameters.Count][];
            for (int i = 0; i < addRuleParameters.Count; i++)
            {
                var rule = addRuleParameters[i];
                parameterTasks[i] = new Task<Guid>[rule.Parameters.Length];
                Guid ruleId = ruleTasks[i].Result;
                for (int j = 0; j < rule.Parameters.Length; j++)
                {
                    parameterTasks[i][j] = CreateParameterAsync(ruleId, j, rule.Parameters[j].ToString());
                    //parameterTasks[i][j].Wait();
                }
            }
            for (int i = 0; i < addRuleParameters.Count; i++)
            {
                  Task.WaitAll(parameterTasks[i]);
            }
            return groupId;
            throw new NotImplementedException();
        }
        

        public T Create<T>()
        {         
            T template = CreateTemplate<T>();
            Guid groupId = SendAllRules();
            Task<T> task = rBObjects.CreatAsync<T>(groupId, template);
            task.Wait();
            T initializedObject = task.Result;
            return initializedObject;

        }
        
        public T CreateTemplate<T>()
        {
            return RBOInitTemplate.Create<T>();
        }
        
        public T[] CreateArray<T>(int length = 1)
        {
            T[] template = CreateArrayTemplate<T>();
            Guid groupId = SendAllRules();
            Task<T[]> task = rBObjects.CreatArrayAsync<T>(groupId, template, length);
            task.Wait();
            T[] initializedObject = task.Result;
            return initializedObject;
        }
        
        public T[] CreateArrayTemplate<T>()
        {
            return RBOInitTemplate.CreateArray<T>();
        }

    }
}
