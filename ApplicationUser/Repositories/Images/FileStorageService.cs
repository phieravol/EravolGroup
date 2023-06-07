using System.IO;
namespace Eravol.WebApi.Repositories.Images
{
    public class FileStorageService: IFileStorageService
    {
        private readonly string userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment != null)
            {
                // Truy cập thuộc tính WebRootPath
                userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            }
            else
            {
                // Xử lý trường hợp webHostEnvironment là null
                // Bạn có thể ném một ngoại lệ khác hoặc xử lý nó phù hợp với tình huống của bạn
                throw new ArgumentNullException("webHostEnvironment", "WebHostEnvironment là null.");
            }
            //userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
