using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Asp_react_api_tutorial.Models;

namespace Asp_react_api_tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        //dependency injection
        private readonly IConfiguration _configuration;
        public ClassController(IConfiguration configuration) {
            _configuration = configuration;
        }

        //get all data from the class table
        [HttpGet]
        public JsonResult Get() {

            // Query execution
            string query = @"
                    select Class_Id, Class_Name, Class_Year from dbo.Class
                    ";

            DataTable table = new DataTable();

            //getting api from json
            string sqlDataSource = _configuration.GetConnectionString("ClassAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) {
                    myReader = myCommand.ExecuteReader();
                    //filling the data in to table using sql reader
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        //post all data from the class table
        [HttpPost]
        public JsonResult Post(Class cls)
        {

            // Query execution
            string query = @"insert into dbo.Class
                            (Class_Name,Class_Year) 
                            values(@Class_Name,@Class_Year)
                            ";

            DataTable table = new DataTable();

            //posting api from json to database
            string sqlDataSource = _configuration.GetConnectionString("ClassAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Class_Name", cls.Class_Name);
                    myCommand.Parameters.AddWithValue("@Class_Year", cls.Class_Year);
                    myReader = myCommand.ExecuteReader();
                    //filling the data in to table using sql reader
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        //put all data from the class table
        [HttpPut]
        public JsonResult Put(Class cls)
        {

            // Query execution
            string query = @"
                    update dbo.Class set 
                    Class_Name = @Class_Name,
                    Class_Year=@Class_Year 
                    where Class_Id=@Class_Id
                    ";

            DataTable table = new DataTable();

            //posting api from json to database
            string sqlDataSource = _configuration.GetConnectionString("ClassAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Class_Id", cls.Class_Id);
                    myCommand.Parameters.AddWithValue("@Class_Name", cls.Class_Name);
                    myCommand.Parameters.AddWithValue("@Class_Year", cls.Class_Year);
                    myReader = myCommand.ExecuteReader();
                    //filling the data in to table using sql reader
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        //delete all data from the class table
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {

            // Query execution
            string query = @"
                    delete from dbo.Class
                    where Class_Id=@Class_Id
                    ";

            DataTable table = new DataTable();

            //posting api from json to database
            string sqlDataSource = _configuration.GetConnectionString("ClassAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Class_Id",id);
                    myReader = myCommand.ExecuteReader();
                    //filling the data in to table using sql reader
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
