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
    public class StudentController : ControllerBase
    {
        //dependency injection
        private readonly IConfiguration _configuration;
        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //get all data from the class table
        [HttpGet]
        public JsonResult Get()
        {

            // Query execution
            string query = @"
                    select Student_Id,Student_Name,convert(varchar(10),Student_Dateofbirth,120) as Student_Dateofbirth,Student_Email,Student_Age,Class_Id from dbo.Student
                    ";

            DataTable table = new DataTable();

            //getting api from json
            string sqlDataSource = _configuration.GetConnectionString("ClassAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
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
        public JsonResult Post(Student std)
        {

            // Query execution
            string query = @"insert into dbo.Student
                            (Student_Name,Student_Dateofbirth,Student_Email,Student_Age,Class_Id) 
                            values(@Student_Name,@Student_Dateofbirth,@Student_Email,@Student_Age,@Class_Id)
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
                    myCommand.Parameters.AddWithValue("@Student_Name", std.Student_Name);
                    myCommand.Parameters.AddWithValue("@Student_Dateofbirth", std.Student_Dateofbirth);
                    myCommand.Parameters.AddWithValue("@Student_Email", std.Student_Email);
                    myCommand.Parameters.AddWithValue("@Student_Age", std.Student_Age);
                    myCommand.Parameters.AddWithValue("@Class_Id", std.Class_Id);
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
        public JsonResult Put(Student std)
        {

            // Query execution
            string query = @"
                    update dbo.Student set 
                    Student_Name=@Student_Name,
                    Student_Dateofbirth=@Student_Dateofbirth,    
                    Student_Email=@Student_Email,
                    Student_Age=@Student_Age,    
                    Class_Id=@Class_Id 
                    where Student_Id=@Student_Id
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
                    myCommand.Parameters.AddWithValue("@Student_Id", std.Student_Id);
                    myCommand.Parameters.AddWithValue("@Student_Name", std.Student_Name);
                    myCommand.Parameters.AddWithValue("@Student_Dateofbirth", std.Student_Dateofbirth);
                    myCommand.Parameters.AddWithValue("@Student_Email", std.Student_Email);
                    myCommand.Parameters.AddWithValue("@Student_Age", std.Student_Age);
                    myCommand.Parameters.AddWithValue("@Class_Id", std.Class_Id);
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
                    delete from dbo.Student
                    where Student_Id=@Student_Id
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
                    myCommand.Parameters.AddWithValue("@Student_Id", id);
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
