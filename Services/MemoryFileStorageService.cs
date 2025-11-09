using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using MemoryMcpNet.Models;

namespace MemoryMcpNet.Services
{

    public class MemoryFileStorageService
    {
        private readonly MemoryConfigService _configService;
        private string MemoryConfigFilePath => Path.Combine(StorageLocation, "MemoryConfig.json");
        public string StorageLocation { get; } = @"C:\temp\MCPMemoryStorage";


        public MemoryFileStorageService(string storageLocation)
        {
            StorageLocation = storageLocation;
            _configService = new MemoryConfigService(MemoryConfigFilePath);
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
            return Path.Combine(StorageLocation, $"{category}.json");
        }

        public async Task AddToCategoryAsync(MemoryCategory category, string content)
        {
            string filePath = GetFilePath(category);
            List<MemoryInfo> memories = new List<MemoryInfo>();
            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    try
                    {
                        memories = JsonSerializer.Deserialize<List<MemoryInfo>>(json) ?? new List<MemoryInfo>();
                    }
                    catch { memories = new List<MemoryInfo>(); }
                }
            }
            var memory = new MemoryInfo
            {
                Id = _configService.GetAndIncrementNextId(),
                Content = content,
                LastUpdated = DateTime.UtcNow.ToString("o")
            };
            memories.Add(memory);
            string outputJson = JsonSerializer.Serialize(memories, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, outputJson);
        }

        public async Task<string> GetCategoryContentsAsync(MemoryCategory category)
        {
            string filePath = GetFilePath(category);
            if (!File.Exists(filePath))
                return string.Empty;
            string json = await File.ReadAllTextAsync(filePath);
            return json;
        }
    }
}