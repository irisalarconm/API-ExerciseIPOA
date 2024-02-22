using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Data.Repository
{
    public class DataExercise : IGenericRepository<Exercise>
    {
        private readonly IConfiguration _configuration;
        ILogger<DataExercise> _logger;

        public DataExercise(IConfiguration configuration, ILogger<DataExercise> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public SqlConnection GetConnection()
        {
            string connection = _configuration.GetConnectionString("ExerciseApi");
            return new SqlConnection(connection);
        }


        public bool Create(Exercise model)
        {
            int x = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_InsertExercise", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Data", model.Data);
                    cmd.Parameters.AddWithValue("@IdType", model.IdType);
                    cmd.Parameters.AddWithValue("@IdSegment", model.IdSegment);

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
                    SqlCommand cmd = new SqlCommand("sp_DeleteExercise", connection);
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

        public List<Exercise> GetAll()
        {
            try
            {
                List<Exercise> exerciseList = new List<Exercise>();

                using (GetConnection())
                {
                    SqlCommand cmd = GetConnection().CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetExercise";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dtExercises = new DataTable();

                    GetConnection().Open();
                    adapter.Fill(dtExercises);
                    GetConnection().Close();

                    

                    foreach (DataRow dr in dtExercises.Rows)
                    {

                        if (dr["Data"] != DBNull.Value)
                        {
                            byte[] videoData = (byte[])dr["Data"];

                            exerciseList.Add(new Exercise
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = dr["Name"].ToString(),
                                Description = dr["Description"].ToString(),
                                Data = videoData,
                                IdType = Convert.ToInt32(dr["IdType"]),
                                IdSegment = Convert.ToInt32(dr["IdSegment"])
                            });
                        }
                        else
                        {
                            exerciseList.Add(new Exercise
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = dr["Name"].ToString(),
                                Description = dr["Description"].ToString(),
                                Data = new byte[0],
                                IdType = Convert.ToInt32(dr["IdType"]),
                                IdSegment = Convert.ToInt32(dr["IdSegment"])
                            });
                        }
                        
                    }
                }

                return exerciseList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }

        public Exercise GetById(int id)
        {

            Exercise exercise = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetExerciseById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader["Data"] != DBNull.Value)
                        {

                            if (reader.Read())
                            {

                                byte[] videoData = (byte[])reader["Data"];
                                exercise = new Exercise
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Data = videoData,
                                    IdType = Convert.ToInt32(reader["IdType"]),
                                    IdSegment = Convert.ToInt32(reader["IdSegment"])
                                };
                            }

                        }
                        else
                        {
                            
                                exercise = new Exercise
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Data = new byte[0],
                                    IdType = Convert.ToInt32(reader["IdType"]),
                                    IdSegment = Convert.ToInt32(reader["IdSegment"])
                                };
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en GetExerciseById: " + ex.Message);
            }

            return exercise;
        }

        public bool Update(Exercise model)
        {
            int x = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_UpdateExercise", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Data", model.Data);
                    cmd.Parameters.AddWithValue("@IdType", model.IdType);
                    cmd.Parameters.AddWithValue("@IdSegment", model.IdSegment);


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
