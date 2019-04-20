using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;

namespace ToolShed.Helpers
{
    public static class RequestExtensions
    {
        /// <summary>
        /// This method takes a model and serializes it. Automatically converts first letter of model property to lower case in json. 
        /// </summary>
        /// <param name="data">any model of most types</param>
        /// <returns>A nicely packaged string to send requests</returns>
        public static StringContent PrepareHttpContent(object data)
        {
            var json = JsonConvert.SerializeObject(data, SetupJsonFormat());

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Add camel case format to bind json from models
        /// </summary>
        /// <returns>JsonSerializerSettings</returns>
        private static JsonSerializerSettings SetupJsonFormat()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            return settings;
        }

        /// <summary>
        /// Takes the response from an API and converts it into the desired Type
        /// </summary>
        /// <typeparam name="T">Generic type input</typeparam>
        /// <param name="jsonResponse">the json string being converted into an object</param>
        /// <returns>a generic object of you choice</returns>
        public static T DeserializeObject<T>(string jsonResponse)
        {
            return JsonConvert.DeserializeObject<T>(jsonResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
