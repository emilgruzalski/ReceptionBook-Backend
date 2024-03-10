using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

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
        
        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(bool trackChanges)
        {
            var reservations = await _repository.Reservation.GetAllReservationsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
        
        public async Task<ReservationDto> GetReservationAsync(Guid reservationId, bool trackChanges)
        {
            var reservation = await _repository.Reservation.GetReservationAsync(reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);
            
            return _mapper.Map<ReservationDto>(reservation);
        }
        
        public async Task<IEnumerable<ReservationDto>> GetReservationsForRoomAsync(Guid roomId, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomAsync(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            
            var reservations = await _repository.Reservation.GetReservationsForRoomAsync(roomId, trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
        
        public async Task<IEnumerable<ReservationDto>> GetReservationsForCustomerAsync(Guid customerId, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            var reservations = await _repository.Reservation.GetReservationsForCustomerAsync(customerId, trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
        
        public async Task<ReservationDto> CreateReservationAsync(ReservationForCreationDto reservation)
        {
            var room = await _repository.Room.GetRoomWithDetailsAsync(reservation.RoomId, false);
            if (room is null)
                throw new RoomNotFoundException(reservation.RoomId);
            
            if (room.Reservations.Any(r => r.StartDate <= reservation.EndDate && r.EndDate >= reservation.StartDate && r.Status != "Cancelled"))
                throw new RoomAlreadyReservedException(reservation.RoomId, reservation.StartDate, reservation.EndDate);
            
            if (room.Maintenances.Any(m => m.StartDate <= reservation.EndDate && m.EndDate >= reservation.StartDate))
                throw new RoomUnderMaintenanceException(reservation.RoomId, reservation.StartDate, reservation.EndDate);
            
            var customer = await _repository.Customer.GetCustomerAsync(reservation.CustomerId, false);
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
            var reservation = await _repository.Reservation.GetReservationAsync(reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);

            _repository.Reservation.DeleteReservation(reservation);
            await _repository.SaveAsync();
        }
        
        public async Task UpdateReservationAsync(Guid reservationId, ReservationForUpdateDto reservation, bool trackChanges)
        {
            var reservationEntity = await _repository.Reservation.GetReservationAsync(reservationId, trackChanges);
            if (reservationEntity is null)
                throw new ReservationNotFoundException(reservationId);
            
            var room = await _repository.Room.GetRoomWithDetailsAsync(reservation.RoomId, false);
            if (room is null)
                throw new RoomNotFoundException(reservation.RoomId);
            
            if (room.Reservations.Any(r => r.StartDate <= reservation.EndDate && r.EndDate >= reservation.StartDate && r.Status != "Cancelled" && r.Id != reservationId))
                throw new RoomAlreadyReservedException(reservation.RoomId, reservation.StartDate, reservation.EndDate);
            
            if (room.Maintenances.Any(m => m.StartDate <= reservation.EndDate && m.EndDate >= reservation.StartDate))
                throw new RoomUnderMaintenanceException(reservation.RoomId, reservation.StartDate, reservation.EndDate);
            
            _mapper.Map(reservation, reservationEntity);
            await _repository.SaveAsync();
        }
    }
}
