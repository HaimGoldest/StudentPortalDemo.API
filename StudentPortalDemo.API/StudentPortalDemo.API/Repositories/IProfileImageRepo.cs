namespace StudentPortalDemo.API.Repositories
{
    public interface IProfileImageRepo
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
