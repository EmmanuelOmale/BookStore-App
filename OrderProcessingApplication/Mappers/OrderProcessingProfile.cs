using AutoMapper;
using OrderProcessingDomain.Entities;
using OrderProcessingDomain.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingApplication.Mappers
{
    public class OrderProcessingProfile : Profile
    {
        public OrderProcessingProfile()
        {
            CreateMap<CartItemDto, CartItem>().ReverseMap();
            CreateMap<CartDto, Cart>().ReverseMap();
        }
    }
}
