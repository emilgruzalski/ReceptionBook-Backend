﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceptionBook.Contracts;
using ReceptionBook.Service.Contracts;

namespace ReceptionBook.Service
{
    internal sealed class MaintenanceService : IMaintenanceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public MaintenanceService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}