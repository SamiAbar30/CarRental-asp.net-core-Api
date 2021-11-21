using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalWebApi.Models
{
    public class Agence
    {
        public int idAgence {get;set;}
        public String NomAgence { get; set; }
        public String Logo { get; set; }
        public String Adresse { get; set; }
        public String Ville { get; set; }
        public String Code_postale { get; set; }
        public String Tel { get; set; }
        public String E_mail { get; set; }
        public String Gsm { get; set; }
        public String Fax { get; set; }
        public String Patant { get; set; }
        public String IF_F { get; set; }
        public String RC { get; set; }
        public String ICE { get; set; }
        public String CNSS { get; set; }

    }
}
