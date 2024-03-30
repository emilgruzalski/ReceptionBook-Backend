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
    public class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
    {
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            builder.HasData
            (
                new Maintenance
                {
                    Id = new Guid("bc1db9a1-8d53-46ab-93f4-e5a9b81e5119"),
                    StartDate = new DateOnly(2024, 4, 1),
                    EndDate = new DateOnly(2024, 4, 8),
                    Description = "Broken sink",
                    Cost = 100.50m,
                    RoomId = new Guid("1ef4db57-145b-4b3d-903d-5486a621646e")
                },
                new Maintenance
                {
                    Id = new Guid("b1bb6af1-f424-411f-9a30-34532015f75d"),
                    StartDate = new DateOnly(2024, 4, 1),
                    EndDate = null,
                    Description = "Broken window",
                    Cost = 249.99m,
                    RoomId = new Guid("9fbbb0c1-a6bb-433c-9520-5a45592f0084")
                }
            );
        }
    }
}
