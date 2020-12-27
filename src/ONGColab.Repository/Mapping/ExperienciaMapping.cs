using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONGColab.Domain.Entities;

namespace ONGColab.Repository.Mapping
{
    public class ExperienciaMapping : IEntityTypeConfiguration<Experiencia>
    {
        public void Configure(EntityTypeBuilder<Experiencia> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.TextoEndereco)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Complemento)
                .IsRequired(false)
                .HasMaxLength(250);

            builder.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(e => e.Telefone)
                .IsRequired()
                .HasMaxLength(15);

            builder.HasMany(e => e.Doacoes)
                .WithOne(d => d.EnderecoCobranca)
                .HasForeignKey(d => d.EnderecoCobrancaId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Experiência profissional");
        }
    }
}