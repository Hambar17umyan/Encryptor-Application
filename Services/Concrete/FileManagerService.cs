using Encryptor_Application.Services.Abstract;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Services.Concrete
{
    public class FileManagerService : IFileManagerService
    {
        public Result RemoveFilesFromDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    File.Delete(file);
                }
            }
            else
            {
                return Result.Fail("Some error occurred!");
            }

            var cnt = Directory.GetFiles(directory).Length;
            if (cnt != 0)
            {
                return Result.Fail("Some error occurred!");
            }

            return Result.Ok();
        }

        public async Task<Result> SaveFileToUserLocationAsync(string tempFilePath)
        {
            tempFilePath = @"C:\Users\hamba\AppData\Local\User Name\com.companyname.encryptorapplication\Cache\TempData.txt";
            if (string.IsNullOrEmpty(tempFilePath) || !File.Exists(tempFilePath))
            {
                return Result.Fail("Temporary file not found.");
            }

            try
            {
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Save Encrypted File",
                    File = new ShareFile(tempFilePath)
                });

                await Task.Delay(10000);
                File.Delete(tempFilePath);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to save file: {ex.Message}");
            }
        }

        public async Task<Result<string>> TryCreateTempTxtFileAsync(string data)
        {
            try
            {
                string fileName = "TempData.txt";
                string tempPath = FileSystem.CacheDirectory;
                var tempFilePath = Path.Combine(tempPath, fileName);

                await File.WriteAllTextAsync(tempFilePath, data);

                return Result.Ok(tempFilePath);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to create temp file: {ex.Message}");
            }
        }
    }
}
