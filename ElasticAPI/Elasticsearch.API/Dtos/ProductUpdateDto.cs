namespace Elasticsearch.API.Dtos
{
    public record ProductUpdateDto(string Name, decimal Price, int Stock, ProductFeatureDto Feature,string Id)
    {
    }
}
