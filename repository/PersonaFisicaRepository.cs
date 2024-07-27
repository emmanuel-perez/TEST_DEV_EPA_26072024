using System.Data;
using Dapper;
using TEST_DEV_EPA_26072024.context;
using TEST_DEV_EPA_26072024.contracts;
using TEST_DEV_EPA_26072024.dtos;
using TEST_DEV_EPA_26072024.models;

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

        public async Task<bool>AddPersonaFisica(AddPersonaFisicaDto personaFisicaToAdd){
            string sql = @$"
                EXECUTE dbo.sp_AgregarPersonaFisica 
                @Nombre='{ personaFisicaToAdd.Nombre }',
                @ApellidoPaterno='{ personaFisicaToAdd.ApellidoPaterno }',
                @ApellidoMaterno='{ personaFisicaToAdd.ApellidoMaterno }',
                @RFC='{ personaFisicaToAdd.RFC }',
                @FechaNacimiento='{ personaFisicaToAdd.FechaNacimiento }',
                @UsuarioAgrega=0
            ";
            
            IDbConnection connection = _context.CreateConnection();
            int rowsAffected = await connection.ExecuteAsync(sql);
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<PersonaFisica>>GetAllPersonasFisicas(){
            string query = @"
                SELECT * FROM dbo.Tb_PersonasFisicas
                WHERE Activo = 1;
            ";

            IDbConnection connection = _context.CreateConnection();
            return await connection.QueryAsync<PersonaFisica>(query);
        }
        
        public async Task<PersonaFisica>GetPersonaFisicaById(int personaFisicaId){
            string query = @$"
                SELECT * FROM dbo.Tb_PersonasFisicas
                WHERE IdPersonaFisica = { personaFisicaId } AND Activo = 1;
            ";

            IDbConnection connection = _context.CreateConnection();
            PersonaFisica? personaFisica = await connection.QueryFirstOrDefaultAsync<PersonaFisica>(query);
            return personaFisica;
        }

    }

}