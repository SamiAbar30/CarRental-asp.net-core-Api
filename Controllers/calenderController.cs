using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using CarRentalWebApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRentalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calenderController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        Ado ado;
        DataTable dt;
        public calenderController(IConfiguration configuration)
        {
            ado = new Ado(configuration);
            
        }

        [HttpGet]
        public JsonResult get()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from calender ", "ExecuteReader");
            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(calender calender)
        {

            ado.crud<bool>("insert into calender values ('"+calender.title.ToString()+ "',GETDATE())", "ExecuteNonQuery");
            return new JsonResult("insert Successfully");
            
        }

    }
}
