using BergerDb.Domain.PdfTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistanse.PdfTemplates;

public class PdfTemplesConfiguration : IEntityTypeConfiguration<PdfTemplate>
{
    public void Configure(EntityTypeBuilder<PdfTemplate> builder)
    {
        builder
            .ToTable("PdfTemplates");

        builder
            .HasKey(pdfTemp => pdfTemp.Id);

        builder
            .HasIndex(pdfTemp => pdfTemp.Id)
            .IsUnique();

        builder
            .Property(pdfTemp => pdfTemp.Color)
            .IsRequired();
    }
}
