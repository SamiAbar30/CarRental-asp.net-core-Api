using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using CarRentalWebApi.Models;

namespace CarRentalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        Ado ado;
        DataTable dt;

        public LoginController(IConfiguration configuration)
        {
            ado = new Ado(configuration);

        }

        [HttpPost]
        public JsonResult Login(Login data)
        {
            dt = new DataTable();
            if ((int)(ado.crud<int>("select count(*) from UserA where CIN='" + data.CIN + "'and pass='" + data.pass + "'", "ExecuteScalar")) > 0)
            {
                return new JsonResult(true);
            }
            else { 
            return new JsonResult(false);
        }
        }
    }
}
