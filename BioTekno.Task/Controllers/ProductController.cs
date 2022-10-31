using BioTekno.Task.Business;
using BioTekno.Task.Models.Output;
using BioTekno.Task.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BioTekno.Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet("getproducts")]
        public async Task<ApiResponse<List<ProductDTO>>> GetProducts(CancellationToken cancellationToken, string? category) =>
            await _productBusiness.GetProducts(cancellationToken, category);
    }
}

