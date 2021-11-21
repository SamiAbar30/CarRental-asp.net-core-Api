using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
namespace CarRentalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlerteController : ControllerBase
    {

        Ado ado =new Ado();
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public AlerteController(IConfiguration configuration, IWebHostEnvironment env)
        {
            ado = new Ado(configuration);
            _env = env;
        }
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();


        DataTable dt2Visite = new DataTable();
        DataTable dtcartegs = new DataTable();
        DataTable dtautori = new DataTable();

        DataTable dtAlert = new DataTable();


        //assurance variables
        string dateAss = "";
        string dateAssMoinalaert = "";
        int alertAss = 0;

        // visite viariables
        string dateVisite = "";
        string dateVisiteMoinalaert = "";
        int alertVisite = 0;

        // carte vairables
        string datecart = "";
        string datecarteMoinalaert = "";
        int alertcarte = 0;

        // autorisation vairables
        string dateAuto = "";
        string dateAutoMoinalaert = "";
        int alertAuto = 0;
        public string imma = "";

        DataRow dr;

        [HttpGet]
        public JsonResult get()
        {

            dt2.Columns.Add("immatricule", typeof(string));
            dt2.Columns.Add("jours", typeof(string));


            dt2Visite.Columns.Add("immatricule", typeof(string));
            dt2Visite.Columns.Add("jours", typeof(string));


            dtcartegs.Columns.Add("immatricule", typeof(string));
            dtcartegs.Columns.Add("jours", typeof(string));



            dtautori.Columns.Add("immatricule", typeof(string));
            dtautori.Columns.Add("jours", typeof(string));

            dtAlert.Columns.Add("immatricule", typeof(string));
            dtAlert.Columns.Add("jours", typeof(string));

            int kilometres = 0;




            //papier

            DataTable dts = ado.crud<DataTable>("select vi.Immatricule,vi.Km_Actuel,Km_Prochain_vidange,Alerte_si_reste from Vidange vi", "ExecuteReader");

            foreach (DataRow rd4 in dts.Rows)
            {
                if (Convert.ToInt32(rd4["km_Actuel"]) > Convert.ToInt32(rd4[2]) || (Convert.ToInt32(rd4["km_Actuel"]) < Convert.ToInt32(rd4[2]) && Convert.ToInt32(rd4["km_Actuel"]) > (Convert.ToInt32(rd4[2]) - Convert.ToInt32(rd4[3]))))
                {

                    dr = dtAlert.NewRow();
                    dr[0] = rd4[0].ToString();
                    kilometres = (Convert.ToInt32(rd4[2]) - Convert.ToInt32(rd4["km_Actuel"]));
                    dr[1] = kilometres.ToString() + " KM rest ";
                    dtAlert.Rows.Add(dr);
                }
            }

            DataTable dt1 = ado.crud<DataTable>("select * from papier", "ExecuteReader");

            foreach (DataRow rd3 in dt1.Rows)
            {
                try
                {
                    dateAss = rd3[2].ToString();
                    alertAss = Convert.ToInt32(rd3[3]);

                    dateVisite = rd3[8].ToString();
                    alertVisite = Convert.ToInt32(rd3[9]);

                    datecart = rd3[5].ToString();
                    alertcarte = Convert.ToInt32(rd3[6]);

                    dateAuto = rd3[11].ToString();
                    alertAuto = Convert.ToInt32(rd3[12]);

                    dateAssMoinalaert = Convert.ToDateTime(dateAss).AddDays(-alertAss).ToString();
                    dateVisiteMoinalaert = Convert.ToDateTime(dateVisite).AddDays(-alertVisite).ToString();
                    datecarteMoinalaert = Convert.ToDateTime(datecart).AddDays(-alertcarte).ToString();

                    dateAutoMoinalaert = Convert.ToDateTime(dateAuto).AddDays(-alertAuto).ToString();


                    // if dyal assurance
                    if ((DateTime.Now <= Convert.ToDateTime(dateAss) &&
                        DateTime.Now >= Convert.ToDateTime(dateAssMoinalaert))
                        || DateTime.Now >= Convert.ToDateTime(dateAss))
                    {
                        dr = dt2.NewRow();
                        int jours = 0;

                        Convert.ToDateTime(dateAss).AddDays(-DateTime.Now.Day).ToString();
                        jours = Convert.ToInt32(Convert.ToDateTime(dateAss).Subtract(DateTime.Now).TotalDays);




                        dr[0] = rd3[0].ToString();
                        dr[1] = jours.ToString() + " jours";

                        dt2.Rows.Add(dr);


                    }



                    //if dyal visite
                    if ((DateTime.Now <= Convert.ToDateTime(dateVisite) &&
                       DateTime.Now >= Convert.ToDateTime(dateVisiteMoinalaert))
                       || DateTime.Now >= Convert.ToDateTime(dateVisite))
                    {


                        int jours = 0;

                        Convert.ToDateTime(dateVisite).AddDays(-DateTime.Now.Day).ToString();
                        jours = Convert.ToInt32(Convert.ToDateTime(dateVisite).Subtract(DateTime.Now).TotalDays);

                        dr = dt2Visite.NewRow();
                        dr[0] = rd3[0].ToString();
                        dr[1] = jours.ToString() + " jours";
                        dt2Visite.Rows.Add(dr);


                    }

                    //if dyal carte
                    if ((DateTime.Now <= Convert.ToDateTime(datecart) &&
                     DateTime.Now >= Convert.ToDateTime(datecarteMoinalaert))
                     || DateTime.Now >= Convert.ToDateTime(datecart))
                    {
                        int jours = 0;

                        Convert.ToDateTime(datecart).AddDays(-DateTime.Now.Day).ToString();
                        jours = Convert.ToInt32(Convert.ToDateTime(datecart).Subtract(DateTime.Now).TotalDays);


                        dr = dtcartegs.NewRow();
                        dr[0] = rd3[0].ToString();
                        dr[1] = jours.ToString() + " jours";
                        dtcartegs.Rows.Add(dr);


                    }

                    //if dyal autorisation
                    if ((DateTime.Now <= Convert.ToDateTime(dateAuto) &&
                     DateTime.Now >= Convert.ToDateTime(dateAutoMoinalaert))
                     || DateTime.Now >= Convert.ToDateTime(dateAuto))
                    {
                        int jours = 0;

                        Convert.ToDateTime(dateAuto).AddDays(-DateTime.Now.Day).ToString();
                        jours = Convert.ToInt32(Convert.ToDateTime(dateAuto).Subtract(DateTime.Now).TotalDays);

                        dr = dtautori.NewRow();
                        dr[0] = rd3[0].ToString();
                        dr[1] = jours.ToString() + " jours";
                        dtautori.Rows.Add(dr);
                    }
                }
                catch
                {

                }
            }
         
            DataSet ds = new DataSet();
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt2Visite);
            ds.Tables.Add(dtcartegs);
            ds.Tables.Add(dtautori);
            ds.Tables.Add(dtAlert);


            return new JsonResult(ds);
        }
      



      
    }
}
