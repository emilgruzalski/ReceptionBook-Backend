using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        public IEnumerable<ReservationDto> GetReservationsForRoom(Guid roomId, bool trackChanges)
        {
            var room = _repository.Room.GetRoom(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            
            var reservations = _repository.Reservation.GetReservationsForRoom(roomId, trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
        
        public ReservationDto GetReservationForRoom(Guid roomId, Guid reservationId, bool trackChanges)
        {
            var room = _repository.Room.GetRoom(roomId, trackChanges);
            if (room is null)
                throw new RoomNotFoundException(roomId);
            
            var reservation = _repository.Reservation.GetReservationForRoom(roomId, reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);
            
            return _mapper.Map<ReservationDto>(reservation);
        }
        
        public IEnumerable<ReservationDto> GetReservationsForCustomer(Guid customerId, bool trackChanges)
        {
            var customer = _repository.Customer.GetCustomer(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            var reservations = _repository.Reservation.GetReservationsForCustomer(customerId, trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
        
        public ReservationDto GetReservationForCustomer(Guid customerId, Guid reservationId, bool trackChanges)
        {
            var customer = _repository.Customer.GetCustomer(customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(customerId);
            
            var reservation = _repository.Reservation.GetReservationForCustomer(customerId, reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);
            
            return _mapper.Map<ReservationDto>(reservation);
        }
        
        public ReservationDto CreateReservation(ReservationForCreationDto reservation)
        {
            var room = _repository.Room.GetRoomWithDetails(reservation.RoomId, false);
            if (room is null)
                throw new RoomNotFoundException(reservation.RoomId);
            
            if (room.Reservations.Any(r => r.StartDate <= reservation.EndDate && r.EndDate >= reservation.StartDate && r.Status != "Cancelled"))
                throw new RoomAlreadyReservedException(reservation.RoomId, reservation.StartDate, reservation.EndDate);
            
            if (room.Maintenances.Any(m => m.StartDate <= reservation.EndDate && m.EndDate >= reservation.StartDate))
                throw new RoomUnderMaintenanceException(reservation.RoomId, reservation.StartDate, reservation.EndDate);
            
            var customer = _repository.Customer.GetCustomer(reservation.CustomerId, false);
            if (customer is null)
                throw new CustomerNotFoundException(reservation.CustomerId);
            
            var reservationEntity = _mapper.Map<Reservation>(reservation);
            
            _repository.Reservation.CreateReservation(reservationEntity);
            _repository.Save();
            
            reservationEntity.Customer = customer;
            reservationEntity.Room = room;
            
            var reservationToReturn = _mapper.Map<ReservationDto>(reservationEntity);
            
            return reservationToReturn;
        }

        public void DeleteReservation(Guid reservationId, bool trackChanges)
        {
            var reservation = _repository.Reservation.GetReservation(reservationId, trackChanges);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);

            _repository.Reservation.DeleteReservation(reservation);
            _repository.Save();
        }
        
        public void UpdateReservation(Guid reservationId, ReservationForUpdateDto reservation, bool trackChanges)
        {
            var reservationEntity = _repository.Reservation.GetReservation(reservationId, trackChanges);
            if (reservationEntity is null)
                throw new ReservationNotFoundException(reservationId);
            
            var room = _repository.Room.GetRoomWithDetails(reservation.RoomId, false);
            if (room is null)
                throw new RoomNotFoundException(reservation.RoomId);
            
            if (room.Reservations.Any(r => r.StartDate <= reservation.EndDate && r.EndDate >= reservation.StartDate && r.Status != "Cancelled" && r.Id != reservationId))
                throw new RoomAlreadyReservedException(reservation.RoomId, reservation.StartDate, reservation.EndDate);
            
            if (room.Maintenances.Any(m => m.StartDate <= reservation.EndDate && m.EndDate >= reservation.StartDate))
                throw new RoomUnderMaintenanceException(reservation.RoomId, reservation.StartDate, reservation.EndDate);
            
            _mapper.Map(reservation, reservationEntity);
            _repository.Save();
        }
    }
}
