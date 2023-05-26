namespace Eravol.WebApi.Repository.General
{
    public class PictureStoreRepository : IPictureStoreRepository
    {
        public Task DeleteFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public string GetImageUrl(string imageName)
        {
            throw new NotImplementedException();
        }

        public Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
