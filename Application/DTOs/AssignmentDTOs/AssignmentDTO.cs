namespace Application.DTOs.AssignmentDTOs
{
    public class AssignmentDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Guid ProjectId { get; set; }
        public Guid? UserId { get; set; }
    }
}
