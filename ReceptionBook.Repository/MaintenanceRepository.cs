using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceptionBook.Contracts;
using ReceptionBook.Entities.Models;

namespace ReceptionBook.Repository
{
    internal sealed class MaintenanceRepository : RepositoryBase<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
