namespace Elasticsearch.API.Dtos
{
    public record ProductCreateDto(string Name,decimal Price,int Stock, ProductFeatureDto Feature) 
    //record özünde bir classtır ve newlendikten sonra propertylerinde değişiklik yapılamaz yapılırsa hata fırlatılır. Yani propertyleri değiştirilemez sonradan.
    {

    }
}
