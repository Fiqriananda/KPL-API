namespace ExpedisiPaketAPI.DTOs.Auth
{
    public class RegisterDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public List<string>? Roles { get; set; }
    }
}
