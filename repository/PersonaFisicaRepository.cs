using System.Data;
using Dapper;
using TEST_DEV_EPA_26072024.context;
using TEST_DEV_EPA_26072024.contracts;
using TEST_DEV_EPA_26072024.dtos;
using TEST_DEV_EPA_26072024.models;

namespace TEST_DEV_EPA_26072024.repository
{

    public class PersonaFisicaRepository : IPersonaFisicaRepository
    {

        private readonly DapperContext _context;

        public PersonaFisicaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPersonaFisica(AddPersonaFisicaDto personaFisicaToAdd)
        {
            string sql = @"
                EXECUTE dbo.sp_AgregarPersonaFisica 
                    @Nombre,
                    @ApellidoPaterno,
                    @ApellidoMaterno,
                    @RFC,
                    @FechaNacimiento,
                    @UsuarioAgrega
            ";

            var parameters = new
            {
                Nombre = personaFisicaToAdd.Nombre,
                ApellidoPaterno = personaFisicaToAdd.ApellidoPaterno,
                ApellidoMaterno = personaFisicaToAdd.ApellidoMaterno,
                RFC = personaFisicaToAdd.RFC,
                FechaNacimiento = personaFisicaToAdd.FechaNacimiento,
                UsuarioAgrega = 0 
            };

            IDbConnection connection = _context.CreateConnection();
            int rowsAffected = await connection.ExecuteAsync(sql, parameters);
            return rowsAffected > 0;
        }


        public async Task<IEnumerable<PersonaFisica>> GetAllPersonasFisicas()
        {
            string query = @"
                SELECT * FROM dbo.Tb_PersonasFisicas
                WHERE Activo = 1;
            ";

            IDbConnection connection = _context.CreateConnection();
            return await connection.QueryAsync<PersonaFisica>(query);
        }

        public async Task<PersonaFisica> GetPersonaFisicaById(int personaFisicaId)
        {
            string query = @"
                SELECT * FROM dbo.Tb_PersonasFisicas
                WHERE IdPersonaFisica = @IdPersonaFisica AND Activo = 1;
            ";

            IDbConnection connection = _context.CreateConnection();
            PersonaFisica? personaFisica = await connection.QueryFirstOrDefaultAsync<PersonaFisica>(query, new { IdPersonaFisica = personaFisicaId });
            return personaFisica;
        }


        public async Task<bool> DeletePersonaFisica(int personaFisicaId)
        {
            string sql = @"
                IF EXISTS (SELECT 1 FROM dbo.Tb_PersonasFisicas WHERE IdPersonaFisica = @IdPersonaFisica AND Activo = 1)
                BEGIN
                    UPDATE dbo.Tb_PersonasFisicas
                    SET 
                        Activo = 0
                    WHERE IdPersonaFisica = @IdPersonaFisica;
                END
                ELSE
                BEGIN
                    THROW 50000, 'El registro con IdPersonaFisica = @IdPersonaFisica no existe o no está activo.', 1;
                END;
            ";

            using IDbConnection connection = _context.CreateConnection();
            int rowsAffected = await connection.ExecuteAsync(sql, new { IdPersonaFisica = personaFisicaId });
            return rowsAffected > 0;
        }


        public async Task<bool> UpdatePersonaFisica(int personaFisicaId, UpdatePersonaFisicaDto fieldsToUpdate)
        {
            string sqlSelect = @"
                SELECT * FROM dbo.Tb_PersonasFisicas
                WHERE IdPersonaFisica = @IdPersonaFisica AND Activo = 1; 
            ";

            IDbConnection connection = _context.CreateConnection();

            // Explicitly mapping the result to PersonaFisica type
            PersonaFisica personaFisicaToUpdate = await connection.QueryFirstOrDefaultAsync<PersonaFisica>(sqlSelect, new { IdPersonaFisica = personaFisicaId });

            if (personaFisicaToUpdate == null)
            {
                return false;
            }

            string sqlUpdate = @"
            UPDATE dbo.Tb_PersonasFisicas
                SET 
                    Nombre = @Nombre,
                    ApellidoPaterno = @ApellidoPaterno,
                    ApellidoMaterno = @ApellidoMaterno,
                    RFC = @RFC,
                    FechaNacimiento = @FechaNacimiento,
                    UsuarioAgrega = @UsuarioAgrega,
                    FechaActualizacion = GETDATE()
                WHERE IdPersonaFisica = @IdPersonaFisica
            ";

            var parameters = new
            {
                Nombre = fieldsToUpdate.Nombre != "" ? fieldsToUpdate.Nombre : personaFisicaToUpdate.Nombre,
                ApellidoPaterno = fieldsToUpdate.ApellidoPaterno != "" ? fieldsToUpdate.ApellidoPaterno : personaFisicaToUpdate.ApellidoPaterno,
                ApellidoMaterno = fieldsToUpdate.ApellidoMaterno != "" ? fieldsToUpdate.ApellidoMaterno : personaFisicaToUpdate.ApellidoMaterno,
                RFC = fieldsToUpdate.RFC != "" ? fieldsToUpdate.RFC : personaFisicaToUpdate.RFC,
                FechaNacimiento = fieldsToUpdate.FechaNacimiento.HasValue ? fieldsToUpdate.FechaNacimiento : personaFisicaToUpdate.FechaNacimiento,
                UsuarioAgrega = fieldsToUpdate.UsuarioAgrega.HasValue && fieldsToUpdate.UsuarioAgrega.Value != 0 ? fieldsToUpdate.UsuarioAgrega : personaFisicaToUpdate.UsuarioAgrega,
                IdPersonaFisica = personaFisicaId
            };

            int rowsAffected = await connection.ExecuteAsync(sqlUpdate, parameters);
            return rowsAffected > 0;
        }


    }

}
