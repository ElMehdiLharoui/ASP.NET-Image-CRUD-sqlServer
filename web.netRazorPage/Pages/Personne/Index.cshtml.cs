using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.netRazorPage.Data;
using web.netRazorPage.Models;

namespace web.netRazorPage.Pages.Personne
{
    public class IndexModel : PageModel
    {
        private readonly web.netRazorPage.Data.webnetRazorPageContext _context;

        public IndexModel(web.netRazorPage.Data.webnetRazorPageContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IList<PersonneModel> Personne { get;set; }

        public async Task OnGetAsync()
        {
            var personnes = from m in _context.Personne
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                personnes = personnes.Where(s => s.nom.Contains(SearchString));
            }

            Personne = await personnes.ToListAsync();
        }
    }
}
