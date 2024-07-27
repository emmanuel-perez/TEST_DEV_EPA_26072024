
using Microsoft.AspNetCore.Mvc;
using TEST_DEV_EPA_26072024.contracts;
using TEST_DEV_EPA_26072024.dtos;

namespace TEST_DEV_EPA_26072024.controllers {

    [Route("api/personas-fisicas")]
    [ApiController]
    public class PersonaFisicaController: ControllerBase {
        private readonly IPersonaFisicaRepository _repository;

        public PersonaFisicaController (IPersonaFisicaRepository repository) {
            _repository = repository;
        }

        [HttpGet("get-date")]
        public async Task<IActionResult>GetDate(){
            try
            {
                DateTime date = await _repository.GetDate();
                return Ok(date);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "failed to return ");
            }
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

                if ( personaFisicaToAdd.RFC.Length != 13 ) {
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

    }
}







