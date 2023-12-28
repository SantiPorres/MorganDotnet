using Domain.Entities;

namespace Application.DTOs.UserDTOs
{
    public class UserNavigationDTO
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }

        public ICollection<UserProject>? UserProjects { get; set; }
    }
}
