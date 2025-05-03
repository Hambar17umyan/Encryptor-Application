using Encryptor_Application.Services.Abstract;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Services.Concrete
{
    public class StringConverterService : IStringConverterService
    {
        public string CreateStringFromByteCollection(byte[] byteCollection)
        {
            if (byteCollection == null || byteCollection.Length == 0)
            {
                throw new ArgumentNullException(nameof(byteCollection), "Byte collection cannot be null or empty.");
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var byteValue in byteCollection)
            {
                stringBuilder.Append((char)byteValue);
                stringBuilder.Append(' ');
            }
            return stringBuilder.ToString();
        }

        public string CreateStringFromIntCollection(int[] intCollection)
        {
            if (intCollection == null || intCollection.Length == 0)
            {
                throw new ArgumentNullException(nameof(intCollection), "Int collection cannot be null or empty.");
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var intValue in intCollection)
            {
                stringBuilder.Append($"{intValue}");
                stringBuilder.Append(' ');
            }
            return stringBuilder.ToString();
        }

        public Result<byte[]> TryRetrieveByteCollection(string convertedString)
        {
            if (string.IsNullOrEmpty(convertedString))
            {
                return Result.Fail("Converted string cannot be null or empty.");
            }
            try
            {
                var byteCollection = convertedString.Split(' ')
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => (byte)Convert.ToInt32(s))
                    .ToList();
                return Result.Ok(byteCollection.ToArray());
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to retrieve byte collection: {ex.Message}");
            }
        }

        public Result<int[]> TryRetrieveIntCollection(string convertedString)
        {
            if (string.IsNullOrEmpty(convertedString))
            {
                return Result.Fail("Converted string cannot be null or empty.");
            }
            try
            {
                var intCollection = convertedString.Split(' ')
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => Convert.ToInt32(s))
                    .ToList();
                return Result.Ok(intCollection.ToArray());
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to retrieve int collection: {ex.Message}");
            }
        }
    }
}
