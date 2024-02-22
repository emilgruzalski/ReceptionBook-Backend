using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace ReceptionBook
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDto>();
            
            CreateMap<Maintenance, MaintenanceDto>();
            
            CreateMap<Reservation, ReservationDto>()
                .ForMember(r => r.RoomNumber, opt => opt.MapFrom(x => x.Room.Number))
                .ForMember(r => r.CustomerName, opt => opt.MapFrom(x => string.Join(' ', x.Customer.FirstName, x.Customer.LastName)));
            CreateMap<Reservation, ReservationForRoomDto>()
                .ForMember(r => r.CustomerName, opt => opt.MapFrom(x => string.Join(' ', x.Customer.FirstName, x.Customer.LastName)));
            CreateMap<Reservation, ReservationForCustomerDto>()
                .ForMember(r => r.RoomNumber, opt => opt.MapFrom(x => x.Room.Number));
            
            CreateMap<Customer, CustomerDto>();
        }
    }
}
