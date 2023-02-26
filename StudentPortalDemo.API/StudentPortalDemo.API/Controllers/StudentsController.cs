using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentPortalDemo.API.DomainModels;
using StudentPortalDemo.API.Repositories;

namespace StudentPortalDemo.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentsRepo _studentsRepo;
        private readonly IMapper _mapper;

        public StudentsController(IStudentsRepo studentsRepo, IMapper mapper)
        {
            _studentsRepo = studentsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentsRepo.GetStudentsAsync();

            return Ok(_mapper.Map<List<DataModels.Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            // Fetch Student Details
            var student = await _studentsRepo.GetStudentAsync(studentId);

            // Return Student
            if (student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdatetStudentAsync([FromRoute] Guid studentId,
            [FromBody] UpdateStudentRequest request)
        {
            if (await _studentsRepo.Exists(studentId))
            {
                // Update details
                var updatedStudent = await _studentsRepo.UpdateStudent(studentId, _mapper.Map<DataModels.Student>(request));

                if (updatedStudent != null)
                {
                    return Ok(_mapper.Map <DomainModels.Student>(updatedStudent));
                }
            }

            return NotFound();
        }
    }
}
