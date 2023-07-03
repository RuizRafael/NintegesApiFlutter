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

    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        
        [HttpPost]
        [Route("[action]")]
        public JsonResult AllUsers()
        {
            string query = @"select * from user";
            DataTable table = new DataTable();
            string sqlDataSource = configuration.GetConnectionString("sqlCon");
            MySqlDataReader reader;
            using(MySqlConnection conection = new MySqlConnection(sqlDataSource))
            {
                conection.Open();
                using(MySqlCommand command = new MySqlCommand(query, conection))
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
        public JsonResult UserById(int id)
        {
            string query = @"select * from user where id = @id";
            DataTable table = new DataTable();
            string sqlDataSource = configuration.GetConnectionString("sqlCon");
            MySqlDataReader reader;
            using (MySqlConnection conection = new MySqlConnection(sqlDataSource))
            {
                conection.Open();
                using (MySqlCommand command = new MySqlCommand(query, conection))
                {
                    command.Parameters.AddWithValue("@id", id);
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
        public JsonResult UserLogin(string nif, string password)
        {
            nif = MySql.Data.MySqlClient.MySqlHelper.EscapeString(nif);
            password = MySql.Data.MySqlClient.MySqlHelper.EscapeString(password);

            //string query = @"select * from user where id = @id";
            string query = $"SELECT * FROM user WHERE NIF = '{nif}' AND Password = '{password}';";

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
        public JsonResult InsertUser(string nif, string fullName, string name, string token, int access, string password)
        {
            //string query = @"insert into record (type,userId,workplaceId,isSameLocation,note) values (@type,@userId,@workplaceId,@isSameLocation,@note";
            string query = $"INSERT INTO `nineteges`.`user`(`nif`,`fullName`,`name`,`workplaces`,`token`,`access`,`password`)VALUES('{nif}','{fullName}','{name}',null,'{token}','{access}','{password}')";
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
