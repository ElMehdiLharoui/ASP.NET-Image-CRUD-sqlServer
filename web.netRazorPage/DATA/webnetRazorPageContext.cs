using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web.netRazorPage.Models;

namespace web.netRazorPage.Data
{
    public class webnetRazorPageContext : DbContext
    {
        public webnetRazorPageContext(DbContextOptions<webnetRazorPageContext> options)
               : base(options)
        {
        }

        public DbSet<PersonneModel> Personne { get; set; } 

    }
}
