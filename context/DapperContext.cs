using System.Data;
using System.Data.SqlClient;

namespace TEST_DEV_EPA_26072024.context
{
    public class DapperContext
    {

        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DapperContext(IConfiguration config)
        {
            _config = config;
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

    }


}