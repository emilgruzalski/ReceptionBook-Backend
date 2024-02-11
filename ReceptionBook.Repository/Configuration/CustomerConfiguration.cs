using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReceptionBook.Entities.Models;

namespace ReceptionBook.Repository.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData
            (
                new Customer
                {
                    Id = new Guid("109ba8ea-dbf1-4221-99dd-00052b252de2"),
                    FirstName = "Jana",
                    LastName = "McLeaf",
                    Email = "jana.mcleaf@gmail.com",
                    PhoneNumber = null
                },
                new Customer
                {
                    Id = new Guid("4b6693b4-f8bc-41b7-b7b8-4ef5b806335a"),
                    FirstName = "Kane",
                    LastName = "Miller",
                    Email = "kane.miller@gmail.com",
                    PhoneNumber = null
                }
            );
        }
    }
}
