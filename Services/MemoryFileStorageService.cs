using System;
using System.IO;
using System.Threading.Tasks;
using MemoryMcpNet.Models;

namespace MemoryMcpNet.Services
{

    public class MemoryFileStorageService
    {
        public string StorageLocation { get; } = @"C:\temp\MCPMemoryStorage";

        public MemoryFileStorageService(string storageLocation)
        {
            StorageLocation = storageLocation;
            Directory.CreateDirectory(StorageLocation);

            foreach (MemoryCategory category in Enum.GetValues(typeof(MemoryCategory)))
            {
                string filePath = GetFilePath(category);
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }
            }
        }

        private string GetFilePath(MemoryCategory category)
        {
            return Path.Combine(StorageLocation, $"{category}.txt");
        }

        public async Task AddToCategoryAsync(MemoryCategory category, string content)
        {
            string filePath = GetFilePath(category);
            await File.AppendAllTextAsync(filePath, content + Environment.NewLine);
        }

        public async Task<string> GetCategoryContentsAsync(MemoryCategory category)
        {
            string filePath = GetFilePath(category);
            if (!File.Exists(filePath))
                return string.Empty;

            return await File.ReadAllTextAsync(filePath);
        }
    }
}