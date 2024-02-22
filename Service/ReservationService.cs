using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
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
        
        public IEnumerable<ReservationDto> GetAllReservations(bool trackChanges)
        {
            var reservations = _repository.Reservation.GetAllReservations(trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
        
        public ReservationDto GetReservation(Guid reservationId, bool trackChanges)
        {
            var reservation = _repository.Reservation.GetReservation(reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);
            
            return _mapper.Map<ReservationDto>(reservation);
        }
        
        public IEnumerable<ReservationForRoomDto> GetReservationsForRoom(Guid roomId, bool trackChanges)
        {
            var room = _repository.Room.GetRoom(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            
            var reservations = _repository.Reservation.GetReservationsForRoom(roomId, trackChanges);
            return _mapper.Map<IEnumerable<ReservationForRoomDto>>(reservations);
        }
        
        public ReservationForRoomDto GetReservationForRoom(Guid roomId, Guid reservationId, bool trackChanges)
        {
            var room = _repository.Room.GetRoom(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            
            var reservation = _repository.Reservation.GetReservationForRoom(roomId, reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);
            
            return _mapper.Map<ReservationForRoomDto>(reservation);
        }
        
        public IEnumerable<ReservationForCustomerDto> GetReservationsForCustomer(Guid customerId, bool trackChanges)
        {
            var customer = _repository.Customer.GetCustomer(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            var reservations = _repository.Reservation.GetReservationsForCustomer(customerId, trackChanges);
            return _mapper.Map<IEnumerable<ReservationForCustomerDto>>(reservations);
        }
        
        public ReservationForCustomerDto GetReservationForCustomer(Guid customerId, Guid reservationId, bool trackChanges)
        {
            var customer = _repository.Customer.GetCustomer(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            var reservation = _repository.Reservation.GetReservationForCustomer(customerId, reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);
            
            return _mapper.Map<ReservationForCustomerDto>(reservation);
        }
    }
}
