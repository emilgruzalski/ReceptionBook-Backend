using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class ReservationService : IReservationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ReservationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<(IEnumerable<ReservationDto> reservations, MetaData metaData)> GetAllReservationsAsync(bool trackChanges, ReservationParameters reservationParameters)
        {
            var reservationsWithMetaData = await _repository.Reservation.GetAllReservationsAsync(trackChanges, reservationParameters);
            var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservationsWithMetaData);
            
            return (reservations: reservationsDto, metaData: reservationsWithMetaData.MetaData);
        }
        
        public async Task<ReservationWithDetailsDto> GetReservationAsync(Guid reservationId, bool trackChanges)
        {
            var reservation = await _repository.Reservation.GetReservationWithDetailsAsync(reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);
            
            return _mapper.Map<ReservationWithDetailsDto>(reservation);
        }
        
        public async Task<ReservationDto> CreateReservationAsync(ReservationForCreationDto reservation)
        {
            var room = await _repository.Room.GetRoomWithDetailsAsync(reservation.RoomId, false);
            if (room is null)
                throw new RoomNotFoundException(reservation.RoomId);
            
            if (room.Reservations.Any(r => r.StartDate <= reservation.EndDate && r.EndDate >= reservation.StartDate && r.Status != "Cancelled"))
                throw new RoomIsReservedException(reservation.RoomId);
            
            var customer = await _repository.Customer.GetCustomerWithDetailsAsync(reservation.CustomerId, false);
            if (customer is null)
                throw new CustomerNotFoundException(reservation.CustomerId);
            
            var reservationEntity = _mapper.Map<Reservation>(reservation);
            
            _repository.Reservation.CreateReservation(reservationEntity);
            await _repository.SaveAsync();
            
            reservationEntity.Customer = customer;
            reservationEntity.Room = room;
            
            var reservationToReturn = _mapper.Map<ReservationDto>(reservationEntity);
            
            return reservationToReturn;
        }

        public async Task DeleteReservationAsync(Guid reservationId, bool trackChanges)
        {
            var reservation = await _repository.Reservation.GetReservationWithDetailsAsync(reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);

            _repository.Reservation.DeleteReservation(reservation);
            await _repository.SaveAsync();
        }
        
        public async Task UpdateReservationAsync(Guid reservationId, ReservationForUpdateDto reservation, bool trackChanges)
        {
            var reservationEntity = await _repository.Reservation.GetReservationWithDetailsAsync(reservationId, trackChanges);
            if (reservationEntity is null)
                throw new ReservationNotFoundException(reservationId);
            
            var room = await _repository.Room.GetRoomWithDetailsAsync(reservation.RoomId, false);
            if (room is null)
                throw new RoomNotFoundException(reservation.RoomId);

                if (room.Reservations.Any(r => r.StartDate <= reservation.EndDate && r.EndDate >= reservation.StartDate && r.Id != reservationId && r.Status != "Cancelled"))
                    throw new RoomIsReservedException(reservation.RoomId);
            
            _mapper.Map(reservation, reservationEntity);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<RaportDto>> GetRaportsAsync(bool trackChanges) 
        {
            var reservations = await _repository.Reservation.GetAllReservationsForRaportAsync(trackChanges);

            // Group the reservations in such a way that the start date of the reservation determines to which month and year a given reservation belongs.
            var groupedReservations = reservations.GroupBy(r => new { r.StartDate.Year, r.StartDate.Month });
            // For each group, calculate the total price of all reservations in the group.
            var raports = groupedReservations.Select(g => new RaportDto
            {
                Year = g.Key.Year.ToString(),
                Month = g.Key.Month.ToString(),
                TotalPrice = g.Sum(r => r.TotalPrice)
            });

            return raports;
        }
    }
}
