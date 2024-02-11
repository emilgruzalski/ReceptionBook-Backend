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
    public class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
    {
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            builder.HasData
            (
                new Maintenance
                {
                    Id = new Guid("bc1db9a1-8d53-46ab-93f4-e5a9b81e5119"),
                    StartDate = new DateTime(2024, 2, 12),
                    EndDate = new DateTime(2024, 2, 19),
                    Description = "Broken sink",
                    Cost = 100,
                    RoomId = new Guid("1ef4db57-145b-4b3d-903d-5486a621646e")
                },
                new Maintenance
                {
                    Id = new Guid("b1bb6af1-f424-411f-9a30-34532015f75d"),
                    StartDate = new DateTime(2024, 2, 1),
                    EndDate = null,
                    Description = "Broken window",
                    Cost = null,
                    RoomId = new Guid("9fbbb0c1-a6bb-433c-9520-5a45592f0084")
                }
            );
        }
    }
}
