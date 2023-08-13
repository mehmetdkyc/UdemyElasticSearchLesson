using Nest;

namespace Elasticsearch.API.Models
{
    public class Product
    {
        [PropertyName("_id")] // buradaki idnin elasticsearchteki karşılığının metadata içerisindeki _id olduğu için propertyname içinde belirttik.
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; } 
        public int Stock { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public ProductFeature? Feature { get; set; }
    }
}     
