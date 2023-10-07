using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.netRazorPage.Data;
using web.netRazorPage.Models;

namespace web.netRazorPage.Pages.Personne
{
    public class EditModel : PageModel
    {
        private readonly web.netRazorPage.Data.webnetRazorPageContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EditModel(web.netRazorPage.Data.webnetRazorPageContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        [HttpPatch]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var personneFromDb = await _context.Personne.FindAsync(Personne.id);

            if (personneFromDb == null)
            {
                return NotFound();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Supprimez l'ancienne image si nécessaire
                if (!string.IsNullOrEmpty(personneFromDb.ImageUrl))
                {
                    // Supprimez l'ancienne image ici (à mettre en œuvre)
                }

                // Téléchargez la nouvelle image
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(ImageFile.FileName)}";
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Mettez à jour la propriété ImageUrl
                personneFromDb.ImageUrl = $"/uploads/{fileName}";
            }

            // Mettez à jour d'autres propriétés si nécessaire
            personneFromDb.nom = Personne.nom;
            personneFromDb.prenom = Personne.prenom;

            _context.Personne.Update(personneFromDb);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }



        private bool PersonneExists(int id)
        {
            return _context.Personne.Any(e => e.id == id);
        }
    }
}
