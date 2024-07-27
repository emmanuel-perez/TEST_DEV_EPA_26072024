using System.ComponentModel.DataAnnotations;

namespace TEST_DEV_EPA_26072024.utilities
{
    public static class ValidationUtilities
    {
        public static ValidationResult ValidateFechaNacimiento(DateTime fechaNacimiento, ValidationContext context)
        {
            if (fechaNacimiento > DateTime.Now)
            {
                return new ValidationResult("FechaNacimiento no puede ser en el futuro");
            }
            return ValidationResult.Success!;
        }
    }
}
