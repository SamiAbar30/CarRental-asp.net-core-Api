using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalWebApi.Models
{
    public class papier
    {
        public String immatricule { get; set; }
        public String date_debut_assurance { get; set; }
        public String date_fin_assurance { get; set; }
        public int alertAssurance { get; set; }
        public String date_debut_grise { get; set; }
        public String date_fin_grise { get; set; }
        public int alertgrise { get; set;}
        public String date_debut_visite { get; set; }
        public String date_fin_visite { get; set; }
        public int alertvisite { get; set; }
        public String date_debut_autoristion { get; set; }
        public String date_fin_autoristion { get; set; }
        public int alertautoristion { get; set; }

    }
}
