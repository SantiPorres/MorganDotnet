﻿using Domain.Entities;

namespace Application.DTOs.ProjectDTOs
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<UserProject>? ProjectUsers { get; set; } = null;
    }
}
