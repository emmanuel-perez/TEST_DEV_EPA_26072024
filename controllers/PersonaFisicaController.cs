
using Microsoft.AspNetCore.Mvc;
using TEST_DEV_EPA_26072024.contracts;

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


    }
}







