using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NintegesApiFlutter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WorkplaceController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public WorkplaceController(IConfiguration _configuration)

        {
            configuration = _configuration;
        }

        [HttpPost]
        [Route("[action]")]
        public JsonResult AllWorkplaces()
        {
            string query = @"select * from workplace";
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
        [Route("[action]/{id}")]
        public JsonResult WorkplaceByUserID(int id)
        {
            string query = $"select w.* from userworkplaces uw join workplace w on w.id = uw.workplaceId where uw.userId = '{id}'";

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
        public JsonResult InsertWorkplace(string code, string name, string address, string latitude, string longitude)
        {
            //string query = @"insert into record (type,userId,workplaceId,isSameLocation,note) values (@type,@userId,@workplaceId,@isSameLocation,@note";
            string query = $"INSERT INTO `nineteges`.`workplace`(`code`,`name`,`address`,`latitude`,`longitude`)VALUES('{code}','{name}','{address}','{latitude}','{longitude}')";

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
            return new JsonResult("Added successfully");
        }







    }
}
