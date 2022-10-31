using BioTekno.Task.Models.Entities;
using BioTekno.Task.Repositories.Abstract;

namespace BioTekno.Task.Repositories.Concrete;

public class OrderRepositoryAsync : EfEntityRepositoryAsyncBase<Order, Context>, IOrderRepositoryAsync
{
    public OrderRepositoryAsync(Context context) : base(context)
    {
    }
}
