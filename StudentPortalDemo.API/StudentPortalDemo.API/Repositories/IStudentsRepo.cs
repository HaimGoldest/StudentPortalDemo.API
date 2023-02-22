using StudentPortalDemo.API.DataModels;

namespace StudentPortalDemo.API.Repositories
{
    public interface IStudentsRepo
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid studentId);
    }
}
