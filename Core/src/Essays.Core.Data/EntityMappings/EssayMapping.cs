using Essays.Core.Data.ValueGenerators;
using Essays.Core.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Essays.Core.Data.EntityMappings;

public class EssayMapping : IEntityTypeConfiguration<Essay>
{
    public void Configure(EntityTypeBuilder<Essay> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.CompressedBody)
            .IsRequired();

        builder.Property(e => e.Author)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.CreatedWhen)
            .HasValueGenerator<CreatedWhenDateGenerator>()
            .IsRequired();

        builder.Property<DateTimeOffset?>("LastEditedWhen");

        builder.Property<bool>("Deleted")
            .HasDefaultValue(false);

        builder.HasQueryFilter(w => EF.Property<bool>(w, "Deleted") == false);
    }
}