using Licensing.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Licensing.Infrastructure.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _storagePath;

    public LocalFileStorageService(IConfiguration configuration)
    {
        var path = configuration["FileStoragePath"] ?? "App_Data/Uploads";
        _storagePath = Path.GetFullPath(path);
        
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    // Fixed: Stream implementation for better memory efficiency
    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var filePath = Path.Combine(_storagePath, $"{Guid.NewGuid()}_{fileName}");
        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(fs);
        }
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
