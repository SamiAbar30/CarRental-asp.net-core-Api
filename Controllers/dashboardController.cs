using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using CarRentalWebApi.Models;

namespace CarRentalWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class dashboardController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        Ado ado;
        DataTable dt = new DataTable();
      
        public dashboardController(IConfiguration configuration)
        {
            ado = new Ado(configuration);

        }

        [Route("vehiculesoui")]
        public JsonResult vehiculesoui()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from Vehicules where disponibilite = 'oui' ", "ExecuteReader");
            return new JsonResult(dt);
        }

        [Route("vehiculesnone")]
        public JsonResult vehiculesnone()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from Vehicules where disponibilite = 'non' ", "ExecuteReader");
            return new JsonResult(dt);
        }
        [Route("vehiculesproch")]
        public JsonResult vehiculesproch()
        {
            DataTable dt4 = new DataTable();
            dt4.Columns.Add("immatricule", typeof(string));
            dt4.Columns.Add("marque", typeof(string));
            int j = 0;
            string date = "";
            string datemoinsAlert = "";
            int alertt = 0;
            DataRow dr;
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from contrats", "ExecuteReader");
            foreach (DataRow rd4 in dt.Rows)
            {

                date = rd4[10].ToString();
                alertt = Convert.ToInt32(rd4[14]);
                datemoinsAlert = Convert.ToDateTime(date).AddDays(-alertt).ToString();
                //if dyal voiture
                if ((DateTime.Now <= Convert.ToDateTime(date) &&
                 DateTime.Now >= Convert.ToDateTime(datemoinsAlert))
                 )
                {
                    j++;
                    dr = dt4.NewRow();
                    dr[0] = rd4["immatricule"].ToString();
                    dr[1] = rd4["marque"].ToString();
                    dt4.Rows.Add(dr);




                }




            }
            return new JsonResult(dt4);
        }
        [Route("vehiculesKM")]
        public JsonResult vehiculesKM()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select Immatricule,marque from Contrats where DATEPART(day,Duree_retour) =  DATEPART(day, getdate()) ", "ExecuteReader");
            return new JsonResult(dt);
        }
        [Route("updatevehiculesKM")]
        [HttpPut]
        public JsonResult updatevehiculesKM(dashV dashV)
        {
            try
            {
                ado.crud<bool>("update Vehicules set disponibilite = 'oui',Kilometrage="+ dashV.Kilometrage + " where Immatricule = '" + dashV.ToString()+ "'", "ExecuteNonQuery");

                return new JsonResult("update Successfully");
            }
            catch
            {
                return new JsonResult("update error");
            }
        }

        [Route("revenuees")]
        public JsonResult revenuees()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select DATEPART(month,Datecontrat) as 'm',sum(total)  as 't' from Contrats group by DATEPART(month,Datecontrat) order by 'm' asc", "ExecuteReader");
            List<int> tablerevenuees=new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                for (var j = 1; j <= 12; j++)
                {
                    
                    if (Convert.ToInt32(dt.Rows[i]["m"]) == j)
                    {
                        tablerevenuees[j - 1] += Convert.ToInt32(dt.Rows[i]["t"]);
                    }
                }
            }

            return new JsonResult(tablerevenuees);
        }
        [Route("depances")]
        public JsonResult depances()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select DATEPART(month,Date_Entretien) as 'm',  SUM( e.Cout+v.cout)  as 't' from Vidange v join Entretien e on e.Immatricule = v.Immatricule group by DATEPART(month,Date_Entretien) order by 'm' asc", "ExecuteReader");

            List<int> tabledepances = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                for (var j = 1; j <= 12; j++)
                {

                    if (Convert.ToInt32(dt.Rows[i]["m"]) == j)
                    {
                        tabledepances[j - 1] += Convert.ToInt32(dt.Rows[i]["t"]);
                    }
                }
            }
            return new JsonResult(tabledepances);
        }
        [Route("contrat")]
        public JsonResult contrat()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select DATEPART(month,Datecontrat) as 'M',count(*) as 'C'  from Contrats group by DATEPART(month,Datecontrat)order by 'm' asc", "ExecuteReader");

            List<int> tablecontrat = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                for (var j = 1; j <= 12; j++)
                {

                    if (Convert.ToInt32(dt.Rows[i]["M"]) == j)
                    {
                        tablecontrat[j - 1] += Convert.ToInt32(dt.Rows[i]["C"]);
                    }
                }
            }
            return new JsonResult(tablecontrat);
        }
    }
}
