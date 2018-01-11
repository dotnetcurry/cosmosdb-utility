using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CosmosDBUtilitiesExample.Models.CosmosDB
{
    public class Person
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
