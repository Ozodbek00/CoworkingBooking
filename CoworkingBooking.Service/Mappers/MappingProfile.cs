using AutoMapper;
using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.DTOs;

namespace CoworkingBooking.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch, BranchDTO>().ReverseMap();
            CreateMap<Floor, FloorDTO>().ReverseMap();
            CreateMap<Table, TableDTO>().ReverseMap();
            CreateMap<Chair, ChairDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
