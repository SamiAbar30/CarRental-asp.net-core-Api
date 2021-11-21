using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalWebApi.Models
{
    public class Vidange
    {
        public int idVidange { get; set; }
        public String Immatricule { get; set; }
        public String Date { get; set; }
        public int Km_Actuel { get; set; }
        public int Type_de_vidange { get; set; }
        public int Km_Prochain_vidange { get; set; }
        public int Alerte_si_reste { get; set; }
        public decimal Cout { get; set; }

    }
}
