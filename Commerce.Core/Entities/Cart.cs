using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Commerce.Core.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string? AppUserId { get; set; }

        [JsonIgnore]
        public AppUser? AppUser { get; set; }
    }
}
