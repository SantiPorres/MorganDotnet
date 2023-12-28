using Application.DTOs.ProjectDTOs;
using Application.Interfaces.ProjectInterfaces;
using Application.Interfaces.ServicesInterfaces;
using Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Common;

namespace WebAPI.Controllers.v1.ProjectControllers
{
    [Authorize]
    public class ProjectController : BaseApiController
    {
        private readonly IProjectService _projectService;
        private readonly ITokenService _tokenService;

        public ProjectController(
            IProjectService projectService,
            ITokenService tokenService
        )
        {
            _projectService = projectService;
            _tokenService = tokenService;
        }

        [HttpGet("single")]
        public async Task<Response<ProjectNavigationDTO>> GetProjectById([FromQuery] Guid projectId)
        {
            ProjectNavigationDTO projectDto = await _projectService.GetProjectById(projectId);
            return new Response<ProjectNavigationDTO>(projectDto);
        }

        [HttpPost]
        public async Task<Response<ProjectDTO>> CreateProject(CreateProjectDTO body)
        {
            Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
            ProjectDTO projectDto = await _projectService.CreateProject(userId, body);
            return new Response<ProjectDTO>(projectDto);
        }
    }
}
