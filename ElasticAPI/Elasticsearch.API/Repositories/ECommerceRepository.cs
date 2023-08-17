using Elastic.Clients.Elasticsearch;

using Elasticsearch.API.Models;

namespace Elasticsearch.API.Repositories
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "kibana_sample_data_ecommerce";


        public ECommerceRepository(ElasticsearchClient client)
        {
            _client = client;
        }
        //full text query ile yani kelimelere ayrılmış demektir. kelime bazlı arayabiliyoruz. term querylerde ise komple bir bütün olarak eşleşenlerde kayıt dönmektedir.
        public async Task<List<ECommerce>> MatchQueryFullTextAsync(string categoryName)
        {
            //Mesela men's shoes kategorisi aratırken men's ve shoes diye iki kelime ayrılır ve bu ikimi kelimenin de bulunduğu kayıdın skoru daha fazla olur.
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000).Query(q => q.Match(m => m.Field(f => f.Category).Query(categoryName))));
            foreach (var item in result.Hits) item.Source!.Id = item!.Id;

            return result.Documents!.ToList();
        }

        public async Task<List<ECommerce>> MatchBoolPrefixFullTextAsync(string customerFullName)
        {
            //boolprefix yazılan kelimede yazılan son kelimede mesela mehmet dokuyucu ama mehmet dok diye aratırsak dok ile başlayan kayıtları da getirir özelliği o burda da or mantığı vardır mehmet olanları da getirir ille dok ile başlayanların olması da lazım değil, full textin özelliği budur zaten.

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(1000).Query(q => q.MatchBoolPrefix(m => m.Field(f => f.CustomerFullName).Query(customerFullName))));
            foreach (var item in result.Hits) item.Source!.Id = item!.Id;

            return result.Documents!.ToList();
        }

        public async Task<List<ECommerce>> MatchPhraseFullTextAsync(string customerFullName)
        {
            //matchphrase ise sırası önemli ve and olarak iş görür yani mehmet dokuyucu ise mehmetle başlayacak ve dokuyucu ile bitecek ve ikisi de bulunacak demektir.
            // burada mesela ahmet mehmet dokuyucu isimli bir kişi var ve biz mehmet dokuyucu diye arattık yine gelir önünde ve sonundaki kelimelerin başka olması önemli değil.
            //maksat mehmet dokuyucu sıralı bir şekilde birlikte bulunsun yeterlidir. Ama sağında ve solunda bulunan kelimelerle de olan ismin de olması lazım
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(100).Query(q => q.MatchPhrase(m => m.Field(f => f.CustomerFullName).Query(customerFullName))));
            foreach (var item in result.Hits) item.Source!.Id = item!.Id;

            return result.Documents!.ToList();
        }

        public async Task<List<ECommerce>> CompoundQueryAsync(string cityName, double taxfulTotalPrice, string categoryName,string manufacturer)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .Bool(b => b.Must(m => m.Term(t => t.Field("geoip.city_name").Value(cityName)))
            .MustNot(mn => mn.Range(r => r.NumberRange(nr => nr.Field(f => f.TaxfulTotalPrice).Lte(taxfulTotalPrice))))
            .Should(s => s.Term(t => t.Field(f => f.Suffix("keyword")).Value(categoryName)))
            .Filter(f => f.Term(t => t.Field("manufacturer.keyword").Value(manufacturer)))
            )));

            foreach (var item in result.Hits) item.Source!.Id = item!.Id;

            return result.Documents!.ToList();
        }

        public async Task<List<ECommerce>> MultiMatchQueryFullTextAsync(string name)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s
            .Index(indexName)
            .Size(100)
            .Query(q => q
            .MultiMatch(m => m
            .Fields(new Field("customer_first_name").And(new Field("customer_last_name")).And(new Field("customer_full_name"))).Query(name))
            
            ));
            

            foreach (var item in result.Hits) item.Source!.Id = item!.Id;

            return result.Documents!.ToList();
        }
    }
}
