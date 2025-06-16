using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teste_RH.Core.Entities;

namespace Teste_RH.Data.Configurations;

internal class CompanyAddressConfiguration
{
    public void Configure(EntityTypeBuilder<CompanyAddress> builder)
    {
        builder.ToTable("company_addresses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.InsertDate)
            .HasColumnName("insert_date")
            .IsRequired();

        builder.Property(a => a.UpdateDate)
            .HasColumnName("update_date")
            .IsRequired();

        builder.Property(a => a.ZipCode)
            .HasColumnName("zip_code")
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(a => a.Address)
            .HasColumnName("address")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.Neighborhood)
            .HasColumnName("neighborhood")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.City)
            .HasColumnName("city")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.State)
            .HasColumnName("state")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(a => a.AddressComplement)
            .HasColumnName("address_complement")
            .HasMaxLength(150);
    }
}
