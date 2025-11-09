using System.ComponentModel;
using MemoryMcpNet.Models;
using MemoryMcpNet.Services;
using ModelContextProtocol.Server;

/// <summary>
/// File-based memory MCP tools for demonstration purposes.
/// </summary>
internal class FileMemoryTools
{
    private readonly MemoryFileStorageService _storageService = new MemoryFileStorageService(@"C:\temp\MCPMemoryStorage");

    [McpServerTool]
    [Description("Stores a memory by category.")]
    public async Task<string> StoreMemory(
        [Description("The type of memory")] MemoryCategory category,
        [Description("The memory to store")] string memory)
    {
        await _storageService.AddToCategoryAsync(category, memory);
        return $"Stored memory in category '{category}': {memory}";
    }

    [McpServerTool]
    [Description("Retrieves a memories by category.")]
    public async Task<string> RetrieveMemory(
    [Description("The type of memory")] MemoryCategory category)
    {
        return await _storageService.GetCategoryContentsAsync(category);
    }
}
