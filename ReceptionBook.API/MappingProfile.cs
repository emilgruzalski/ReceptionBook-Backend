using AutoMapper;
using ReceptionBook.Entities.Models;
using ReceptionBook.Shared.DataTransferObjects;

namespace ReceptionBook.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDto>();
            
            CreateMap<Maintenance, MaintenanceDto>();
        }
    }
}
