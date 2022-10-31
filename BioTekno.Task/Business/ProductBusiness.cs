using AutoMapper;
using BioTekno.Task.Models.Entities;
using BioTekno.Task.Models.Output;
using BioTekno.Task.Models.Response;
using BioTekno.Task.Repositories.Abstract;
using BioTekno.Task.Services;
using Castle.Core.Internal;
using StackExchange.Redis;
using System.Threading;
using ILogger = Serilog.ILogger;

namespace BioTekno.Task.Business;

public interface IProductBusiness
{
    Task<ApiResponse<List<ProductDTO>>> GetProducts(CancellationToken cancellationToken, string? category);
}

public class ProductBusiness : IProductBusiness
{
    private const string GetAllProductsKey = "getAllProductsKey";
    private readonly IMapper _mapper;
    private readonly IProductRepositoryAsync _productRepositoryAsync;
    private readonly IRedisCacheService<List<Product>> _redisCacheService;
    private readonly ILogger _logger;


    public ProductBusiness(IRedisCacheService<List<Product>> redisCacheService,
        IProductRepositoryAsync productRepositoryAsync, IMapper mapper, ILogger logger)
    {
        _redisCacheService = redisCacheService;
        _productRepositoryAsync = productRepositoryAsync;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponse<List<ProductDTO>>> GetProducts(CancellationToken cancellationToken, string? category)
    {
        return category.IsNullOrEmpty()
            ? await GetAllProducts(cancellationToken)
            : await GetProductsByCategory(cancellationToken, category);
    }


    private async Task<ApiResponse<List<ProductDTO>>> GetAllProducts(CancellationToken cancellationToken)
    {
        var cache = await _redisCacheService.GetCache(GetAllProductsKey);
        if (cache == null)
        {
            var product = await _productRepositoryAsync.GetAllAsync(cancellationToken);
            if (product.Count == 0)
                return new ApiResponse<List<ProductDTO>>(null, Status.Failed, 404, "Product Not Found...");

            await _redisCacheService.SetCache(GetAllProductsKey, product);
            var responseDb = _mapper.Map<List<ProductDTO>>(product);
            _logger.Information("Tüm ürünler database'den geldi...");
            return new ApiResponse<List<ProductDTO>>(responseDb);
        }

        var resposneCache = _mapper.Map<List<ProductDTO>>(cache);
        _logger.Information("Tüm ürünler cache'den geldi...");
        return new ApiResponse<List<ProductDTO>>(resposneCache);
    }

    private async Task<ApiResponse<List<ProductDTO>>> GetProductsByCategory(CancellationToken cancellationToken,
        string? category)
    {
        var cache = await _redisCacheService.GetCache(category);
        if (cache == null)
        {
            var product = await _productRepositoryAsync.GetAllAsync(cancellationToken, x => x.Category == category);
            if (product.Count == 0)
                return new ApiResponse<List<ProductDTO>>(null, Status.Failed, 404, "Category Not Found...");
            
            await _redisCacheService.SetCache(category, product);
            var responseDb = _mapper.Map<List<ProductDTO>>(product);
            _logger.Information("{category}'e ait ürünler database'den geldi...", category);
            return new ApiResponse<List<ProductDTO>>(responseDb);
        }

        var resposneCache = _mapper.Map<List<ProductDTO>>(cache);
        _logger.Information("{category}'e ait ürünler cache'den geldi...", category);
        return new ApiResponse<List<ProductDTO>>(resposneCache);
    }
}