using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Elasticsearch.API.Dtos;
using Elasticsearch.API.Exceptions;
using Elasticsearch.API.Models;
using Elasticsearch.API.Repositories;

namespace Elasticsearch.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(ProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> SaveAsync(ProductCreateDto request)
        {
            var dto = _mapper.Map<Product>(request);
            var response = await _repository.SaveAsync(dto);
            return _mapper.Map<ProductDto>(response);
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(string id)
        {
            var products = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(products);
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto updateDto)
        {
            var response = await _repository.UpdateAsync(updateDto);
            if (!response.IsValidResponse && response.ElasticsearchServerError!.Status == 404) throw new NotFoundException(nameof(Product), updateDto.Id);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response= await _repository.DeleteAsync(id);
            var asd = response.IsSuccess();
            if (!response.IsValidResponse && response.Result == Result.NotFound) throw new NotFoundException(nameof(Product),id);
            return response.IsSuccess();
        }
    }
}
