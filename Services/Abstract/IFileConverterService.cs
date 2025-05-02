using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Services.Abstract
{
    public interface IFileConverterService : IService
    {
        /// <summary>
        /// Converts a file to a byte collection.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <returns>Byte collection.</returns>
        /// <exception cref="ArgumentException">Thrown when the file is not found.</exception>"
        /// <exception cref="ArgumentNullException">Thrown when filePath is null.</exception>
        byte[] ConvertFileToByteCollection(string filePath);

        /// <summary>
        /// Tries to convert a file to a byte collection.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <returns>Byte collection coupled in Result.</returns>
        Result<byte[]> TryConvertFileToByteCollection(string filePath);

        /// <summary>
        /// Converts a byte collection to a file.
        /// </summary>
        /// <param name="byteCollection">Byte collection</param>
        /// <param name="filePath">File path</param>
        /// <returns>File path.</returns>
        /// <exception cref="ArgumentException">Thrown when byteCollection is converted wrong.</exception>
        /// <exception cref="ArgumentNullException">Thrown when byteCollection is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when filePath is null.</exception>
        /// <exception cref="ArgumentException">Thrown when filePath is invalid.</exception>
        Result<string> ConvertByteCollectionToFile(int[] byteCollection, string filePath);

        /// <summary>
        /// Tries to convert a byte collection to a file.
        /// </summary>
        /// <param name="byteCollection">Byte collection</param>
        /// <param name="filePath">File path</param>
        /// <returns>File path coupled in Result.</returns>
        Result<string> TryConvertByteCollectionToFile(int[] byteCollection, string filePath);
    }
}
