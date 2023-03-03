namespace StudentPortalDemo.API.Repositories
{
    public class LocalProfileImageRepo : IProfileImageRepo
    {
        string profileImagePath = @"Resources\ProfileImages";

        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), profileImagePath, fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return GetServerRelativePath(fileName);
        }

        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(profileImagePath, fileName);
        }
    }
}
