using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Models
{
    public class Category
    {
        public int IdCategory { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
