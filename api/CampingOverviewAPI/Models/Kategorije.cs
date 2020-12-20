using System;
using System.Collections.Generic;

namespace CampingOverviewAPI.Models
{
    public partial class Kategorije
    {
        public Kategorije()
        {
            KampirnaMesta = new HashSet<KampirnaMesta>();
        }

        public int KategorijaId { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<KampirnaMesta> KampirnaMesta { get; set; }
    }
}
