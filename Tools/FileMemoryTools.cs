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
    [Description("Removes a memory by category and ID.")]
    public async Task<string> RemoveMemory(
        [Description("The type of memory")] MemoryCategory category,
        [Description("The ID of the memory to remove")] int id)
    {
        bool removed = await _storageService.RemoveMemoryByIdAsync(category, id);
        if (removed)
            return $"Removed memory with ID {id} from category '{category}'.";
        else
            return $"Memory with ID {id} not found in category '{category}'.";
    }

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
