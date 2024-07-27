
using TEST_DEV_EPA_26072024.dtos;
using TEST_DEV_EPA_26072024.models;

namespace TEST_DEV_EPA_26072024.contracts {

    public interface IPersonaFisicaRepository {
        Task<bool>AddPersonaFisica(AddPersonaFisicaDto personaFisicaToAdd);
        Task<IEnumerable<PersonaFisica>>GetAllPersonasFisicas();
        Task<PersonaFisica>GetPersonaFisicaById(int personaFisicaId);
        Task<bool>DeletePersonaFisica(int personaFisicaId);
        Task<bool> UpdatePersonaFisica(int personaFisicaId, UpdatePersonaFisicaDto fieldsToUpdate);
    }
}
