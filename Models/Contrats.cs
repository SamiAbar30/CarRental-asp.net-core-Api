using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalWebApi.Models
{
    public class Contrats
    {
        public int idContrats { get; set; }
        public String Datecontrat { get; set; }
        public String realise_par_user { get; set; }
        public String Marque { get; set; }
        public String Immatricule { get; set; }
        public int Km_actuel { get; set; }
        public String numpi { get; set; }
        public String numpi2 { get; set; }
        public int Duree { get; set; }
        public String Duree_depart { get; set; }
        public String Duree_retour { get; set; }
        public String Lieu_de_livraison { get; set; }
        public String Lieu_de_retour { get; set; }
        public decimal Prix { get; set; }
        public int alert_rotour { get; set; }
        public decimal Total { get; set; }
        public bool facturé { get; set; }
        public decimal montantPayé { get; set; }
        public decimal restmontant { get; set; }
       
    }
}
