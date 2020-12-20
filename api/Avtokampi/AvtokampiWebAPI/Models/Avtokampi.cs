using System;
using System.Collections.Generic;

namespace AvtokampiWebAPI.Models
{
    public partial class Avtokampi
    {
        public Avtokampi()
        {
            Ceniki = new HashSet<Ceniki>();
            KampirnaMesta = new HashSet<KampirnaMesta>();
        }

        public int AvtokampId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string Telefon { get; set; }
        public int Regija { get; set; }

        public virtual Regije RegijaNavigation { get; set; }
        public virtual ICollection<Ceniki> Ceniki { get; set; }
        public virtual ICollection<KampirnaMesta> KampirnaMesta { get; set; }
    }
}
