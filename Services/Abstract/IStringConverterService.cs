using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Services.Abstract
{
    public interface IStringConverterService : IService
    {
        /// <summary>
        /// Creates a string from a byte array.
        /// </summary>
        /// <param name="byteCollection">Byte array.</param>
        /// <returns>String.</returns>
        /// <exception cref="ArgumentNullException">Thrown when byteCollection is null.</exception>
        string CreateStringFromByteCollection(byte[] byteCollection);

        /// <summary>
        /// Creates a string from an int array.
        /// </summary>
        /// <param name="intCollection">Int array.</param>
        /// <returns>String.</returns>
        /// <exception cref="ArgumentNullException">Thrown when byteCollection is null.</exception>
        string CreateStringFromIntCollection(int[] intCollection);

        /// <summary>
        /// Tries to retrieve a byte array from a string.
        /// </summary>
        /// <param name="convertedString">The converted string</param>
        /// <returns>The Result of the process including the collection</returns>
        Result<byte[]> TryRetrieveByteCollection(string convertedString);

        /// <summary>
        /// Tries to retrieve an int array from a string.
        /// </summary>
        /// <param name="convertedString">The converted string</param>
        /// <returns>The Result of the process including the collection</returns>
        Result<int[]> TryRetrieveIntCollection(string convertedString);
    }
}
