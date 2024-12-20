using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading.Tasks;

namespace KerazaWebApplication.Data
{
    public class GoogleDriveService
    {
        private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
        private readonly string _credentialsFilePath;

        public GoogleDriveService()
        {
            // Path to the Service Account JSON file
            _credentialsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "kerazawebapplication-c0ec554e0151.json");
        }

        public async Task<DriveService> AuthenticateAsync()
        {
            var credential = GoogleCredential.FromFile(_credentialsFilePath)
                .CreateScoped(Scopes);

            // Create the Drive API client
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "My Google Drive App",
            });

            return driveService;
        }

        public async Task DeleteFileAsync(string fileId)
        {
            var driveService = await AuthenticateAsync();

            // Delete the file from Google Drive
            await driveService.Files.Delete(fileId).ExecuteAsync();
        }


        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string mimeType, string folderId = null)
        {
            try
            {
                var driveService = await AuthenticateAsync();

                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName,
                    MimeType = mimeType
                };

                // If a folderId is provided, set it as the parent folder for the uploaded file
                if (!string.IsNullOrEmpty(folderId))
                {
                    fileMetadata.Parents = new List<string> { folderId };
                }

                var request = driveService.Files.Create(fileMetadata, fileStream, mimeType);
                request.Fields = "id, webViewLink"; // Only requesting 'id' and 'webViewLink' fields
                var file = await request.UploadAsync();

                if (file.Status == Google.Apis.Upload.UploadStatus.Failed)
                {
                    throw new Exception("File upload failed.");
                }

                // Once uploaded, the file metadata can be accessed via the request response object
                var uploadedFile = request.ResponseBody;

                // Make the file publicly accessible
                var permission = new Permission()
                {
                    Type = "anyone",
                    Role = "reader"
                };

                // Set the permission to make the file publicly accessible
                await driveService.Permissions.Create(permission, uploadedFile.Id).ExecuteAsync();

                // Return the public URL for the uploaded file

                /*return uploadedFile.WebViewLink;*/ // URL to view the file
                return uploadedFile.Id;
                
            }
            catch (Exception ex)
            {
                // Handle errors (logging or throwing more specific exceptions)
                throw new Exception("An error occurred during file upload: " + ex.Message, ex);
            }
        }
    
        

    
    }
}
