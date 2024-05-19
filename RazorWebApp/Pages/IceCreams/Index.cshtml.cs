using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorWebApp.Data;
using RazorWebApp.Models;

namespace RazorWebApp.Pages.IceCreams
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IList<IceCream> IceCream { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("IceCreamApiClient");
            var url = "/api/icecream";

            var iceCreams = await client.GetFromJsonAsync<IEnumerable<IceCream>>(url);
            IceCream = iceCreams.ToList();
        }
    }
}
