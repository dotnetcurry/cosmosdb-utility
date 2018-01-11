using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBUtilitiesExample.DTOs
{
    public class DetailItemDTO
    {
        public string Id { get; set; } 
        public string Name { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public IEnumerable<SubItemDTO> SubItems { get; set; }

        public string AssignedToSurname { get; set; }

        public string AssignedToId { get; set; }
    }

    public class SubItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
