using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal Subtotal { get; set; }

        public string UserName { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; }

        public int IdProduct { get; set; }

        public string ProductName { get; set; }
    }
}
