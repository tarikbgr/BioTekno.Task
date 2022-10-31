using BioTekno.Task.Business;
using BioTekno.Task.Models.Input;
using BioTekno.Task.Models.Response;
using BioTekno.Task.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BioTekno.Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;

        public OrderController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [HttpPost("createorder")]
        public async Task<ApiResponse<int>> CreateOrder(CreateOrderRequest createOrderRequest, CancellationToken cancellationToken) =>
            await _orderBusiness.CreateOrder(createOrderRequest, cancellationToken);
    }
}
