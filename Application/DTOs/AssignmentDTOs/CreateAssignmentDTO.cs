using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.DTOs.AssignmentDTOs
{
    public class CreateAssignmentDTO
    {
        [Required]
        public required string Title { get; set; }
        [AllowNull]
        public string? Description { get; set; }
    }
}
