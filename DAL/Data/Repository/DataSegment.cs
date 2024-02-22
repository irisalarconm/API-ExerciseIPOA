using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repository
{
    public class DataSegment : IGenericRepository<Segment>
    {
        private readonly IConfiguration _configuration;
        ILogger<DataSegment> _logger;

        public DataSegment(IConfiguration configuration, ILogger<DataSegment> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public SqlConnection GetConnection()
        {
            string connection = _configuration.GetConnectionString("ExerciseApi");
            return new SqlConnection(connection);
        }
        public bool Create(Segment model)
        {
            int x = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_InsertSegment", connection);
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
                    SqlCommand cmd = new SqlCommand("sp_DeleteSegment", connection);
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

        public List<Segment> GetAll()
        {
            try
            {
                List<Segment> segmentList = new List<Segment>();

                using (GetConnection())
                {
                    SqlCommand cmd = GetConnection().CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetSegment";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dtSegments = new DataTable();

                    GetConnection().Open();
                    adapter.Fill(dtSegments);
                    GetConnection().Close();



                    foreach (DataRow dr in dtSegments.Rows)
                    {
                        //byte[] videoData = (byte[])dr["Data"];

                        segmentList.Add(new Segment
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                        });
                    }
                }

                return segmentList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public Segment GetById(int id)
        {
            Segment segment = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetSegmentById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        byte[] videoData = (byte[])reader["Data"];

                        if (reader.Read())
                        {
                            segment = new Segment
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
                _logger.LogError("Error en GetExerciseById: " + ex.Message);
            }

            return segment;
        }

        public bool Update(Segment model)
        {
            int x = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_UpdateSegment", connection);
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
