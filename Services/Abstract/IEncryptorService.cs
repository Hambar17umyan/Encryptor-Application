using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Services.Abstract
{
    public interface IEncryptorService : IService
    {
        /// <summary>
        /// Encrypts the given data using a specific algorithm.
        /// </summary>
        /// <param name="data">Data to be encrypted</param>
        /// <returns>Encrypted data.</returns>
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        IEnumerable<int> Encrypt(IReadOnlyCollection<byte> data);

        /// <summary>
        /// Decrypts the given data using a specific algorithm.
        /// </summary>
        /// <param name="data">Data to be decrypted</param>
        /// <returns>The decrypted data.</returns>
        /// <exception cref="ArgumentException">Thrown when the data is encrypted wrong</exception>"
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        IEnumerable<byte> Decrypt(IReadOnlyCollection<int> data);

        /// <summary>
        /// Tries to decrypt the given data using a specific algorithm.
        /// </summary>
        /// <param name="data">Data to be decrypted</param>
        /// <returns>The decrypted data coupled in a Result.</returns>
        Result<IEnumerable<byte>> TryDecrypt(IReadOnlyCollection<int> data);

        /// <summary>
        /// Encrypts the given data asynchronously using a specific algorithm.
        /// </summary>
        /// <param name="data">Data to be encrypted</param>
        /// <returns>Encrypted data coupled in task.</returns>
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        Task<IEnumerable<int>> EncryptAsync(IReadOnlyCollection<byte> data);

        /// <summary>
        /// Decrypts the given data asynchronously using a specific algorithm.
        /// </summary>
        /// <param name="data">Data to be decrypted</param>
        /// <returns>The decrypted data coupled in task.</returns>
        /// <exception cref="ArgumentException">Thrown when the data is encrypted wrong.</exception>"
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        Task<IEnumerable<byte>> DecryptAsync(IReadOnlyCollection<int> data);

        /// <summary>
        /// Tries to decrypt the given data asynchronously using a specific algorithm.
        /// </summary>
        /// <param name="data">Data to be decrypted</param>
        /// <returns>The decrypted data coupled in a Result and Tak.</returns>
        Task<Result<IEnumerable<byte>>> TryDecryptAsync(IReadOnlyCollection<int> data);
    }
}
