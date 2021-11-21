using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalWebApi.Models
{
    public class Entretien
    {
        public int idEntretien { get; set; }
        public String Immatricule { get; set; }
        public int Km_Actuel { get; set; }
        public String Date_Entretien { get; set; }
        public String Type_de_Entretien { get; set; }
        public decimal Cout { get; set; }
        public String Observation { get; set; }
       
    }
}
