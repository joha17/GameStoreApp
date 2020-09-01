using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Models
{
    public class Products
    {
        [Key]
        public int IdProduct { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int Discount { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
