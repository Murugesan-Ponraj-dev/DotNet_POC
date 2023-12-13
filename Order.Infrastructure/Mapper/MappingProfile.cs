using AutoMapper;
using Order.Domain.DTOs;
using Order.Domain.Entities;

namespace Order.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<ProductDTO, Product>().ReverseMap();
        }

    }
}
