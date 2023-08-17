using AutoMapper;
using Elasticsearch.API.Dtos;
using Elasticsearch.API.Models;
using Elasticsearch.API.Repositories;

namespace Elasticsearch.API.Services
{
    public class ECommerceService
    {
        private readonly ECommerceRepository _repository;
        private readonly IMapper _mapper;
        public ECommerceService(ECommerceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ECommerceDto>> MatchQueryFullTextAsync(string categoryName)
        {           
            var response = await _repository.MatchQueryFullTextAsync(categoryName);
            return _mapper.Map<List<ECommerceDto>>(response);
        }

        public async Task<List<ECommerceDto>> MatchBoolPrefixFullTextAsync(string customerFullName)
        {
            var response = await _repository.MatchBoolPrefixFullTextAsync(customerFullName);
            return _mapper.Map<List<ECommerceDto>>(response);
        }
        public async Task<List<ECommerceDto>> MatchPhraseFullTextAsync(string customerFullName)
        {
            var response = await _repository.MatchPhraseFullTextAsync(customerFullName);
            return _mapper.Map<List<ECommerceDto>>(response);
        }
        public async Task<List<ECommerceDto>> CompoundQueryAsync(string cityName, double taxfulTotalPrice, string categoryName, string manufacturer)
        {
            var response = await _repository.CompoundQueryAsync(cityName,taxfulTotalPrice,categoryName,manufacturer);
            return _mapper.Map<List<ECommerceDto>>(response);
        }

        public async Task<List<ECommerceDto>> MultiMatchQueryFullTextAsync(string name)
        {
            var response = await _repository.MultiMatchQueryFullTextAsync(name);
            return _mapper.Map<List<ECommerceDto>>(response);
        }
        
    }
}
