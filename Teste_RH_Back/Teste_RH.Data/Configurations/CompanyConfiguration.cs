using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teste_RH.Core.Entities;

namespace Teste_RH.Data.Configurations;

internal class CompanyConfiguration
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.InsertDate)
            .HasColumnName("insert_date")
            .IsRequired();

        builder.Property(c => c.UpdateDate)
            .HasColumnName("update_date")
            .IsRequired();

        builder.Property(c => c.CompanyType)
            .HasColumnName("company_type")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.CompanyName)
            .HasColumnName("company_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.DocumentNumber)
            .HasColumnName("document_number")
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(c => c.AdministratorName)
            .HasColumnName("administrator_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.AdministratorDocumentNumber)
            .HasColumnName("administrator_document_number")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("email")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.MobilePhone)
            .HasColumnName("mobile_phone")
            .HasMaxLength(14)
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey("user_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Address)
            .WithOne(a => a.Company)
            .HasForeignKey<CompanyAddress>(a => a.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
