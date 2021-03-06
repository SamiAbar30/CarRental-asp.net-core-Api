using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CarRentalWebApi.Models;
using System.Collections;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace CarRentalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        DataTable dt;

        private readonly IConfiguration _configuration;
        Ado ado;

        public ContratsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            ado = new Ado(configuration);
            _env = env;
        }
        public Hashtable hashtable(Contrats contrats)
        {
            Hashtable para = new Hashtable();
            para.Add("idclient2", contrats.numpi2);
            para.Add("idclient", contrats.numpi);
            para.Add("datecontrat", contrats.Datecontrat);
            para.Add("realise", contrats.realise_par_user);
            para.Add("marque", contrats.Marque);
            para.Add("imatricule", contrats.Immatricule);
            para.Add("kmactuel", Convert.ToInt32(contrats.Km_actuel));
            para.Add("duree", Convert.ToInt32(contrats.Duree));
            para.Add("duree_depart", contrats.Duree_depart);
            para.Add("duree_retour", contrats.Duree_retour);
            para.Add("lieuliv", contrats.Lieu_de_livraison);
            para.Add("lieure", contrats.Lieu_de_retour);
            para.Add("prix", Convert.ToDouble(contrats.Prix));
            para.Add("alrt", Convert.ToInt32(contrats.alert_rotour));
            para.Add("total", Convert.ToDouble(contrats.Total));
            para.Add("montapayer", Convert.ToDouble(contrats.montantPayé));
            para.Add("restmontant", Convert.ToDouble(contrats.restmontant));
            return para;
        }

        [HttpGet]
        public JsonResult get()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from contrats ", "ExecuteReader");
            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(Contrats contrats)
        {

            ado.crud<bool>("insert into contrats values (@datecontrat,@realise,@marque,@imatricule,@kmactuel,@idclient,@idClient2,@duree,@duree_depart,@duree_retour,@lieuliv,@lieure,@prix,@alrt,@total,0,@montapayer,@restmontant)", "ExecuteNonQuery", hashtable(contrats));
            ado.crud<bool>("update vehicules set Disponibilite='non' where Immatricule ='" + contrats.Immatricule + "'", "ExecuteNonQuery");
            Hashtable cimp = new Hashtable();
            cimp.Add("idclient", contrats.numpi);
            cimp.Add("solde", Convert.ToDouble(contrats.Total));
            cimp.Add("des", "location d'un vehicule : " + contrats.Marque + Environment.NewLine + "du " + contrats.Duree_depart + " au " + contrats.Duree_retour);
            ado.crud<bool>("insert into comptecliant values (@idclient,@des,@solde)", "ExecuteNonQuery", cimp);
            return new JsonResult("insert Successfully");
        }
        [HttpPut]
        public JsonResult put(Contrats contrats)
        {
            Hashtable para = new Hashtable();
            para.Add("idContrats", Convert.ToInt32(contrats.idContrats));
            para.Add("duree", Convert.ToInt32(contrats.Duree));       
            para.Add("duree_retour", contrats.Duree_retour);       
            para.Add("total", Convert.ToDouble(contrats.Total));
            para.Add("montapayer", Convert.ToDouble(contrats.montantPayé));
            para.Add("restmontant", Convert.ToDouble(contrats.restmontant));
            ado.crud<bool>("update contrats set Duree=@duree ,Duree_retour=@duree_retour,Total=@total,montantPayé=@montapayer,restmontant=@restmontant where id=@idContrats", "ExecuteNonQuery", para);
           Hashtable cimp = new Hashtable();
            cimp.Add("idclient", contrats.numpi);
            cimp.Add("solde", Convert.ToDouble(contrats.Total));
            cimp.Add("des", "location d'un veh-icule : " + contrats.Marque + Environment.NewLine + "du " + contrats.Duree_depart + " au " + contrats.Duree_retour);
            ado.crud<bool>("insert into comptecliant values (@idclient,@des,@solde)", "ExecuteNonQuery", cimp);
            return new JsonResult("insert Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            Hashtable par = new Hashtable();
            par.Add("id", id);
            ado.crud<bool>("delete from contrats where idcontrats = @id", "ExecuteNonQuery", par);
            return new JsonResult("Deleted Successfully");
        }
        



    }
}
