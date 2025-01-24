using PublicRecords.Domain.Entities.OfficialStateDiary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PublicRecords.Infraestructure.Context.Configurations
{
    internal sealed class OfficialStateDiaryConfiguration : IEntityTypeConfiguration<OfficialDiaries>
    {
        public void Configure(EntityTypeBuilder<OfficialDiaries> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.Session)
                 .WithMany(x => x.OfficialStateDiaries)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Person)
                 .WithMany(x => x.OfficialDiaries)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
