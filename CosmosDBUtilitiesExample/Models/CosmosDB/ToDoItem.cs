using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcControlsToolkit.Core.DataAnnotations;
using Newtonsoft.Json;

namespace CosmosDBUtilitiesExample.Models.CosmosDB
{
    public class ToDoItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        [JsonProperty(PropertyName = "subItems"), 
            CollectionKey("Id")]
        public IEnumerable<ToDoItem> SubItems { get; set; }

        [JsonProperty(PropertyName = "assignedTo")]
        public Person AssignedTo { get; set; }

        [JsonProperty(PropertyName = "team"),
            CollectionKey("Id")]
        public IEnumerable<Person> Team { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }
    }
}
