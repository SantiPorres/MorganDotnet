using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ProjectDTOs
{
    public class CreateProjectDTO
    {
        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
