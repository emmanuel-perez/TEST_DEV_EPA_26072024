using System.ComponentModel.DataAnnotations;

namespace TEST_DEV_EPA_26072024.utilities
{
    public static class ValidationUtilities
    {
        public static ValidationResult ValidateFechaNacimiento(DateTime? fechaNacimiento, ValidationContext context)
        {
            if (fechaNacimiento == null)
            {
                return ValidationResult.Success;
            }

            if (fechaNacimiento.Value > DateTime.Now)
            {
                return new ValidationResult("La fecha de nacimiento no puede ser una fecha futura.");
            }

            return ValidationResult.Success;
        }
    }
}
