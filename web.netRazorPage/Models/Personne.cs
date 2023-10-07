using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.netRazorPage.Models
{
    public class PersonneModel
    {
        public int id { set; get; }
        public int age { set; get; }
        public string nom { set; get; }
        public string prenom { set; get; }
        public string ImageUrl { get; set; }
        public PersonneModel()
        {

        }
        public PersonneModel(int age, string n, string p, string i )
        {

            this.age = age;
            this.nom = n;
            prenom = p;
            ImageUrl = i;

        }

    }
}
