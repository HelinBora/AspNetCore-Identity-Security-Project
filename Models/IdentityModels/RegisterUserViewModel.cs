namespace NotikaIdentityEmail.Models.IdentityModels
{
    public class RegisterUserViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
