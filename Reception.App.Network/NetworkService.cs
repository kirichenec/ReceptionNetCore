﻿using Newtonsoft.Json;
using Reception.App.Network.Exceptions;
using Reception.Model.Network;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reception.App.Network
{
    public class NetworkService<T> : INetworkService<T>
    {
        public NetworkService(string serverPath)
        {
            ServerPath = serverPath;
        }

        public string ServerPath { get; set; }
         
        public async Task<IEnumerable<T>> SearchTAsync(string searchText)
        {
            var response = await Core.ExecuteGetTaskAsync($"{ServerPath}/api/{typeof(T).Name}?searchText={searchText}");

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<List<T>>>(response.Content);
                if (content.ErrorCode == ErrorCode.Ok)
                {
                    return content.Data;
                }
                if (content.ErrorCode == ErrorCode.NotFound)
                {
                    throw new NotFoundException<T>(ErrorCode.NotFound.ToString());
                }
                throw new Exception(content.ErrorMessage);
            }
            throw response.ErrorException;
        }
    }
}