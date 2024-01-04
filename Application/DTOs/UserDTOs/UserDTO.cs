using Domain.Entities;

namespace Application.DTOs.UserDTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public bool? NavigatingUserProjects { get; set; } = false;
        public ICollection<UserProject>? UserProjects { get; set; }
    }
}
