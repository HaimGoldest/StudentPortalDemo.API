﻿using StudentPortalDemo.API.DataModels;

namespace StudentPortalDemo.API.Repositories
{
    public interface IStudentsRepo
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid studentId);
        Task<List<Gender>> GetGendersAsync();
        Task<bool> Exists(Guid studentId);
        Task<Student> UpdateStudent(Guid studentId, Student request);
    }
}
