using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalWebApi.Models
{
    public class Vehicules
    {
        
        public String Immatricule { get; set; }
        public String Marque { get; set; }
        public String Model { get; set; }
        public String Date_MEC { get; set; }
        public int Kilometrage { get; set; }
        public int KM_inclus { get; set; }
        public String Disponibilite { get; set; }
        public int Prix { get; set; }
        public String Carburant { get; set; }    
        public String carimg { get; set; }
        public String cartegrise_recto { get; set; }
        public String cartegrise_verso { get; set; }
        public String image3_recto { get; set; }
        public String image4_verso { get; set; }
    }
}
