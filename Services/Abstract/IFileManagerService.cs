using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace Encryptor_Application.Services.Abstract
{
    public interface IFileManagerService : IService
    {
        /// <summary>
        /// Asynchronously creates a temporary txt file with the specified data.
        /// </summary>
        /// <param name="data">Data to be saved.</param>
        /// <returns>Result with file path coupled in Task.</returns>
        Task<Result<string>> TryCreateTempTxtFileAsync(string data);

        /// <summary>
        /// Asynchronously lets the user to save or share the file.
        /// </summary>
        /// <param name="tempFilePath">The path of file which should be saved.</param>
        /// <returns>Result coupled in Task.</returns>
        Task<Result> SaveFileToUserLocationAsync(string tempFilePath);

        /// <summary>
        /// Deletes all files from a directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns>The result of the process.</returns>
        Result RemoveFilesFromDirectory(string directory);
    }
}
