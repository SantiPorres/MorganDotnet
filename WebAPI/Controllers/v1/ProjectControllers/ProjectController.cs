using Application.DTOs.ProjectDTOs;
using Application.Filters;
using Application.Interfaces.IServices;
using Application.Wrappers;
using Domain.CustomEntities;
using Domain.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Common;

namespace WebAPI.Controllers.v1.ProjectControllers
{
    [Authorize]
    public class ProjectController : BaseApiController
    {
        private readonly IProjectService _projectService;
        private readonly IUserProjectService _userProjectService;
        private readonly ITokenService _tokenService;

        public ProjectController(
            IProjectService projectService,
            ITokenService tokenService,
            IUserProjectService userProjectService
        )
        {
            _projectService = projectService;
            _tokenService = tokenService;
            _userProjectService = userProjectService;
        }

        [HttpGet("userProjects")]
        public async Task<PagedResponse<PagedList<ProjectDTO>>> GetProjectsByUserId([FromQuery] PaginationQueryParameters filters)
        {
            try
            {
                Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
                PagedList<ProjectDTO> pagedProjects = await _projectService.GetAllProjectsByUserId(filters, userId);
                string? message = null;
                if (pagedProjects.TotalCount == 0)
                    message = "The user does not have any project";
                return new PagedResponse<PagedList<ProjectDTO>>(
                        pagedProjects,
                        message: message,
                        totalCount: pagedProjects.TotalCount,
                        pagedProjects.PageSize,
                        pagedProjects.CurrentPage,
                        pagedProjects.HasNextPage,
                        pagedProjects.HasPreviousPage,
                        pagedProjects.NextPageNumber,
                        pagedProjects.PreviousPageNumber
                );
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is UnauthorizedAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
                || ex is KeyNotFoundException
            )
            { throw; }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }

        [HttpGet]
        [Route("{projectId}")]
        public async Task<Response<ProjectDTO>> GetProjectById([FromRoute] Guid projectId)
        {
            try
            {
                Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
                bool userAndProjectAreRelated = await _userProjectService.UserAndProjectAreRelated(projectId, userId);
                if (userAndProjectAreRelated == false)
                    throw new UnauthorizedAccessException("The user is not related to the project");
                ProjectDTO projectDto = await _projectService.GetProjectById(projectId);
                return new Response<ProjectDTO>(projectDto);
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is UnauthorizedAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
                || ex is KeyNotFoundException
            )
            { throw; }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }

        [HttpPost("add")]
        public async Task<Response<ProjectDTO>> CreateProject(CreateProjectDTO body)
        {
            try
            {
                Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
                ProjectDTO projectDto = await _projectService.CreateProject(userId, body);
                return new Response<ProjectDTO>(projectDto);
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is UnauthorizedAccessException
                || ex is FluentValidation.ValidationException
                || ex is BusinessException
                || ex is KeyNotFoundException
            )
            { throw; }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
        }
    }
}
