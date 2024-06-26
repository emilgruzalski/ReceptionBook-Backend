﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Models;

namespace Repository.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasData
            (
                new Room
                {
                    Id = new Guid("1ef4db57-145b-4b3d-903d-5486a621646e"),
                    Number = "A01",
                    Type = "Single",
                    Price = 100.00m
                },
                new Room
                {
                    Id = new Guid("9fbbb0c1-a6bb-433c-9520-5a45592f0084"),
                    Number = "A02",
                    Type = "Double",
                    Price = 150.00m
                },
                new Room
                {
                    Id = new Guid("a48e654e-7e13-4a3a-83c8-18f179dd9eea"),
                    Number = "A03",
                    Type = "Single",
                    Price = 100.00m
                },
                new Room
                {
                    Id = new Guid("56625ffa-ef46-461c-8867-2600d87a637a"),
                    Number = "B10",
                    Type = "Double",
                    Price = 300.00m
                },
                new Room
                {
                    Id = new Guid("7fac1f9c-b24b-4f3c-a4ea-4a0b3f5e2577"),
                    Number = "B11",
                    Type = "Single",
                    Price = 200.00m
                },
                new Room
                {
                    Id = new Guid("282eee35-eea8-4ced-a43b-533b0ad7a946"),
                    Number = "C01",
                    Type = "Single",
                    Price = 250.00m
                }
            );
        }
    }
}
