using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarRentalWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CarRentalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenceController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        DataTable dt;
        private readonly IConfiguration _configuration;
         Ado ado;
        public AgenceController( IConfiguration configuration, IWebHostEnvironment env)
        {
            ado = new Ado(configuration);
            _env = env;
        }

        [HttpGet]
        public JsonResult get()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from agence ", "ExecuteReader");
            return new JsonResult(dt);
        }
       
        [HttpPut]
        public JsonResult Put(Agence deagence)
        {

            
           ado.crud<bool>("update agence set Logo='" + deagence.Logo + "', NomAgence='" + deagence.NomAgence + "',Adresse='" + deagence.Adresse + "',Ville='" + deagence.Ville + "',Code_postale='" + deagence.Code_postale + "',Tel='" + deagence.Tel + "',E_mail='" + deagence.E_mail + "',Gsm='" + deagence.Gsm + "',Fax='" + deagence.Fax + "',Patant='" + deagence.Patant + "',IF_F='" + deagence.IF_F + "',RC='" + deagence.RC + "',ICE='" + deagence.ICE + "',CNSS = '" + deagence.CNSS + "'", "ExecuteNonQuery");

            return new JsonResult("update Successfully");
        }
       






    }
}
