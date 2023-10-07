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
using web.netRazorPage.Data;
using web.netRazorPage.Models;

namespace web.netRazorPage.Pages.Personne
{
    public class CreateModel : PageModel
    {
        private readonly web.netRazorPage.Data.webnetRazorPageContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CreateModel(web.netRazorPage.Data.webnetRazorPageContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PersonneModel Personne { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Generate a unique filename using a timestamp
                var fileName = DateTime.Now.Ticks + Path.GetExtension(ImageFile.FileName);

                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Ensure the uploads folder exists
                Directory.CreateDirectory(uploadsFolder);

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Save the file path in your database
                Personne.ImageUrl = "/uploads/" + fileName; // Update the path as per your project structure
            }

            _context.Personne.Add(Personne);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
