using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal Subtotal { get; set; }

        public string UserName { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; }

        public int IdProduct { get; set; }
    }
}
