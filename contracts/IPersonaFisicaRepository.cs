
using TEST_DEV_EPA_26072024.dtos;
using TEST_DEV_EPA_26072024.models;

namespace TEST_DEV_EPA_26072024.contracts {

    public interface IPersonaFisicaRepository {
        //  *   Task to test connection to DB
        Task<DateTime>GetDate();
        Task<bool>AddPersonaFisica(AddPersonaFisicaDto personaFisicaToAdd);
        Task<IEnumerable<PersonaFisica>>GetAllPersonasFisicas();
        Task<PersonaFisica>GetPersonaFisicaById(int personaFisicaId);
    }
}
