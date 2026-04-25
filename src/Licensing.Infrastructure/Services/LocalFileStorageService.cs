using Licensing.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Licensing.Infrastructure.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _storagePath;

    public LocalFileStorageService(IConfiguration configuration)
    {
        _storagePath = configuration["FileStoragePath"] ?? "App_Data/Uploads";
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<string> SaveFileAsync(byte[] content, string fileName, string contentType)
    {
        var filePath = Path.Combine(_storagePath, $"{Guid.NewGuid()}_{fileName}");
        await File.WriteAllBytesAsync(filePath, content);
        return filePath;
    }

    public async Task<byte[]> GetFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            return await File.ReadAllBytesAsync(filePath);
        }
        throw new FileNotFoundException("File not found at specified path.", filePath);
    }

    public Task DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        return Task.CompletedTask;
    }
}
