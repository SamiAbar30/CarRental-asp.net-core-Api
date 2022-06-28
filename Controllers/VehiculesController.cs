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
    public class VehiculesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        
       
        int i = 0;
      
        Ado ado;
        DataTable dt;

        public VehiculesController(IConfiguration configuration)
        {
            ado = new Ado(configuration);
             
        }
        [HttpGet]
        public JsonResult get()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select carimg,Immatricule,Marque,Model,Kilometrage,Disponibilite,Prix,Carburant from vehicules ", "ExecuteReader");
            return new JsonResult(dt);
        }
        [Route("Vehiculedisp")]
        [HttpGet]
        public JsonResult Vehiculedisp()
        {
            try
            {
                dt = new DataTable();
                dt = ado.crud<DataTable>("select Immatricule,Marque,Model,Kilometrage,Prix,Carburant,Disponibilite from vehicules ", "ExecuteReader");
            }
            catch { }
            return new JsonResult(dt);
        }
        [Route("Vehicule/{id}")]
        public JsonResult Vehicule(string id)
        {
            try
            {
                dt = new DataTable();
                dt = ado.crud<DataTable>("select * from vehicules where Immatricule ='" + id + "'", "ExecuteReader");
            }
            catch
            {

            }
            return new JsonResult(dt);
        }
        [Route("VehiclesImmatricule")]
        public JsonResult VehiclesImmatricule()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select Immatricule from vehicules ", "ExecuteReader");
            return new JsonResult(dt);
        }
        [HttpPost]
        public JsonResult Post(Vehicules vehicules)
        {
            try
            {
                ado.crud<bool>("insert into vehicules values('" + vehicules.Immatricule + "','" + vehicules.Marque + "','" + vehicules.Model + "','" + vehicules.Date_MEC + "'," + Convert.ToInt32(vehicules.Kilometrage) + "," + Convert.ToInt32(vehicules.KM_inclus) + ",'" + vehicules.Disponibilite + "'," + Convert.ToInt32(vehicules.Prix) + ",'" + vehicules.Carburant + "','" + vehicules.carimg + "','" + vehicules.cartegrise_recto + "','" + vehicules.cartegrise_verso + "','" + vehicules.image3_recto + "','" + vehicules.image4_verso + "')", "ExecuteNonQuery");
               ado.crud<bool>("insert into papier(immatricule) values('" + vehicules.Immatricule + "')", "ExecuteNonQuery");

                return new JsonResult("insert Successfully");
        }
            catch
            {
                return new JsonResult("insert error");
    }
}
        [HttpPut]
        public JsonResult Put(Vehicules vehicules)
        {
            try
            {
                ado.crud<bool>("update vehicules set carimg='" + vehicules.carimg + "',Marque='" + vehicules.Marque + "',Model='" + vehicules.Model + "',Date_MEC='" + vehicules.Date_MEC + "',Kilometrage=" + Convert.ToInt32(vehicules.Kilometrage) + ",KM_inclus=" + Convert.ToInt32(vehicules.KM_inclus) + ",Disponibilite='" + vehicules.Disponibilite + "',Prix=" + Convert.ToInt32(vehicules.Prix) + ",Carburant='" + vehicules.Carburant + "',cartegrise_recto='" + vehicules.cartegrise_recto + "',cartegrise_verso='" + vehicules.cartegrise_verso + "',image3_recto='" + vehicules.image3_recto + "',image4_verso='" + vehicules.image4_verso + "' where Immatricule ='" + vehicules.Immatricule + "'", "ExecuteNonQuery");

                return new JsonResult("update Successfully");
            }
            catch
            {
                return new JsonResult("update error");
            }
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(string id)
        {
            ado.crud<bool>("delete from vehicules where Immatricule='" + id + "'", "ExecuteNonQuery");
            return new JsonResult("Deleted Successfully");
        }
        [Route("StatistiquesVehicles/{date}")]
        public JsonResult StatistiquesVehicles(string date)
        {
            
            
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
             dt = new DataTable();
            DataColumn dc ;
           
            dc = new DataColumn("Immatricule", typeof(String));
            dt.Columns.Add(dc);
            for (int i = 1; i <= 12; i++)
            {
                dc = new DataColumn(i.ToString(), typeof(string));
                dt.Columns.Add(dc);
            }
            dc = new DataColumn("total", typeof(int));
            dt.Columns.Add(dc);
            i = 0;
            dt1 = ado.crud<DataTable>("select Immatricule from vehicules", "ExecuteReader");
            dt.Rows.Clear();

            foreach (DataRow dtRow in dt1.Rows)
            {
                DataRow lign = dt.NewRow();
                lign["Immatricule"] = dtRow["Immatricule"].ToString();
                dt2 = ado.crud<DataTable>("select Duree_depart,Duree_retour,Duree from Contrats  where  Immatricule='" + dtRow["Immatricule"].ToString() + "'", "ExecuteReader");


                foreach (DataRow dt2Row in dt2.Rows)
                {

                    DateTime Duree_depart = Convert.ToDateTime(dt2Row["Duree_depart"].ToString());
                    DateTime Duree_retour = Convert.ToDateTime(dt2Row["Duree_retour"].ToString());
                    int depart = Convert.ToInt32((lign[Duree_depart.Month.ToString()].ToString() == "" ? 0 : lign[Duree_depart.Month.ToString()]));
                    int retour = Convert.ToInt32((lign[Duree_retour.Month.ToString()].ToString() == "" ? 0 : lign[Duree_retour.Month.ToString()]));

                    if (Duree_depart.Month == Duree_retour.Month && Duree_depart.Year == Duree_retour.Year && Duree_depart.Year.ToString() == date.ToString())
                    {

                        lign[Duree_depart.Month.ToString()] = depart + (Convert.ToInt32(Duree_retour.Day.ToString()) - Convert.ToInt32(Duree_depart.Day.ToString()));
                    }
                    else if (Duree_depart.Month + 1 == Duree_retour.Month && Duree_depart.Year == Duree_retour.Year && Duree_depart.Year.ToString() == date.ToString())
                    {

                        lign[Duree_depart.Month.ToString()] = depart + (DateTime.DaysInMonth(Duree_depart.Year, Duree_depart.Month) - Duree_depart.Day);

                        lign[Duree_retour.Month.ToString()] = retour + Duree_retour.Day;
                    }
                    else
                    {
                        for (int i = Duree_depart.Year; i <= Duree_retour.Year; i++)
                        {
                            if (i.ToString() == date.ToString())
                            {
                                int var1;
                                int var2;
                                if (i == Duree_depart.Year)
                                {
                                    lign[Duree_depart.Month.ToString()] = depart + (DateTime.DaysInMonth(Duree_depart.Year, Duree_depart.Month) - Duree_depart.Day);
                                    var1 = Duree_depart.Month + 1;
                                    var2 = 12;
                                }
                                else if (i == Duree_retour.Year)
                                {
                                    lign[Duree_retour.Month.ToString()] = retour + (Duree_retour.Day);
                                    var1 = 1;
                                    var2 = Duree_retour.Month - 1;
                                }
                                else
                                {
                                    var1 = 1;
                                    var2 = 12;
                                }
                                for (int y = var1; y <= var2; y++)
                                {
                                    lign[y.ToString()] = Convert.ToInt32((lign[y.ToString()].ToString() == "" ? 0 : lign[y.ToString()])) + (DateTime.DaysInMonth(i, y));
                                }
                            }
                        }
                    }

                }
                for (int y = 1; y <= 12; y++)
                {
                    lign["total"] = Convert.ToInt32((lign["total"].ToString() == "" ? 0 : lign["total"])) + Convert.ToInt32((lign[y.ToString()].ToString() == "" ? 0 : lign[y.ToString()]));
                }
                dt.Rows.Add(lign);
               

            }

            return new JsonResult(dt);
        }




    }
}
