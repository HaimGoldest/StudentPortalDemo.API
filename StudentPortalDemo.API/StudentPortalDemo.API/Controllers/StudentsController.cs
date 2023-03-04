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
        private readonly IProfileImageRepo _profileImageRepo;

        public StudentsController(IStudentsRepo studentsRepo, IMapper mapper, 
            IProfileImageRepo profileImageRepo)
        {
            _studentsRepo = studentsRepo;
            _mapper = mapper;
            _profileImageRepo = profileImageRepo;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentsRepo.GetStudentsAsync();

            return Ok(_mapper.Map<List<DataModels.Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            var student = await _studentsRepo.GetStudentAsync(studentId);

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
                var updatedStudent = await _studentsRepo.UpdateStudent(studentId, _mapper.Map<DataModels.Student>(request));

                if (updatedStudent != null)
                {
                    return Ok(_mapper.Map <DomainModels.Student>(updatedStudent));
                }
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await _studentsRepo.Exists(studentId))
            {
                var student = await _studentsRepo.DeleteStudent(studentId);
                return Ok(_mapper.Map<Student>(student));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await _studentsRepo.AddStudent(_mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },
                _mapper.Map<Student>(student));
        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadProfileImage([FromRoute] Guid studentId, 
            IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
               ".jpeg", ".png", ".gif", ".jpg"
            };

            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    if (await _studentsRepo.Exists(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await _profileImageRepo.Upload(profileImage, fileName);

                        if (await _studentsRepo.UpdateProfileImage(studentId, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }

                return BadRequest("This is not a valid Image format");
            }

            return NotFound();
        }
    }
}
