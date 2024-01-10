using Application.DTOs.AssignmentDTOs;
using Application.Interfaces.IServices;
using Application.Wrappers;
using Domain.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.v1.Common;

namespace WebAPI.Controllers.v1.AssignmentControllers
{
    [Authorize]
    public class AssignmentController : BaseApiController
    {
        private readonly IAssignmentService _assignmentService;
        private readonly ITokenService _tokenService;
        private readonly IUserProjectService _userProjectService;

        public AssignmentController(
            IAssignmentService assignmentService,
            ITokenService tokenService,
            IUserProjectService userProjectService
        )
        {
            _assignmentService = assignmentService;
            _tokenService = tokenService;
            _userProjectService = userProjectService;
        }

        [HttpGet("single")]
        public async Task<Response<AssignmentDTO>> GetAssignmentById(
            [FromQuery] Guid projectId, 
            [FromQuery] Guid assignmentId
        )
        {
            try
            {
                Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
                bool userAndProjectAreRelated = await _userProjectService.UserAndProjectAreRelated(projectId, userId);
                if (userAndProjectAreRelated == false)
                    throw new UnauthorizedAccessException("The user is not related to the project");
                AssignmentDTO assignmentDto = await _assignmentService.GetAssignmentById(assignmentId);
                return new Response<AssignmentDTO>(assignmentDto);
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
        public async Task<Response<AssignmentDTO>> CreateAssignment(
            [FromQuery] Guid projectId,
            [FromBody] CreateAssignmentDTO createAssignmentDto
        )
        {
            try
            {
                Guid userId = await _tokenService.GetUserIdFromJwt(HttpContext) ?? throw new UnauthorizedAccessException();
                bool userAndProjectAreRelated = await _userProjectService.UserAndProjectAreRelated(projectId, userId);
                if (userAndProjectAreRelated == false)
                    throw new UnauthorizedAccessException("The user is not related to the project");
                AssignmentDTO assignmentDto = await _assignmentService.AddAssignment(createAssignmentDto, projectId);
                return new Response<AssignmentDTO>(assignmentDto);
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
