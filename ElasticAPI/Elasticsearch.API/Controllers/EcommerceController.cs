using Elasticsearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcommerceController : ControllerBase
    {

        private readonly ECommerceService _eCommerceService;

        public EcommerceController(ECommerceService eCommerceService)
        {
            _eCommerceService = eCommerceService;
        }
        [HttpGet("FullText/MatchQuery")]
        public async Task<IActionResult> MatchQueryFullText(string categoryName)
        {
            return Ok(await _eCommerceService.MatchQueryFullTextAsync(categoryName));
        }

        [HttpGet("FullText/MatchBoolPrefix")]
        public async Task<IActionResult> MatchBoolPrefixFullText(string customerFullName)
        {
            return Ok(await _eCommerceService.MatchBoolPrefixFullTextAsync(customerFullName));
        }

        [HttpGet("FullText/MatchPhrase")]
        public async Task<IActionResult> MatchPhraseFullText(string customerFullName)
        {
            return Ok(await _eCommerceService.MatchPhraseFullTextAsync(customerFullName));
        }
        [HttpGet("CompoundQuery")]
        public async Task<IActionResult> CompoundQuery(string cityName, double taxfulTotalPrice, string categoryName, string manufacturer)
        {
            return Ok(await _eCommerceService.CompoundQueryAsync(cityName,taxfulTotalPrice,categoryName,manufacturer));
        }

        [HttpGet("FullText/MultiMatchQueryFullText")]
        public async Task<IActionResult> MultiMatchQueryFullText(string name)
        {
            return Ok(await _eCommerceService.MultiMatchQueryFullTextAsync(name));
        }
        
    }
}
