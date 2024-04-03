using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            builder.HasData
            (
                new User
                {
                    Id = "b778396b-8016-492a-8f38-4188eaca1e1e",
                    FirstName = "Emil",
                    LastName = "Grużalski",
                    UserName = "emil.gruzalski@gmail.com",
                    NormalizedUserName = "EMIL.GRUZALSKI@GMAIL.COM",
                    Email = "emil.gruzalski@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Admin12345"),
                }
            );
        }
    }
}
