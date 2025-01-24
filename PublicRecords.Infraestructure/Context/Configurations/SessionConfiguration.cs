using PublicRecords.Domain.Entities.OfficialStateDiary;
using PublicRecords.Domain.Entities.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PublicRecords.Infraestructure.Context.Configurations
{
    internal sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            builder.HasOne(x => x.Person)
                 .WithMany(x => x.Sessions)
                 .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
