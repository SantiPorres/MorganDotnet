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

        //[HttpGet]
        //public async Task<PagedResponse<ProjectDTO>> GetUserProjects()
        //{
        //    int user_id = _tokenService.GetUserIdFromJwt(HttpContext);
        //    return await _projectService.GetProjectsByUser(user_id);
        //}

        [HttpPost]
        public async Task<Response<ProjectDTO>> CreateProject(CreateProjectDTO body)
        {
            int user_id = _tokenService.GetUserIdFromJwt(HttpContext);
            ProjectDTO projectDto = await _projectService.CreateProject(user_id, body);
            return new Response<ProjectDTO>(projectDto);
        }
    }
}
