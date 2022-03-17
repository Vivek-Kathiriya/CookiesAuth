using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CookiesAuth.Data
{


    public class Person
    {
        //public Person()
        //{

        //}

        private readonly IConfiguration _Configuration;
        public Person(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public class PersonDetails
        {

            public int PersonID;
            public string FirstName;
            public string LastName;
            public int Age;
            public string EmailID;
            public string Gender;
            public int AddressID;
            public string Message;
        }



        public List<PersonDetails> GetAllPerson()
        {
            string connectionString = this._Configuration.GetConnectionString("MVC");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_get_all_persons", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter outputParameter = new SqlParameter();
            outputParameter.ParameterName = "@PersonCount";
            outputParameter.SqlDbType = System.Data.SqlDbType.Int;
            outputParameter.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(outputParameter);

            try
            {
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                List<PersonDetails> persons = new List<PersonDetails>();

                while (rdr.Read())
                {
                    PersonDetails person = new PersonDetails();

                    person.PersonID = Convert.ToInt32(rdr["PersonID"]);
                    person.FirstName = Convert.ToString(rdr["FirstName"]);
                    person.LastName = Convert.ToString(rdr["LastName"]);
                    person.Age = Convert.ToInt32(rdr["Age"]);
                    person.EmailID = Convert.ToString(rdr["EmailID"]);
                    person.Gender = Convert.ToString(rdr["Gender"]);
                    person.AddressID = Convert.ToInt32(rdr["AddressID"]);

                    persons.Add(person);

                }
                return persons;
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

        public PersonDetails GetPerson(int id)
        {
            string connectionString = this._Configuration.GetConnectionString("MVC");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_get_person", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonID", id);

            conn.Open();

            SqlDataReader rdr = cmd.ExecuteReader();

            PersonDetails person = new PersonDetails();

            while (rdr.Read())
            {
                person.PersonID = Convert.ToInt32(rdr["PersonID"]);
                person.FirstName = Convert.ToString(rdr["FirstName"]);
                person.LastName = Convert.ToString(rdr["LastName"]);
                person.Age = Convert.ToInt32(rdr["Age"]);
                person.EmailID = Convert.ToString(rdr["EmailID"]);
                person.Gender = Convert.ToString(rdr["Gender"]);
                person.AddressID = Convert.ToInt32(rdr["AddressID"]);
            }

            return person;
        }


        public void InsertPerson(PersonDetails obj)
        {

            string connectionString = this._Configuration.GetConnectionString("MVC");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_insert_person", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
            cmd.Parameters.AddWithValue("@LastName", obj.LastName);
            cmd.Parameters.AddWithValue("@Age", obj.Age);
            cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
            cmd.Parameters.AddWithValue("@Gender", obj.Gender);
            cmd.Parameters.AddWithValue("@AddressID", obj.AddressID);

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

        public void DeletePerson(int id)
        {
            string connectionString = this._Configuration.GetConnectionString("MVC");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_delete_person", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonID", id);

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

        public void UpdatePerson(PersonDetails obj)
        {
            string connectionString = this._Configuration.GetConnectionString("MVC");
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SP_update_person", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonID", obj.PersonID);
            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
            cmd.Parameters.AddWithValue("@LastName", obj.LastName);
            cmd.Parameters.AddWithValue("@Age", obj.Age);
            cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
            cmd.Parameters.AddWithValue("@Gender", obj.Gender);
            cmd.Parameters.AddWithValue("@AddressID", obj.AddressID);


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
    }

}
