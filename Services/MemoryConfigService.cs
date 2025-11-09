using System.IO;
using System.Text.Json;
using MemoryMcpNet.Models;

namespace MemoryMcpNet.Services
{
    public class MemoryConfigService
    {
        public string ConfigFilePath { get; }

        public MemoryConfigService(string configFilePath)
        {
            ConfigFilePath = configFilePath;
        }

        public MemoryConfig ReadConfig()
        {
            if (!File.Exists(ConfigFilePath))
            {
                var defaultConfig = new MemoryConfig { NextId = 1 };
                WriteConfig(defaultConfig);
                return defaultConfig;
            }

            var json = File.ReadAllText(ConfigFilePath);
            return JsonSerializer.Deserialize<MemoryConfig>(json);
        }

        public void WriteConfig(MemoryConfig config)
        {
            var json = JsonSerializer.Serialize(config);
            File.WriteAllText(ConfigFilePath, json);
        }

        public int GetAndIncrementNextId()
        {
            var config = ReadConfig();
            int nextId = config.NextId;
            config.NextId++;
            WriteConfig(config);
            return nextId;
        }
    }
}