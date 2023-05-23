namespace Eravol.WebApi.Repository.General
{
    public interface IPictureStoreRepository
    {
        string GetImageUrl(string imageName);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        Task DeleteFileAsync(string fileName);

    }
}
