namespace ExpedisiPaketAPI.DTOs.Auth
{
    public class AuthResponseDto
    {
        public required string Token { get; set; }
        public required string Role { get; set; }
        public DateTime Expiration { get; set; }
    }
}
