using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MapboxApi.Data
{
    public class DataContext
    {
        private readonly string _connectionString;

        public DataContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MapboxGIS");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
