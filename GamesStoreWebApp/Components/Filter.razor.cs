using GamesStoreWebApp.Data;
using GamesStoreWebApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Components
{
    public partial class Filter
    {
        public int FilterTerm { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnFilterChanged { get; set; }


    }
}
