namespace Application.DTOs.ProjectDTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
