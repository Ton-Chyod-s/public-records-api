using PublicRecords.Domain.Entities.User;
using PublicRecords.Domain.Enums.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PublicRecords.Infraestructure.Context.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.Roles)
                .HasDefaultValue(UserEnum.User);
        }
    }
}
