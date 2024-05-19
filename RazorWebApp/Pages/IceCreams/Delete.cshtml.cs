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
    public class DeleteModel : PageModel
    {
        private readonly RazorWebApp.Data.RazorWebAppContext _context;

        public DeleteModel(RazorWebApp.Data.RazorWebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IceCream IceCream { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icecream = await _context.IceCream.FirstOrDefaultAsync(m => m.Id == id);

            if (icecream == null)
            {
                return NotFound();
            }
            else
            {
                IceCream = icecream;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icecream = await _context.IceCream.FindAsync(id);
            if (icecream != null)
            {
                IceCream = icecream;
                _context.IceCream.Remove(IceCream);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
