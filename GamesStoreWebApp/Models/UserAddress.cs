using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Models
{
    public class UserAddress
    {
        [Key]
        public string UserName { get; set; }
        [Required]
        public string CountryCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string FirstAddress { get; set; }

        public string SecondAddress { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}
