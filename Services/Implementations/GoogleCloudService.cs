using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Nyneo_Web.Services.Implementations;

public class GoogleCloudService : IGoogleCloudService
{
    private GoogleCredential googleCredential;
    private StorageClient storageClient;
    private string? bucketName;

    public GoogleCloudService(IConfiguration configuration)
    {
        var key = configuration.GetValue<string>("GoogleCredentialFile");
        googleCredential = GoogleCredential.FromFile(key);
        storageClient = StorageClient.Create(googleCredential);
        bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
    }
    public async Task DeleteFileAsync(string? fileNameForStorage)
    {
        if (!string.IsNullOrWhiteSpace(fileNameForStorage))
        {
            try
            {

                await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public async Task<string> GetSignedUrlAsync(string? fileNameToRead, int timeOutInMinutes = 30)
    {
        try
        {
            var sac = googleCredential.UnderlyingCredential as ServiceAccountCredential;
            var urlSigner = UrlSigner.FromCredential(sac);
            // provides limited permission and time to make a request: time here is mentioned for 30 minutes.
            var signedUrl = await urlSigner.SignAsync(bucketName, fileNameToRead, TimeSpan.FromMinutes(timeOutInMinutes));
            return signedUrl.ToString();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> UploadFileAsync(IFormFile? imageFile, string? fileNameForStorage)
    {
        try
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
                return dataObject.MediaLink;
            }
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }

    public string GenerateFileNameToSave(string? incomingFileName)
    {
        var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
        var extension = Path.GetExtension(incomingFileName);
        return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
    }
}