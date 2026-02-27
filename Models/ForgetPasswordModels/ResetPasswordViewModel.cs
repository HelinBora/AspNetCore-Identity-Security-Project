using System.Reflection.Metadata.Ecma335;

namespace NotikaIdentityEmail.Models.ForgetPasswordModels
{
    public class ResetPasswordViewModel
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
