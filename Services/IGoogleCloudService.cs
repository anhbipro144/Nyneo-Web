namespace Nyneo_Web.Services
{
    public interface IGoogleCloudService
    {

        Task<string> GetSignedUrlAsync(string? fileNameToRead, int timeOutInMinutes = 30);
        Task<string> UploadFileAsync(IFormFile? imageFile, string? fileNameForStorage);
        Task DeleteFileAsync(string? fileNameForStorage);
        string GenerateFileNameToSave(string? incomingFileName);

    }
}