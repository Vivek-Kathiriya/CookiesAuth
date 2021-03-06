using CookiesAuth.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CookiesAuth.Data
{
    public class UserDA
    {
        private readonly IConfiguration _Configuration;

        public UserDA(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public void InsertUser(string username, string password)
        {
            string connectionString = this._Configuration.GetConnectionString("MVC");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_Create_User", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

            public UserModel GetUser(string username, string password)
        {
            string connectionString = this._Configuration.GetConnectionString("MVC");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_get_User", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            conn.Open();

            SqlDataReader rdr = cmd.ExecuteReader();

            UserModel user = new UserModel();

            while (rdr.Read())
            {
                user.Username = Convert.ToString(rdr["Username"]);
                user.Password = Convert.ToString(rdr["Password"]);
                user.Role = Convert.ToString(rdr["Role"]);
            }

            return user;
        }

    }
}
