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
            
            CreateMap<RoomForCreationDto, Room>();
            
            CreateMap<RoomForUpdateDto, Room>();

            CreateMap<Room, RoomWithDetailsDto>();

            CreateMap<Reservation, ReservationDto>()
                .ConstructUsing(r => new ReservationDto(
                    r.Id,
                    r.StartDate,
                    r.EndDate,
                    r.TotalPrice,
                    $"{r.Customer.FirstName} {r.Customer.LastName}",
                    r.Room.Number));

            CreateMap<Reservation, ReservationShortDto>();
            
            CreateMap<ReservationForCreationDto, Reservation>();
            
            CreateMap<ReservationForUpdateDto, Reservation>();

            CreateMap<Reservation, ReservationWithDetailsDto>();
            
            CreateMap<Customer, CustomerDto>();
            
            CreateMap<CustomerForCreationDto, Customer>();
            
            CreateMap<CustomerForUpdateDto, Customer>();

            CreateMap<Customer, CustomerWithDetalisDto>()
                .ForSourceMember(c => c.Reservations, opt => opt.DoNotValidate());

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<User, UserDto>();

            CreateMap<UserForUpdateDto, User>();
        }
    }
}
