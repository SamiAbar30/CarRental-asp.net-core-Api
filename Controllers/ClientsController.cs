using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CarRentalWebApi.Models;
using System.Collections;
using Microsoft.Extensions.Configuration;

namespace CarRentalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        DataTable dt;

        private readonly IConfiguration _configuration;
        Ado ado;

        public ClientsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            ado = new Ado(configuration);
            _env = env;
        }
        public Hashtable hashtable(Clients clients)
        {
            Hashtable paramss = new Hashtable();
            paramss.Add("Clienttype", clients.Clienttype);
            paramss.Add("Civilite", clients.Civilite);
            paramss.Add("Prenom", clients.Prenom);
            paramss.Add("Nom", clients.Nom);
            paramss.Add("Date_de_naissance", clients.Date_de_naissance);
            paramss.Add("Age", clients.Age);
            paramss.Add("Lieu_de_naissance", clients.Lieu_de_naissance);
            paramss.Add("Numpermis", clients.Numpermis);
            paramss.Add("Delivre_le", clients.Delivre_le);
            paramss.Add("Validite", clients.Validite);
            paramss.Add("delivre_a", clients.delivre_a);
            paramss.Add("Adresse", clients.Adresse);
            paramss.Add("Tel", clients.Tel);
            paramss.Add("Societe", clients.Societe);
            paramss.Add("Numpi", clients.Numpi);
            paramss.Add("Type_pi", clients.Type_pi);
            paramss.Add("ADresse_de_facturation", clients.ADresse_de_facturation);
            paramss.Add("ICE", clients.ICE);
            paramss.Add("Contact", clients.Contact);
            paramss.Add("Listenoir", clients.Listenoir);
            paramss.Add("identiteimg_recto", clients.identiteimg_recto);
            paramss.Add("identiteimg_verso", clients.identiteimg_verso);
            paramss.Add("permi_recto", clients.permi_recto);
            paramss.Add("permi_verso", clients.permi_verso);
            paramss.Add("passeport_recto", clients.passeport_recto);
            paramss.Add("passeport_verso", clients.passeport_verso);
            paramss.Add("Validite_pi", clients.Validite_pi);
            paramss.Add("Delivrer_a_pi", clients.Delivrer_a_pi);
            return paramss;
        }

        [HttpGet]
        public JsonResult get()
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select Clienttype,Prenom,Nom,Age,Tel,Numpi from clients ", "ExecuteReader");
            return new JsonResult(dt);
        }
        [Route("CompteCliant/{id}")]
        public JsonResult CompteCliant(string id)
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from CompteCliant where numpi ='"+id+"'", "ExecuteReader");
            return new JsonResult(dt);
        }
        [Route("Cliant/{id}")]
        public JsonResult Cliant(string id)
        {
            dt = new DataTable();
            dt = ado.crud<DataTable>("select * from clients where numpi ='" + id + "'", "ExecuteReader");
            return new JsonResult(dt);
        }
        [HttpPost]
        public JsonResult Post(Clients clients)
        {

            ado.crud<bool>("insert into clients values (@Clienttype,@Civilite,@Prenom,@Nom,@Date_de_naissance,@Age,@Lieu_de_naissance,@Numpermis,@Delivre_le,@Validite,@delivre_a,@Adresse,@Tel,@Type_pi,@Numpi,@Validite_pi,@Delivrer_a_pi,@Societe,@ADresse_de_facturation,@ICE,@Contact,@Listenoir,@identiteimg_recto,@identiteimg_verso,@permi_recto,@permi_verso,@passeport_recto,@passeport_verso)", "ExecuteNonQuery", hashtable(clients));
            return new JsonResult("insert Successfully");
        }
        [HttpPut]
        public JsonResult Put(Clients clients)
        {
            
           ado.crud<bool>("update clients set Clienttype=@Clienttype,Civilite=@Civilite,Prenom=@Prenom,Nom=@Nom,Date_de_naissance=@Date_de_naissance,Age=@Age,Lieu_de_naissance=@Lieu_de_naissance,Numpermis=@Numpermis,Delivre_le=@Delivre_le,Validite=@Validite,delivre_a=@delivre_a,Adresse=@Adresse,Tel=@Tel,Type_pi=@Type_pi,Numpi=@Numpi,Validite_pi=@Validite_pi,Delivrer_a_pi=@Delivrer_a_pi,Societe=@Societe,ADresse_de_facturation=@ADresse_de_facturation,ICE=@ICE,Contact=@Contact,Listenoir=@Listenoir,identiteimg_recto=@identiteimg_recto,identiteimg_verso=@identiteimg_verso,permi_recto=@permi_recto,permi_verso=@permi_verso,passeport_recto=@passeport_recto,passeport_verso=@passeport_verso where numpi = @numpi", "ExecuteNonQuery", hashtable(clients));
            return new JsonResult("update Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(string id)
        {
            Hashtable par = new Hashtable();
            par.Add("cin", id);
            ado.crud<bool>("delete from clients where Numpi = @cin", "ExecuteNonQuery", par);
            return new JsonResult("Deleted Successfully");
        }


        [Route("SavePhotoCliant")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/Client/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

    }
}
