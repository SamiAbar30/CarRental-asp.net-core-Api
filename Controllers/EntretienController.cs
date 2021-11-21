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
    public class EntretienController : ControllerBase
    {
   
        Ado ado;
        DataTable dt;
        public EntretienController(IConfiguration configuration)
        {
            ado = new Ado(configuration);
            
        }
        [HttpGet]
        public JsonResult get()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from Entretien ", "ExecuteReader");
            return new JsonResult(dt);
        }
        [HttpPost]
        public JsonResult Post(Entretien entretien)
        {
            try
            {
                ado.crud<bool>("insert into Entretien values('" +entretien.Immatricule + "'," + entretien.Km_Actuel + ",'" + entretien.Date_Entretien + "','" + entretien.Type_de_Entretien + "'," + entretien.Cout + ",'" + entretien.Observation + "')", "ExecuteNonQuery");

                return new JsonResult("insert Successfully");
            }
            catch
            {
                return new JsonResult("insert error");
            }
        }
        [HttpPut]
        public JsonResult Put(Entretien entretien)
        {
            try
            {
                ado.crud<bool>(@"update Entretien set  Immatricule='" + entretien.Immatricule + "',Km_Actuel=" + Convert.ToInt32(entretien.Km_Actuel) + ",Date_Entretien='" + entretien.Date_Entretien + "',Type_de_Entretien='" + entretien.Type_de_Entretien + "',Cout=" + Convert.ToInt32(entretien.Cout) + ",Observation='" + entretien.Observation + "' where idEntretien=" + entretien.idEntretien, "ExecuteNonQuery");

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
            ado.crud<bool>("delete from Entretien where idEntretien=" +id+ "", "ExecuteNonQuery");
            return new JsonResult("Deleted Successfully");
        }
    }
}
