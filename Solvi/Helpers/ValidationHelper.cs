using System.ComponentModel.DataAnnotations;

namespace Solvi.Helpers
{
    public class PasswordValidation : RequiredAttribute
    {
        public PasswordValidation()
        {
            this.ErrorMessage = Resources.Resource.InvalidPasswordError;
        }

        public override bool IsValid(object? value)
        {
            string passwordValue = value as string ?? "";

            // Ensure the password is not null or empty
            if (!string.IsNullOrEmpty(passwordValue))
            {
                bool hasDigit = passwordValue.Any(char.IsDigit);// At least one digit
                bool hasUppercase = passwordValue.Any(char.IsUpper);// At least one uppercase
                bool hasLowercase = passwordValue.Any(char.IsLower);// At least one lowercase
                bool hasNonAlphanumeric = passwordValue.Any(ch => !char.IsLetterOrDigit(ch)); // At least one non-alphanumeric characters

                // Validate the password length and complexity
                if (passwordValue.Length >= 6 && hasDigit && hasUppercase && hasLowercase && hasNonAlphanumeric)
                {
                    return true; // Password is valid
                }
            }

            return false; // Password is invalid
        }
    }
}
