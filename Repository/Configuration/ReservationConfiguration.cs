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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasData
            (
                new Reservation
                {
                    Id = new Guid("856ebb5e-fa0e-48c7-8d4d-6605e304efaa"),
                    StartDate = new DateTime(2024, 2, 16),
                    EndDate = new DateTime(2024, 2, 18),
                    CustomerId = new Guid("4b6693b4-f8bc-41b7-b7b8-4ef5b806335a"),
                    RoomId = new Guid("a48e654e-7e13-4a3a-83c8-18f179dd9eea"),
                    Status = "Pending",
                    TotalPrice = 200
                },
                new Reservation
                {
                    Id = new Guid("0857ed5f-98fa-4fdd-b78f-daf6955588a3"),
                    StartDate = new DateTime(2024, 2, 12),
                    EndDate = new DateTime(2024, 2, 25),
                    CustomerId = new Guid("109ba8ea-dbf1-4221-99dd-00052b252de2"),
                    RoomId = new Guid("56625ffa-ef46-461c-8867-2600d87a637a"),
                    Status = "Confirmed",
                    TotalPrice = 2100
                }
            );
        }
    }
}
