using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public List<Products> Products { get; set; }
    }
}
