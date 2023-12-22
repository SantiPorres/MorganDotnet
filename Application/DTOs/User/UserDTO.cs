namespace Application.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }
    }
}
