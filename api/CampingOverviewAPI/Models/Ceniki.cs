using System;
using System.Collections.Generic;

namespace CampingOverviewAPI.Models
{
    public partial class Ceniki
    {
        public Ceniki()
        {
        }

        public int CenikId { get; set; }
        public string Naziv { get; set; }
        public decimal? Cena { get; set; }
        public int Avtokamp { get; set; }

        public virtual Avtokampi AvtokampNavigation { get; set; }
    }
}
