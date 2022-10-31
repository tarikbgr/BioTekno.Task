using BioTekno.Task.Models.Entities;
using BioTekno.Task.Repositories.Abstract;

namespace BioTekno.Task.Repositories.Concrete;

public class ProductRepositoryAsync : EfEntityRepositoryAsyncBase<Product, Context>, IProductRepositoryAsync
{
    public ProductRepositoryAsync(Context context) : base(context)
    {
    }
}
