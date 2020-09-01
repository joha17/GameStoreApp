using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Models
{
    public class ShoppingCart
    {
        public int IdProduct { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int Discount { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public string UsernameOrder { get; set; }
    }
}
