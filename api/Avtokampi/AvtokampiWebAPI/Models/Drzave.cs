using System;
using System.Collections.Generic;

namespace AvtokampiWebAPI.Models
{
    public partial class Drzave
    {
        public Drzave()
        {
            Regije = new HashSet<Regije>();
        }

        public int DrzavaId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Regije> Regije { get; set; }
    }
}
