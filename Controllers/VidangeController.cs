using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CarRentalWebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CarRentalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VidangeController : ControllerBase
    {
      
        Ado ado;
        DataTable dt;

        public VidangeController(IConfiguration configuration)
        {
            ado = new Ado(configuration);
          
        }
        [HttpGet]
        public JsonResult get()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from vidange ", "ExecuteReader");
            return new JsonResult(dt);
        }
        [HttpPost]
        public JsonResult Post(Vidange vidange)
        {
            try
            {
                ado.crud<bool>(@"insert into vidange values('" + vidange.Immatricule + "','" + vidange.Date + "'," + vidange.Km_Actuel + "," + vidange.Type_de_vidange + "," + vidange.Km_Prochain_vidange + "," + vidange.Alerte_si_reste + "," + vidange.Cout + ")", "ExecuteNonQuery");

                return new JsonResult("insert Successfully");
            }
            catch
            {
                return new JsonResult("insert error");
            }
        }
        [HttpPut]
        public JsonResult Put(Vidange vidange)
        {
            try
            {
                ado.crud<bool>(@"update vidange set Immatricule='" + vidange.Immatricule + "',Date='" + vidange.Date + "',Km_Actuel=" + vidange.Km_Actuel + ",Type_de_vidange=" + vidange.Type_de_vidange + ",Km_Prochain_vidange=" + vidange.Km_Prochain_vidange + ",Alerte_si_reste=" +vidange.Alerte_si_reste + ",Cout=" + vidange.Cout + " where id=" + vidange.idVidange + " ", "ExecuteNonQuery");

                return new JsonResult("update Successfully");
            }
            catch
            {
                return new JsonResult("update error");
            }
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            ado.crud<bool>("delete from vidange where id=" + id + "", "ExecuteNonQuery");
            return new JsonResult("Deleted Successfully");
        }


    }
}
