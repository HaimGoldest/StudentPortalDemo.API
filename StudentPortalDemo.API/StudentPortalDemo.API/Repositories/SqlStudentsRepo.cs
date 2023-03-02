using Microsoft.EntityFrameworkCore;
using StudentPortalDemo.API.DataModels;

namespace StudentPortalDemo.API.Repositories
{
    public class SqlStudentsRepo : IStudentsRepo
    {
        private readonly StudentAdminContext _context;

        public SqlStudentsRepo(StudentAdminContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await _context.Students
                .Include(nameof(Gender)).Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await _context.Students.AnyAsync(x => x.Id == studentId);

        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            var existingStudent = await GetStudentAsync(studentId);
            if (existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;

                await _context.SaveChangesAsync();
                return existingStudent;
            }

            return null;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var student = await GetStudentAsync(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return student;
            }

            return null;
        }

        public async Task<Student> AddStudent(Student request)
        {
            var student = await _context.Students.AddAsync(request);
            await _context.SaveChangesAsync();
            return student.Entity;
        }
    }
}
