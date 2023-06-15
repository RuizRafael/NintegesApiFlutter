using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using NintegesApiFlutter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NintegesApiFlutter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecordController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public RecordController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        [HttpGet]
        [Route("[action]")]
        public JsonResult AllRecords(){
            string query = @"select * from record";
            DataTable table = new DataTable();
            string sqlDataSource = configuration.GetConnectionString("sqlCon");
            MySqlDataReader reader;
            using (MySqlConnection conection = new MySqlConnection(sqlDataSource))
            {
                conection.Open();
                using (MySqlCommand command = new MySqlCommand(query, conection))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader);

                    reader.Close();
                    conection.Close();
                }
            }
            return new JsonResult(table);
        }




        [HttpPost]
        [Route("[action]")]
        public JsonResult InsertRecord(Record record)
        {
            //string query = @"insert into record (type,userId,workplaceId,isSameLocation,note) values (@type,@userId,@workplaceId,@isSameLocation,@note";
            string query = $"insert into record (type,userId,workplaceId,isSameLocation,note) values ('{record.type}','{record.userId}','{record.workplaceId}','{record.isSameLocation}','{record.note}')";

            DataTable table = new DataTable();
            string sqlDataSource = configuration.GetConnectionString("sqlCon");
            MySqlDataReader reader;
            using (MySqlConnection conection = new MySqlConnection(sqlDataSource))
            {
                conection.Open();
                using (MySqlCommand command = new MySqlCommand(query, conection))
                {
                    //command.Parameters.AddWithValue("@type", record.type);
                    //command.Parameters.AddWithValue("@userId", record.userId);
                    //command.Parameters.AddWithValue("@workplaceId", record.workplaceId);
                    //command.Parameters.AddWithValue("@isSameLocation", record.isSameLocation);
                    //command.Parameters.AddWithValue("@note", record.note);
                    reader = command.ExecuteReader();
                    table.Load(reader);

                    reader.Close();
                    conection.Close();
                }
            }
            return new JsonResult("Added successfully");
        }

        [HttpPost]
        [Route("[action]")]
        public JsonResult InsertRecordParams(int type,int userId,int workplaceId,int isSameLocation, string note)
        {
            //string query = @"insert into record (type,userId,workplaceId,isSameLocation,note) values (@type,@userId,@workplaceId,@isSameLocation,@note";
            string query = $"insert into record (type,userId,workplaceId,isSameLocation,note) values ('{type}','{userId}','{workplaceId}','{isSameLocation}','{note}')";

            DataTable table = new DataTable();
            string sqlDataSource = configuration.GetConnectionString("sqlCon");
            MySqlDataReader reader;
            using (MySqlConnection conection = new MySqlConnection(sqlDataSource))
            {
                conection.Open();
                using (MySqlCommand command = new MySqlCommand(query, conection))
                {
                    //command.Parameters.AddWithValue("@type", record.type);
                    //command.Parameters.AddWithValue("@userId", record.userId);
                    //command.Parameters.AddWithValue("@workplaceId", record.workplaceId);
                    //command.Parameters.AddWithValue("@isSameLocation", record.isSameLocation);
                    //command.Parameters.AddWithValue("@note", record.note);
                    reader = command.ExecuteReader();
                    table.Load(reader);

                    reader.Close();
                    conection.Close();
                }
            }
            return new JsonResult("Added successfully");
        }



    }
}
