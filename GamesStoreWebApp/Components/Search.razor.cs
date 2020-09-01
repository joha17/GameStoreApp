using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Components
{
    public partial class Search
    {
        public string SearchTerm { get; set; }

        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }
    }
}
