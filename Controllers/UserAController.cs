using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CarRentalWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CarRentalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAController : ControllerBase
    {
        Ado ado;
        DataTable dt;
        public UserAController(IConfiguration configuration)
        {
            ado = new Ado(configuration);

        }
        [HttpGet]
        public JsonResult get()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from usera", "ExecuteReader");
            return new JsonResult(dt);
        }
       
        [HttpPost]
        public JsonResult Post(UserA user)
        {
            try
            {
                ado.crud<bool>(@"insert into usera values('" + user.CIN + "','" + user.Nomuser + "','" + user.pass + "','" + user.prenomuser + "')", "ExecuteNonQuery");

                return new JsonResult("insert Successfully");
            }
            catch
            {
                return new JsonResult("insert error");
            }
        }
        [HttpPut]
        public JsonResult Put(UserA user)
        {
            try
            {
                ado.crud<bool>(@"update usera set Nomuser ='" + user.Nomuser + "',prenomuser='" + user.prenomuser + "',pass='" + user.pass + "' where CIN ='" + user.CIN + "' ", "ExecuteNonQuery");

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
            ado.crud<bool>("delete from usera where CIN='" +id+ "'", "ExecuteNonQuery");
            return new JsonResult("Deleted Successfully");
        }
    }
}
