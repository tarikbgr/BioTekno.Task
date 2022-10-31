using AutoMapper;
using BioTekno.Task.Models.Entities;
using BioTekno.Task.Models.Input;
using BioTekno.Task.Models.Output;

namespace BioTekno.Task.AutoMapProfiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Product, ProductDTO>();
        CreateMap<CreateOrderRequest, Order>();
        CreateMap<ProductDetail, OrderDetail>();
    }
}