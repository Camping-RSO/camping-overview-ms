using System;
using System.Collections.Generic;

namespace AvtokampiWebAPI.Models
{
    public partial class KampirnaMesta
    {
        public KampirnaMesta()
        {
        }

        public int KampirnoMestoId { get; set; }
        public string Naziv { get; set; }
        public string Velikost { get; set; }
        public int Avtokamp { get; set; }
        public int Kategorija { get; set; }

        public virtual Avtokampi AvtokampNavigation { get; set; }
        public virtual Kategorije KategorijaNavigation { get; set; }
    }
}
