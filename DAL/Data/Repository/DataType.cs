using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repository
{
    public class DataType : IGenericRepository<Models.Type>
    {
        private IConfiguration _configuration;
        ILogger<DataType> _logger;

        public DataType(IConfiguration configuration, ILogger<DataType> logger)
        {
            _configuration = configuration;
            _logger = logger;   
        }
        public SqlConnection GetConnection()
        {
            string connection = _configuration.GetConnectionString("ExerciseApi");
            return new SqlConnection(connection);
        }
        public bool Create(Models.Type model)
        {
            int x = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_InsertType", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);


                    connection.Open();
                    x = cmd.ExecuteNonQuery();
                    connection.Close();

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            if (x > 0)
            {
                return true;
            }
            else
            {
                _logger.LogError("Algo paso");
                return false;
            }
        }

        public string Delete(int id)
        {
            string result = "";
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteType", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in delete: " + ex.Message);
            }

            return result;
        }

        public List<Models.Type> GetAll()
        {
            try
            {
                List<Models.Type> typeList = new List<Models.Type>();

                using (GetConnection())
                {
                    SqlCommand cmd = GetConnection().CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetType";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dtType = new DataTable();

                    GetConnection().Open();
                    adapter.Fill(dtType);
                    GetConnection().Close();



                    foreach (DataRow dr in dtType.Rows)
                    {
                        typeList.Add(new Models.Type
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString()
                        });
                    }
                }

                return typeList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }

        public Models.Type GetById(int id)
        {
            Models.Type type = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetTypeById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                   

                        if (reader.Read())
                        {
                            type = new Models.Type
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en GetTypeById: " + ex.Message);
            }

            return type;
        }

        public bool Update(Models.Type model)
        {
            int x = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_UpdateType", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);

                    connection.Open();
                    x = cmd.ExecuteNonQuery();
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en Update: " + ex.Message);
            }


            if (x > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
