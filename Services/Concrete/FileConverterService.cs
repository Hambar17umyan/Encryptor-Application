using Encryptor_Application.Services.Abstract;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Services.Concrete
{
    public class FileConverterService : IFileConverterService
    {
        public Result<string> ConvertByteCollectionToFile(int[] byteCollection, string filePath)
        {
            try
            {
                if (byteCollection == null) throw new ArgumentNullException(nameof(byteCollection));
                if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

                var byteArray = byteCollection.Select(b => (byte)b).ToArray();
                File.WriteAllBytes(filePath, byteArray);

                return Result.Ok(filePath);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>(ex.Message);
            }
        }

        public byte[] ConvertFileToByteCollection(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new ArgumentException("File not found.", nameof(filePath));

            return File.ReadAllBytes(filePath);
        }

        public Result<string> TryConvertByteCollectionToFile(int[] byteCollection, string filePath)
        {
            try
            {
                return ConvertByteCollectionToFile(byteCollection, filePath);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>(ex.Message);
            }
        }

        public Result<byte[]> TryConvertFileToByteCollection(string filePath)
        {
            try
            {
                var byteCollection = ConvertFileToByteCollection(filePath);
                return Result.Ok(byteCollection);
            }
            catch (Exception ex)
            {
                return Result.Fail<byte[]>(ex.Message);
            }
        }
    }
}
