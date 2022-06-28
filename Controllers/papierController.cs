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
    public class papierController  : ControllerBase
    {
        
        DataTable dt;

        private readonly IConfiguration _configuration;
        Ado ado;

        public papierController(IConfiguration configuration)
        {
            ado = new Ado(configuration);
          
        }
        public Hashtable hashtable(papier papier)
        {
            Hashtable paramss = new Hashtable();
            paramss.Add("imm", papier.immatricule);
            paramss.Add("dateAss", papier.date_debut_assurance);
            paramss.Add("dateAss2", papier.date_fin_assurance);
            paramss.Add("alertAss", Convert.ToInt32(papier.alertAssurance));


            paramss.Add("dateGrise", papier.date_debut_grise);
            paramss.Add("dateGrise2", papier.date_fin_grise);
            paramss.Add("alertGrise", Convert.ToInt32(papier.alertgrise));

            paramss.Add("dateVisite", papier.date_debut_visite);
            paramss.Add("dateVisite2", papier.date_fin_visite);
            paramss.Add("alertVisite", papier.alertvisite);

            paramss.Add("dateauto", papier.date_debut_autoristion);
            paramss.Add("dateauto2", papier.date_fin_autoristion);
            paramss.Add("alertAuto", Convert.ToInt32(papier.alertautoristion));


            return paramss;
        }

        [HttpGet("{id}")]
        public JsonResult get(string id)
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from papier where immatricule = '"+id+"'", "ExecuteReader");
            return new JsonResult(dt);
        }

        [HttpPut]
        public JsonResult Put(papier papier)
        {

            ado.crud<bool>("update papier set  date_debut_assurance=@dateAss,date_fin_assurance=@dateAss2,alertAssurance=@alertAss,date_debut_grise=@dateGrise,date_fin_grise=@dateGrise2,alertgrise=@alertGrise,date_debut_visite=@dateVisite,date_fin_visite=@dateVisite2,alertvisite=@alertVisite,date_debut_autoristion=@dateauto,date_fin_autoristion=@dateauto2,alertautoristion=@alertAuto where immatricule  = @imm", "ExecuteNonQuery", hashtable(papier));
            return new JsonResult("update Successfully");
        }
        
    }
}
