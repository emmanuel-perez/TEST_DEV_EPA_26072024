using System.Data;
using Dapper;
using TEST_DEV_EPA_26072024.context;
using TEST_DEV_EPA_26072024.contracts;

namespace TEST_DEV_EPA_26072024.repository {

    public class PersonaFisicaRepository: IPersonaFisicaRepository {

        private readonly DapperContext _context;

        public PersonaFisicaRepository (DapperContext context){
            _context = context;
        }

        public async Task<DateTime>GetDate(){
            string query = "SELECT GETDATE()";

            IDbConnection connection = _context.CreateConnection();
            DateTime currentDate = await connection.QueryFirstOrDefaultAsync<DateTime>(query);
            return currentDate;
        }

    }

}