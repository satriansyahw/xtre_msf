using System.Threading.Tasks;
using System.IO;

namespace Licensing.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(byte[] content, string fileName, string contentType);
    Task<byte[]> GetFileAsync(string filePath);
    Task DeleteFileAsync(string filePath);
}
