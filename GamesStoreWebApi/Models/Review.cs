using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApi.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public string UserName { get; set; }

        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
