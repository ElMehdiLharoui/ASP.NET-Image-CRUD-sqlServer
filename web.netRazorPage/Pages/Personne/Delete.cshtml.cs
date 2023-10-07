using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using web.netRazorPage.Data;
using web.netRazorPage.Models;

namespace web.netRazorPage.Pages.Personne
{
    public class DeleteModel : PageModel
    {
        private readonly web.netRazorPage.Data.webnetRazorPageContext _context;

        public DeleteModel(web.netRazorPage.Data.webnetRazorPageContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PersonneModel Personne { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Personne = await _context.Personne.FirstOrDefaultAsync(m => m.id == id);

            if (Personne == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Personne = await _context.Personne.FindAsync(id);

            if (Personne != null)
            {
                _context.Personne.Remove(Personne);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
