using System.Security.Cryptography;
using AutoMapper;
using BioTekno.Task.Models.Entities;
using BioTekno.Task.Models.Input;
using BioTekno.Task.Models.Response;
using BioTekno.Task.Repositories.Abstract;
using MassTransit;
using SendMail;
using ILogger = Serilog.ILogger;

namespace BioTekno.Task.Business;

public interface IOrderBusiness
{
    Task<ApiResponse<int>> CreateOrder(CreateOrderRequest createOrderRequest, CancellationToken cancellationToken);
}

public class OrderBusiness : IOrderBusiness
{
    private readonly IOrderRepositoryAsync _orderRepositoryAsync;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IBusControl _bus;

    public OrderBusiness(IOrderRepositoryAsync orderRepositoryAsync, ILogger logger, IMapper mapper, IBusControl bus)
    {
        _orderRepositoryAsync = orderRepositoryAsync;
        _logger = logger;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<ApiResponse<int>> CreateOrder(CreateOrderRequest createOrderRequest,
        CancellationToken cancellationToken)
    {
        var amount = createOrderRequest.ProductDetails.Sum(item => item.UnitPrice * item.Amount);
        var order = _mapper.Map<Order>(createOrderRequest);
        var orderDetail = _mapper.Map<List<OrderDetail>>(createOrderRequest.ProductDetails);

        order.TotalAmount = amount;
        order.OrderDetails = orderDetail;

        var response = await _orderRepositoryAsync.AddAsync(cancellationToken, order);
        _logger.Information(
            $"Sipariş başarıyla oluşturuldu...Sipariş Id={response.Id} Toplam tutar: {response.TotalAmount}");

        await _bus.Publish<ISendMail>(new SendMails
        {
            CustomerEmail = createOrderRequest.CustomerEmail,
            OrderId = response.Id
        });

        return new ApiResponse<int>(response.Id, Status.Success, 200,
            $"{order.CustomerName} isimli kullanıcının siparişi başarıyla oluştu.. Sipariş Id: {response.Id} Toplam tutar = {amount} TL. Birazdan {order.CustomerEmail} adresine bilgilendirme maili gelecektir...");
    }
}