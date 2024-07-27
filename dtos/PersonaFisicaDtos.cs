
using System.ComponentModel.DataAnnotations;
using TEST_DEV_EPA_26072024.utilities;

namespace TEST_DEV_EPA_26072024.dtos
{

    public class AddPersonaFisicaDto
    {
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string ApellidoPaterno { get; set; }

        [Required]
        public string ApellidoMaterno { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El RFC debe tener 13 caracteres")]
        public string RFC { get; set; }

        [CustomValidation(typeof(ValidationUtilities), nameof(ValidationUtilities.ValidateFechaNacimiento))]
        public DateTime FechaNacimiento { get; set; }
        public int UsuarioAgrega { get; set; } = 0;

        public AddPersonaFisicaDto()
        {
            Nombre ??= "";
            ApellidoPaterno ??= "";
            ApellidoMaterno ??= "";
            RFC ??= "";
        }

    }

    public class UpdatePersonaFisicaDto
    {
        public string Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "El RFC debe tener 13 caracteres")]
        public string? RFC { get; set; }
        
        // [CustomValidation(typeof(ValidationUtilities), nameof(ValidationUtilities.ValidateFechaNacimiento))]
        public DateTime? FechaNacimiento { get; set; }
        public int? UsuarioAgrega { get; set; }

        public UpdatePersonaFisicaDto()
        {
            Nombre = "";
            ApellidoPaterno = "";
            ApellidoMaterno = "";
            RFC = "0000000000000";
            FechaNacimiento = null;
            UsuarioAgrega = 0;
        }
    }

}