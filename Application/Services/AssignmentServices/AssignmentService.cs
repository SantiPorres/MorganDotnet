using Application.DTOs.AssignmentDTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Services.AssignmentServices
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IValidator<CreateAssignmentDTO> _createAssignmentDTOValidator;
        private readonly IValidator<Assignment> _assignmentValidator;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public AssignmentService(
            IValidator<CreateAssignmentDTO> createAssignmentDTOValidator,
            IValidator<Assignment> assignmentValidator,

            IUnitOfWork unitOfWork,

            IMapper mapper
        )
        {
            _createAssignmentDTOValidator = createAssignmentDTOValidator;
            _assignmentValidator = assignmentValidator;

            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }

        public async Task<AssignmentDTO> GetAssignmentById(Guid assignmentId)
        {
            try
            {
                Assignment assignment = await _unitOfWork.Assignments.GetAsync(assignmentId);
                AssignmentDTO assignmentDto = _mapper.Map<AssignmentDTO>(assignment);
                return assignmentDto;
            }
            catch (Exception ex) when (
                ex is KeyNotFoundException
                || ex is DataAccessException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }

        public async Task<AssignmentDTO> AddAssignment(CreateAssignmentDTO body, Guid projectId)
        {
            try
            {
                ValidationResult validationResult = await _createAssignmentDTOValidator.ValidateAsync(body);
                if (validationResult.IsValid == false)
                    throw new FluentValidation.ValidationException(
                        validationResult.Errors
                    );
                Assignment assignment = _mapper.Map<Assignment>(body);
                assignment.ProjectId = projectId;
                ValidationResult validationResult2 = await _assignmentValidator.ValidateAsync(assignment);
                if (validationResult2.IsValid == false)
                    throw new FluentValidation.ValidationException(
                        validationResult2.Errors
                    );
                Assignment newAssignment = await _unitOfWork.Assignments.AddAndGetAsync(assignment);
                await _unitOfWork.Complete();
                AssignmentDTO newAssignmentDto = _mapper.Map<AssignmentDTO>(newAssignment);
                return newAssignmentDto;
            }
            catch (Exception ex) when (
                ex is DataAccessException
                || ex is FluentValidation.ValidationException
            )
            { throw; }
            catch (Exception ex) { throw new BusinessException(ex.Message); }
        }
    }
}
