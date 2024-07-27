
using Microsoft.AspNetCore.Mvc;
using TEST_DEV_EPA_26072024.contracts;
using TEST_DEV_EPA_26072024.dtos;
using TEST_DEV_EPA_26072024.models;

namespace TEST_DEV_EPA_26072024.controllers
{

    [Route("api/personas-fisicas")]
    [ApiController]
    public class PersonaFisicaController : ControllerBase
    {
        private readonly IPersonaFisicaRepository _repository;

        public PersonaFisicaController(IPersonaFisicaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddPersonaFisica([FromBody] AddPersonaFisicaDto personaFisicaToAdd)
        {
            if (personaFisicaToAdd == null)
            {
                return BadRequest("Invalid input");
            }

            try
            {

                if (personaFisicaToAdd.RFC.Length != 13)
                {
                    return StatusCode(400, "El RFC debe de tener 13 caracteres");
                }


                bool userAdded = await _repository.AddPersonaFisica(personaFisicaToAdd);
                return Ok("El usuario se ha guardado en la base de datos");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "error al guardar PersonaFisica en la base de datos");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllPersonasFisicas()
        {
            try
            {
                IEnumerable<PersonaFisica> personaFisicas = await _repository.GetAllPersonasFisicas();
                return Ok(personaFisicas);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Error al consultar datos de PersonasFisicas");
            }
        }

        [HttpGet("/{personaFisicaId}")]
        public async Task<IActionResult> GetPersonaFisicaById(int personaFisicaId)
        {

            if (personaFisicaId <= 0)
            {
                return BadRequest("El ID de PersonaFisica debe de ser un numero entero positivo");
            }

            try
            {
                PersonaFisica personaFisica = await _repository.GetPersonaFisicaById(personaFisicaId);

                if (personaFisica == null)
                {
                    return BadRequest($"PersonaFisica con id: {personaFisicaId} no encontrada");
                }

                return Ok(personaFisica);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Error al consultar datos de PersonaFisica");
            }
        }

        [HttpDelete("{personaFisicaId}")]
        public async Task<IActionResult> DeletePersonaFisica(int personaFisicaId)
        {
            if (personaFisicaId <= 0)
            {
                return BadRequest("El ID de PersonaFisica debe de ser un numero entero positivo");
            }

            try
            {
                bool personaFisicaDeleted = await _repository.DeletePersonaFisica(personaFisicaId);

                if (!personaFisicaDeleted)
                {
                    return NotFound($"PersonaFisica con id: {personaFisicaId} no encontrada o ya está inactiva.");
                }
                return Ok("Persona fisica eliminada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Error al eliminar PersonaFisica con id {personaFisicaId}");
            }
        }

        [HttpPut("{personaFisicaId}")]
        public async Task<IActionResult>UpdatePersonaFisica(int personaFisicaId, UpdatePersonaFisicaDto fieldsToUpdate){

            if (personaFisicaId <= 0) {
                return BadRequest("El ID de PersonaFisica debe de ser un entero positivo");
            }

            try
            {
                bool personaFisicaUpdated = await _repository.UpdatePersonaFisica(personaFisicaId, fieldsToUpdate);
                return Ok("PersonaFisica actualizada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Error al actualizar PersonaFisica");
            }
        }


    }
}







