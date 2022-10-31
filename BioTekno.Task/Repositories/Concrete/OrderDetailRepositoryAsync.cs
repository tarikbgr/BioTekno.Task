using BioTekno.Task.Models.Entities;
using BioTekno.Task.Repositories.Abstract;

namespace BioTekno.Task.Repositories.Concrete;

public class OrderDetailRepositoryAsync : EfEntityRepositoryAsyncBase<OrderDetail, Context>, IOrderDetailRepositoryAsync
{
    public OrderDetailRepositoryAsync(Context context) : base(context)
    {
    }
}
