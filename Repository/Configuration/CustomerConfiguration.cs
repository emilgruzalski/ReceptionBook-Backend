using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Models;

namespace Repository.Configuration
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
                    PhoneNumber = "569-51-9770"
                },
                new Customer
                {
                    Id = new Guid("4b6693b4-f8bc-41b7-b7b8-4ef5b806335a"),
                    FirstName = "Kane",
                    LastName = "Miller",
                    Email = "kane.miller@gmail.com",
                    PhoneNumber = null
                },
                new Customer
                {
                    Id = new Guid("ff9b6370-e01d-4947-af32-f55c58905c95"),
                    FirstName = "Jon",
                    LastName = "Matzl",
                    Email = "jmatzl0@slashdot.org",
                    PhoneNumber = null
                },
                new Customer
                {
                    Id = new Guid("50aca617-2f75-4da7-a3af-074757f429fb"),
                    FirstName = "Tallia",
                    LastName = "Thomtson",
                    Email = "tthomtsonv@boston.com",
                    PhoneNumber = "726-61-4409"
                },
                new Customer
                {
                    Id = new Guid("eb36d4c7-3ed6-4a9b-8b61-dc42e1d40001"),
                    FirstName = "Kirk",
                    LastName = "Malia",
                    Email = "kmalia10@soundcloud.com",
                    PhoneNumber = "753-55-8744"
                }
            );
        }
    }
}
