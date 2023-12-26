namespace Application.DTOs.ProjectDTOs
{
    public class CreateProjectDTO
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
