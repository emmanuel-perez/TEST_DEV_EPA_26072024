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
            string sql = @$"
                EXECUTE dbo.sp_AgregarPersonaFisica 
                @Nombre='{personaFisicaToAdd.Nombre}',
                @ApellidoPaterno='{personaFisicaToAdd.ApellidoPaterno}',
                @ApellidoMaterno='{personaFisicaToAdd.ApellidoMaterno}',
                @RFC='{personaFisicaToAdd.RFC}',
                @FechaNacimiento='{personaFisicaToAdd.FechaNacimiento}',
                @UsuarioAgrega=0
            ";

            IDbConnection connection = _context.CreateConnection();
            int rowsAffected = await connection.ExecuteAsync(sql);
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
            string query = @$"
                SELECT * FROM dbo.Tb_PersonasFisicas
                WHERE IdPersonaFisica = {personaFisicaId} AND Activo = 1;
            ";

            IDbConnection connection = _context.CreateConnection();
            PersonaFisica? personaFisica = await connection.QueryFirstOrDefaultAsync<PersonaFisica>(query);
            return personaFisica;
        }

        public async Task<bool> DeletePersonaFisica(int personaFisicaId)
        {
            //  !   IMPORTANTE
            //  EL StoredProcedure de eliminar del arhivo de scripts del archivo de las pruebas tiene 
            //  la condicion invertida ya que debería especificar que si NO existe el usuario Activo 
            //  marque el error:

            // IF EXISTS <---- Debería ser: IF NOT EXISTS
            // (
            //     SELECT*
            //     FROM dbo.Tb_PersonasFisicas
            //     WHERE IdPersonaFisica = @IdPersonaFisica
            //           AND Activo = 1
            // )
            // BEGIN
            //     SELECT @ERROR = 'La persona fisica no existe.';
            //     THROW 50000, @ERROR, 1;
            // END;

            //  Entonces haré mi propio sql script:

            string sql = @$"
                IF EXISTS (SELECT 1 FROM dbo.Tb_PersonasFisicas WHERE IdPersonaFisica = { personaFisicaId } AND Activo = 1)
                    BEGIN
                        UPDATE dbo.Tb_PersonasFisicas
                        SET 
                            Activo = 0
                        WHERE IdPersonaFisica = { personaFisicaId };
                    END
                    ELSE
                    BEGIN
                        THROW 50000, 'El registro con IdPersonaFisica = { personaFisicaId } no existe o no está activo.', 1;
                    END;
            ";

            using IDbConnection connection = _context.CreateConnection();
            int rowsAffected = await connection.ExecuteAsync(sql, new { IdPersonaFisica = personaFisicaId });
            return rowsAffected > 0;
        }

    }

}