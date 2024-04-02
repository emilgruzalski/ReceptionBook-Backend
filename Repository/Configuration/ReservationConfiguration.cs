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
                    StartDate = new DateOnly(2024, 4, 5),
                    EndDate = new DateOnly(2024, 4, 7),
                    CustomerId = new Guid("4b6693b4-f8bc-41b7-b7b8-4ef5b806335a"),
                    RoomId = new Guid("a48e654e-7e13-4a3a-83c8-18f179dd9eea"),
                    TotalPrice = 200.00m
                },
                new Reservation
                {
                    Id = new Guid("0857ed5f-98fa-4fdd-b78f-daf6955588a3"),
                    StartDate = new DateOnly(2024, 4, 10),
                    EndDate = new DateOnly(2024, 4, 15),
                    CustomerId = new Guid("109ba8ea-dbf1-4221-99dd-00052b252de2"),
                    RoomId = new Guid("56625ffa-ef46-461c-8867-2600d87a637a"),
                    TotalPrice = 1200.00m
                },
                new Reservation 
                {
                    Id = new Guid("e2afccbc-24df-4552-97dc-6af2b985d01a"),
                    StartDate = new DateOnly(2024, 4, 6),
                    EndDate = new DateOnly(2024, 4, 11),
                    CustomerId = new Guid("eb36d4c7-3ed6-4a9b-8b61-dc42e1d40001"),
                    RoomId = new Guid("282eee35-eea8-4ced-a43b-533b0ad7a946"),
                    TotalPrice = 1199.99m
                }
            );
        }
    }
}
