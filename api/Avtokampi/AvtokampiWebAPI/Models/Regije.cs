using System;
using System.Collections.Generic;

namespace AvtokampiWebAPI.Models
{
    public partial class Regije
    {
        public Regije()
        {
            Avtokampi = new HashSet<Avtokampi>();
        }

        public int RegijaId { get; set; }
        public string Naziv { get; set; }
        public int Drzava { get; set; }

        public virtual Drzave DrzavaNavigation { get; set; }
        public virtual ICollection<Avtokampi> Avtokampi { get; set; }
    }
}
