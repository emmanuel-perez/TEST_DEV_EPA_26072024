
using System.ComponentModel.DataAnnotations;

namespace TEST_DEV_EPA_26072024.dtos {

    public class AddPersonaFisicaDto
    {
        public DateTime FechaRegistro {get; set;} 
        public DateTime FechaActualizacion {get; set;}

        [Required]
        public string Nombre {get; set;}

        [Required]
        public string ApellidoPaterno {get; set;}

        [Required]
        public string ApellidoMaterno {get; set;}

        [Required]
        [StringLength(13, MinimumLength=13, ErrorMessage="El RFC debe tener 13 caracteres")]
        public string RFC {get; set;}

        [CustomValidation(typeof(AddPersonaFisicaDto),"ValidateFechaNacimiento")]
        public DateTime FechaNacimiento {get; set;}
        public int UsuarioAgrega {get; set;} = 0;

        public AddPersonaFisicaDto(){
            Nombre??="";
            ApellidoPaterno??="";
            ApellidoMaterno??="";
            RFC??="";
        }

        public static ValidationResult ValidateFechaNacimiento(DateTime fechaNacimiento){
            if (fechaNacimiento > DateTime.Now){
                return new ValidationResult("FechaNacimiento no puede ser en el futuro");
            }
            return ValidationResult.Success!;
        }

    }



}