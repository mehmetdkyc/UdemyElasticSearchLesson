﻿using System.Text.Json.Serialization;

namespace Elasticsearch.Web.Models
{
    public class Blog
    {
        [JsonPropertyName("_id")]
        public string? Id { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("created")]
        public DateTime CreatedDate { get; set; }
        [JsonPropertyName("tags")]
        public string[]? Tags { get; set; }
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }
    }
}
