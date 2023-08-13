using Elasticsearch.API.Dtos;
using Elasticsearch.API.Models;
using Nest;

namespace Elasticsearch.API.Repositories
{
    public class ProductRepository
    {
         private readonly ElasticClient _client;
        private const string indexName = "products";


        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product product)
        {
            product.Created=DateTime.Now;

            var response = await _client.IndexAsync(product,x=>x.Index(indexName)); //buradaki products yazan kısım hangi tabloya kaydedeceğimizi söylemektir.
            if (!response.IsValid) return null;

            product.Id = response.Id; //elasticsearchün atadığı idyi bizim objeye setliyoruz.
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var result = await _client.SearchAsync<Product>(s => s.Index(indexName).Query(q=>q.MatchAll())); // elasticsearchteki gibi ilk index ismi sonra query dedikten sonra matchAll ile tüm dataları getiriyoruz.
            foreach (var item in result.Hits) item.Source.Id = item.Id;
            return result.Documents.ToList();


        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var result = await _client.GetAsync<Product>(id,x=>x.Index(indexName));
            if (!result.IsValid) return null;

            result.Source.Id= result.Id;
            return result.Source ;


        }

        public async Task<UpdateResponse<Product>> UpdateAsync(ProductUpdateDto updateDto)
        {
            
            var response = await _client.UpdateAsync<Product,ProductUpdateDto>(updateDto.Id, x=>x.Index(indexName).Doc(updateDto));  //buradak updatebyqueryasync metotlarında queryler belirterek idsi şu olanı güncelle veya ismi şu olanı güncelle gibi kurallar koyarak güncellemeler de yapabiliriz.
            return response;

        }
        /// <summary>
        /// Hata yönetimi için bu method ele alınmıştır.
        /// </summary> 
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeleteResponse> DeleteAsync(string id)
        {

            var response = await _client.DeleteAsync<Product>(id,x=>x.Index(indexName));
            return response;

        }
    }
}
