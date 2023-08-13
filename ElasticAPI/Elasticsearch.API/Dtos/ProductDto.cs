namespace Elasticsearch.API.Dtos
{
    public record ProductDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public ProductFeatureDto? Feature { get; set; }
    }
}
