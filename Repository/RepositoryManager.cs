﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IRoomRepository> _roomRepository;
        private readonly Lazy<IReservationRepository> _reservationRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(repositoryContext));
            _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(repositoryContext));
            _reservationRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(repositoryContext));
        }

        public ICustomerRepository Customer => _customerRepository.Value;
        public IRoomRepository Room => _roomRepository.Value;
        public IReservationRepository Reservation => _reservationRepository.Value;
        
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
