using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalWebApi.Models
{
    public class Clients
    {
        public String Clienttype { get; set; }
        public String Civilite { get; set; }
        public String Prenom { get; set; }
        public String Nom { get; set; }
        public String Date_de_naissance { get; set; }
        public int Age { get; set; }
        public String Lieu_de_naissance { get; set; }
        public String Numpermis { get; set; }
        public String Delivre_le { get; set; }
        public String Validite { get; set; }
        public String delivre_a { get; set; }
        public String Adresse { get; set; }
        public String Tel { get; set; }
    //    public String Email { get; set; }
        public String Type_pi { get; set; }
        public String Numpi { get; set; }
        public String Validite_pi { get; set; }
        public String Delivrer_a_pi { get; set; }
        public String Societe { get; set; }
        public String ADresse_de_facturation { get; set; }
        public String ICE { get; set; }
        public String Contact { get; set; }
        public bool Listenoir { get; set; }
        public String identiteimg_recto { get; set; }
        public String identiteimg_verso { get; set; }
        public String permi_recto { get; set; }
        public String permi_verso { get; set; }
        public String passeport_recto { get; set; }
        public String passeport_verso { get; set; }
     

    }
}
