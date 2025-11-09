using System;
using System.Text.Json.Serialization;

namespace MemoryMcpNet.Models
{
    /// <summary>
    /// Represents a memory item for storage in JSON format.
    /// </summary>
    public class MemoryInfo
    {
        /// <summary>
        /// A description of the memory item. Might want to use local LLM summarization to create this.
        /// </summary>
        // [JsonPropertyName("Description")]
        // public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The actual content of the memory item.
        /// </summary>
        [JsonPropertyName("Content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// The last updated timestamp in ISO 8601 format.
        /// </summary>
        [JsonPropertyName("LastUpdated")]
        public string LastUpdated { get; set; } = string.Empty;
    }
}
