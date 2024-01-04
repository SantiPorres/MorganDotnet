using Application.DTOs.ProjectDTOs;
using Application.Filters;
using Application.Wrappers;
using Domain.CustomEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Common;

namespace WebAPI.Controllers.v1.ProjectControllers
{
    [Authorize]
    public class ProjectController : BaseApiController
    {
        //private readonly IProjectService _projectService;
        //private readonly IUserProjectService _userProjectService;
        //private readonly ITokenService _tokenService;

        //public ProjectController(
        //    IProjectService projectService,
        //    ITokenService tokenService,
        //    IUserProjectService userProjectService
        //)
        //{
        //    _projectService = projectService;
        //    _tokenService = tokenService;
        //    _userProjectService = userProjectService;
        //}

        //[HttpGet("userProjects")]
        //public async Task<PagedResponse<PagedList<ProjectDTO>>> GetProjectsByUserId([FromQuery] PaginationQueryParameters filters)
        //{
        //    Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
        //    PagedList<ProjectDTO> pagedProjects = await _projectService.GetProjectsByUserId(filters, userId);
        //    string? message = null;
        //    if (pagedProjects.TotalCount == 0)
        //        message = "The user does not have any project";
        //    return new PagedResponse<PagedList<ProjectDTO>>(
        //            pagedProjects,
        //            message: message,
        //            totalCount: pagedProjects.TotalCount,
        //            pagedProjects.PageSize,
        //            pagedProjects.CurrentPage,
        //            pagedProjects.HasNextPage,
        //            pagedProjects.HasPreviousPage,
        //            pagedProjects.NextPageNumber,
        //            pagedProjects.PreviousPageNumber
        //    );
        //}

        //[HttpGet("single")]
        //public async Task<Response<ProjectNavigationDTO>> GetProjectById([FromQuery] Guid projectId)
        //{
        //    Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
        //    bool isRelated = await _userProjectService.VerifyRelation(projectId, userId);
        //    if (isRelated)
        //    {
        //        ProjectNavigationDTO projectDto = await _projectService.GetProjectById(projectId);
        //        return new Response<ProjectNavigationDTO>(projectDto);
        //    }
        //    throw new UnauthorizedAccessException("The user is not related to the project");
        //}

        //[HttpPost]
        //public async Task<Response<ProjectDTO>> CreateProject(CreateProjectDTO body)
        //{
        //    Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
        //    ProjectDTO projectDto = await _projectService.CreateProject(userId, body);
        //    return new Response<ProjectDTO>(projectDto);
        //}
    }
}
