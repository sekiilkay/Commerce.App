using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.ViewModels
{
    public class CartViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalCartPrice { get; set; }
        public int ProductId { get; set; }
    }
}
