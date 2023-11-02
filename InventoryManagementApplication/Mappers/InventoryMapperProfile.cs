using AutoMapper;
using InventoryManagementDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementApplication.Mappers
{
    public class InventoryMapperProfile : Profile
    {
        public InventoryMapperProfile()
        {
            CreateMap<InventoryItem,  InventoryItemDto>().ReverseMap();
        }
    }
}
