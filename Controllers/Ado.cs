using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Collections;
//using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Xml.Linq;
using System.Xml;
using Microsoft.Extensions.Configuration;

namespace CarRentalWebApi.Controllers
{
   public class Ado 
    {
        string connc = "";
  
        public  SqlConnection cnx;
        public SqlConnection cnx1;
        private readonly IConfiguration _configuration;

        public Ado(IConfiguration configuration)
        {
            _configuration = configuration;
            string sqlDatasours = _configuration.GetConnectionString("EmployeeAppCon");
            cnx = new SqlConnection(sqlDatasours);
            cnx1 = new SqlConnection(sqlDatasours);
            connc = sqlDatasours;
        }

        public Ado()
        {
           
        }
        SqlCommand cmd;
        SqlDataReader rd;
        DataTable dt;
        public T crud<T>(string query, String name, Hashtable Parameters = null, bool StoredProcedure = false)
        {
           

            var value = (dynamic)null;
            cmd = new SqlCommand(query, cnx);
            if (StoredProcedure == true)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            if (Parameters != null)
            {
                foreach (DictionaryEntry kvp in Parameters)
                {
                   
                    if (kvp.Value == null)
                    {
                        SqlParameter Parameter = new SqlParameter("@" + kvp.Key, kvp.Value);
                        Parameter.Value = DBNull.Value;
                        cmd.Parameters.Add(Parameter);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                    }
                    
                }
            }
            if (cnx.State == ConnectionState.Closed || cnx.State == ConnectionState.Broken)
            {
                cnx.Open();
            }
            
            if (name == "ExecuteReader")
            {
                value = new DataTable();
                rd = cmd.ExecuteReader();
                value.Load(rd);
            }
            else if (name == "ExecuteNonQuery")
            {
                     cmd.ExecuteNonQuery();
                    value = true;

            }
            else if (name == "ExecuteScalar")
            {
                try
                {
                    value = (T)cmd.ExecuteScalar();
                }
                catch
                {
                    value = 0;
                }
   
            }
            if (cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
            return value;

        }
        public SqlDataReader DataReader(string query)
        {
            cmd = new SqlCommand(query, cnx1);
            if (cnx1.State == ConnectionState.Closed || cnx1.State == ConnectionState.Broken)
            {
                cnx1.Open();
            }
            rd = cmd.ExecuteReader();
            
            return rd;
        }
        public  void cloase()
        {
            if (cnx1.State == ConnectionState.Open)
            {
                cnx1.Close();
            }
        }

        public string sqlconnec()
        {
            return connc;
        }
    }
}
