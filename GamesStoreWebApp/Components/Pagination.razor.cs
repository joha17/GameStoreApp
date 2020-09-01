using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesStoreWebApp.Components
{
    public partial class Pagination
    {
        [Parameter]
        public Entities.MetaData MetaData { get; set; }
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        private List<Features.PagingLink> _links;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            _links = new List<Features.PagingLink>();

            _links.Add(new Features.PagingLink(MetaData.CurrentPage - 1, MetaData.HasPrevious, "Previous"));

            for (int i = 1; i <= MetaData.TotalPages; i++)
            {
                if (i >= MetaData.CurrentPage - Spread && i <= MetaData.CurrentPage + Spread)
                {
                    _links.Add(new Features.PagingLink(i, true, i.ToString()) { Active = MetaData.CurrentPage == i });
                }
            }

            _links.Add(new Features.PagingLink(MetaData.CurrentPage + 1, MetaData.HasNext, "Next"));
        }

        private async Task OnSelectedPage(Features.PagingLink link)
        {
            if (link.Page == MetaData.CurrentPage || !link.Enabled)
                return;

            MetaData.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
    }
}
