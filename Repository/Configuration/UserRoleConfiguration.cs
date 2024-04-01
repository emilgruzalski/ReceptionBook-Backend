using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData
            (
                new IdentityUserRole<string>
                {
                    RoleId = "f4a6305f-859a-45da-9b5b-b0eafe40dce8",
                    UserId = "b778396b-8016-492a-8f38-4188eaca1e1e"
                }
            );
        }
    }
}
