using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBUtilitiesExample.DTOs
{
    public class ListItemDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AssignedToSurname { get; set; }
    }
}
