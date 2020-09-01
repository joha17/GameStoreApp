using GamesStoreWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Features
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
