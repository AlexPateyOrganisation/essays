using Essays.Core.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Essays.Core.Data.EntityMappings;

public class AuthorMapping : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.DateOfBirth)
            .IsRequired();

        builder.Property(a => a.Slug)
            .HasMaxLength(255)
            .IsRequired();
    }
}