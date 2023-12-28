using Application.DTOs.UserDTOs;

namespace Application.Wrappers
{
    public class JwtTokenResponse
    {
        public bool Succeeded { get; set; }
        public required string Token { get; set; }

        public required UserDTO User { get; set; }
    }
}
