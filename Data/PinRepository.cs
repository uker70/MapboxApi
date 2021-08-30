using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MapboxApi.DTOs;


namespace MapboxApi.Data
{
    public class PinRepository : IApi
    {
        private readonly Random rnd = new Random();
        private readonly DataContext _context;

        public PinRepository(DataContext context)
        {
            _context = context;
        }
        
        public IEnumerable<PinReadDTO> GetPins()
        {
            List<PinReadDTO> pins = new List<PinReadDTO>();

            using (IDbConnection connection = _context.CreateConnection())
            {
                SqlCommand command = new SqlCommand("get_pins", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    PinReadDTO pin = new PinReadDTO();
                    while (dataReader.Read())
                    {
                        pins.Add(new PinReadDTO()
                        {
                            Id = dataReader.GetInt32(0),
                            Type = dataReader.GetString(1),
                            Longitude = dataReader.GetFloat(2),
                            Latitude = dataReader.GetFloat(3),
                            Title = dataReader.GetString(4),
                            Description = dataReader.GetString(5)
                        });
                    }
                }
            }

            return pins;
        }

        public PinReadDTO GetPin()
        {
            List<PinReadDTO> pins = new List<PinReadDTO>();

            using (IDbConnection connection = _context.CreateConnection())
            {
                SqlCommand command = new SqlCommand("get_pins", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        pins.Add(new PinReadDTO()
                        {
                            Id = dataReader.GetInt32(0),
                            Type = dataReader.GetString(1),
                            Longitude = dataReader.GetFloat(2),
                            Latitude = dataReader.GetFloat(3),
                            Title = dataReader.GetString(4),
                            Description = dataReader.GetString(5)
                        });
                    }
                }
                dataReader.Close();
            }

            return pins[rnd.Next(0, pins.Count)];
        }

        public PinReadDTO GetPin(int id)
        {
            PinReadDTO pin = null;

            using (IDbConnection connection = _context.CreateConnection())
            {
                SqlCommand command = new SqlCommand("get_pin_by_id", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PinId", SqlDbType.Int).Value = id;

                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        pin = new PinReadDTO()
                        {
                            Id = dataReader.GetInt32(0),
                            Type = dataReader.GetString(1),
                            Longitude = dataReader.GetFloat(2),
                            Latitude = dataReader.GetFloat(3),
                            Title = dataReader.GetString(4),
                            Description = dataReader.GetString(5)
                        };
                    }
                }
                dataReader.Close();
            }

            return pin;
        }

        public PinReadDTO GetPin(string title)
        {
            PinReadDTO pin = null;

            using (IDbConnection connection = _context.CreateConnection())
            {
                SqlCommand command = new SqlCommand("get_pin_by_title", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PinTitle", SqlDbType.NVarChar).Value = title;

                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        pin = new PinReadDTO()
                        {
                            Id = dataReader.GetInt32(0),
                            Type = dataReader.GetString(1),
                            Longitude = dataReader.GetFloat(2),
                            Latitude = dataReader.GetFloat(3),
                            Title = dataReader.GetString(4),
                            Description = dataReader.GetString(5)
                        };
                    }
                }
                dataReader.Close();
            }

            return pin;
        }

        public PinReadDTO GetPin(float longitude, float latitude)
        {
            PinReadDTO pin = null;

            using (IDbConnection connection = _context.CreateConnection())
            {
                SqlCommand command = new SqlCommand("get_pin_by_location", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Long", SqlDbType.Float).Value = longitude;
                command.Parameters.Add("@Lat", SqlDbType.Float).Value = latitude;

                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        pin = new PinReadDTO()
                        {
                            Id = dataReader.GetInt32(0),
                            Type = dataReader.GetString(1),
                            Longitude = dataReader.GetFloat(2),
                            Latitude = dataReader.GetFloat(3),
                            Title = dataReader.GetString(4),
                            Description = dataReader.GetString(5)
                        };
                    }
                }
                dataReader.Close();
            }

            return pin;
        }

        public int CreatePin(PinCreateDTO createPin)
        {
            int id = 0;
            using (IDbConnection connection = _context.CreateConnection())
            {
                SqlCommand command = new SqlCommand("create_pin", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Type", SqlDbType.NVarChar).Value = createPin.Type;
                command.Parameters.Add("@Long", SqlDbType.Float).Value = createPin.Longitude;
                command.Parameters.Add("@Lat", SqlDbType.Float).Value = createPin.Latitude;

                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        id = dataReader.GetInt32(0);
                    }
                }
                dataReader.Close();
                connection.Close();

                command = new SqlCommand("create_description", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PinId", SqlDbType.Int).Value = id;
                command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = createPin.Title;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = createPin.Description;

                connection.Open();
                command.ExecuteNonQuery();
            }

            return id;
        }

        public void UpdatePin(PinUpdateDTO updatePin)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                SqlCommand command = new SqlCommand("update_pin", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PinId", SqlDbType.Int).Value = updatePin.Id;
                command.Parameters.Add("@Type", SqlDbType.NVarChar).Value = updatePin.Type;
                command.Parameters.Add("@Long", SqlDbType.Float).Value = updatePin.Longitude;
                command.Parameters.Add("@Lat", SqlDbType.Float).Value = updatePin.Latitude;
                command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = updatePin.Title;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = updatePin.Description;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeletePin(int id)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                SqlCommand command = new SqlCommand("delete_pin", (SqlConnection)connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PinId", SqlDbType.Int).Value = id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
