
using TEST_DEV_EPA_26072024.dtos;

namespace TEST_DEV_EPA_26072024.contracts {

    public interface IPersonaFisicaRepository {
        //  *   Task to test connection to DB
        Task<DateTime>GetDate();
        Task<bool>AddPersonaFisica(AddPersonaFisicaDto personaFisicaToAdd);
    }
}
