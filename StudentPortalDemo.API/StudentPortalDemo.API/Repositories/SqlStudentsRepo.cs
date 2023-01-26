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
    }
}
