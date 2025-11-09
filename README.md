# MCP Server

This is a sample MCP server implemented in .NET using the [ModelContextProtocol](https://www.nuget.org/packages/ModelContextProtocol) C# SDK. It provides tools to store and retrieve memories categorized by type using file-based storage.

## Developing locally

To test this MCP server from source code (locally) without using a built MCP server package, you can configure your IDE to run the project directly using `dotnet run`.

```json
{
  "servers": {
    "MemoryMcpNet": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "<PATH TO PROJECT DIRECTORY>"
      ]
    }
  }
}
```

## Testing the MCP Server

Once configured, you can use the following MCP server tools provided by `FileMemoryTools`:

- **StoreMemory**: Stores a memory in a specified category.
  - Parameters:
    - `category`: The type of memory (see `MemoryCategory` enum)
    - `memory`: The memory content to store
  - Returns: Confirmation message with stored content

- **RetrieveMemory**: Retrieves all memories from a specified category.
  - Parameters:
    - `category`: The type of memory
  - Returns: JSON array of memory items (`Id`, `Content`, `LastUpdated`)

- **RemoveMemory**: Removes a memory by category and ID.
  - Parameters:
    - `category`: The type of memory
    - `id`: The unique ID of the memory to remove
  - Returns: Confirmation message if removed, or not found message

Example usage in Copilot Chat:

```
mcp_memorymcpnet_store_memory: Store a note in the 'Notes' category
mcp_memorymcpnet_retrieve_memory: Retrieve all reminders
mcp_memorymcpnet_remove_memory: Remove memory with ID 5 from 'History'
```

## Publishing to NuGet.org

1. Run `dotnet pack -c Release` to create the NuGet package
2. Publish to NuGet.org with `dotnet nuget push bin/Release/*.nupkg --api-key <your-api-key> --source https://api.nuget.org/v3/index.json`

## Using the MCP Server from NuGet.org

Once the MCP server package is published to NuGet.org, you can configure it in your preferred IDE. Both VS Code and Visual Studio use the `dnx` command to download and install the MCP server package from NuGet.org.

- **VS Code**: Create a `<WORKSPACE DIRECTORY>/.vscode/mcp.json` file
- **Visual Studio**: Create a `<SOLUTION DIRECTORY>\.mcp.json` file

For both VS Code and Visual Studio, the configuration file uses the following server definition:

```json
{
  "servers": {
    "MemoryMcpNet": {
      "type": "stdio",
      "command": "dnx",
      "args": [
        "<your package ID here>",
        "--version",
        "<your package version here>",
        "--yes"
      ]
    }
  }
}
```

## More information

.NET MCP servers use the [ModelContextProtocol](https://www.nuget.org/packages/ModelContextProtocol) C# SDK. For more information about MCP:

- [Official Documentation](https://modelcontextprotocol.io/)
- [Protocol Specification](https://spec.modelcontextprotocol.io/)
- [GitHub Organization](https://github.com/modelcontextprotocol)

Refer to the VS Code or Visual Studio documentation for more information on configuring and using MCP servers:

- [Use MCP servers in VS Code (Preview)](https://code.visualstudio.com/docs/copilot/chat/mcp-servers)
- [Use MCP servers in Visual Studio (Preview)](https://learn.microsoft.com/visualstudio/ide/mcp-servers)
