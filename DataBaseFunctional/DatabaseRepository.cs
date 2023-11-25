using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Collections;

namespace DataBaseFunctional
{
    public class DatabaseRepository
    {
        private readonly string _connectionString;
        public DatabaseRepository() 
        { 
            _connectionString = "Data Source = LAPTOP-1BQG2FKL\\SQLEXPRESS; Initial Catalog = InfoMessageDB; Integrated Security=true";
        }

        public string GetByID(int id)
        {
            if (id <= 0) return "ID should be more zero";

            bool flag;
            string name, message;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select ID from Informations where ID =@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    flag = command.ExecuteScalar() == null ? false : true;

                    if (!flag) return "This Information isn't exist";

                    command.CommandText = "select name from Informations where ID =@id";
                    name = command.ExecuteScalarAsync().Result.ToString();

                    command.CommandText = "select message from Informations where ID =@id";
                    message = command.ExecuteScalarAsync().Result.ToString();

                    connection.Close();
                }
            }
            return "ID: " + id + "\nName: "+name+"\nMessage: "+message;
        }

        public string GetByName(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                return "Name is empty";

            bool flag;
            string id, message;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select name from Informations where name =@name";
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    flag = command.ExecuteScalar() == null ? false : true;

                    if (!flag) return "Information isn't exist";

                    command.CommandText = "select ID from Informations where name =@name";
                    id = command.ExecuteScalarAsync().Result.ToString();

                    command.CommandText = "select message from Informations where name =@name";
                    message = command.ExecuteScalarAsync().Result.ToString();

                    connection.Close();
                }
            }

            return "ID: " + id + "\nName: " + name + "\nMessage: " + message;
        }

        public string Add(int id, string name, string message)
        {
            if (id <= 0)
                return "ID should be more zero";
            else if (string.IsNullOrEmpty(name))
                return "Name is empty";
            else if (string.IsNullOrEmpty(message))
                return "Message is empty";

            bool flag;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select ID from Informations where ID =@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    flag = command.ExecuteScalar() == null ? false : true;

                    if (flag == true ) return "This Information is exist";

                    command.CommandText = "insert into Informations values (@id, @name, @message)";
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@message", SqlDbType.NVarChar).Value = message;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return "Information is added";
        }

        public string Delete(int id)
        {
            if (id == 0)
                return "ID should be more zero";

            bool flag;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select ID from Informations where ID =@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    flag = command.ExecuteScalar() == null ? false : true;

                    if (!flag) return "Information isn't exist";

                    command.CommandText = "delete from Informations where ID = @id";
                    command.ExecuteNonQuery();
                    
                    connection.Close();
                }
            }
           
            return "Information is deleted";

        }

        public string Update (int id, string message)
        {
            if (id <= 0) 
                return "ID should be more zero";
            if (string.IsNullOrEmpty(message))
                return "Message is empty";

            bool flag;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select ID from Informations where ID =@id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    flag = command.ExecuteScalar() == null ? false : true;

                    if (!flag) return "This Information isn't exist";

                    command.CommandText = "update Informations set message = @message where ID = @id";
                    command.Parameters.Add("@message", SqlDbType.NVarChar).Value = message;
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return "Information is updated";
        }

        public void DeleteAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "delete from Informations";
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
