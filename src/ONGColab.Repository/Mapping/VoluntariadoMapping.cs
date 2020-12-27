using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONGColab.Domain.Entities;

namespace ONGColab.Repository.Mapping
{
    public class VoluntariadoMapping : IEntityTypeConfiguration<Voluntariado>
    {
        public void Configure(EntityTypeBuilder<Voluntariado> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Valor)
                .IsRequired()
                .HasColumnType("decimal(9,2)");

            builder.Property(e => e.DataHora)
                .IsRequired();

            builder.HasOne(d => d.DadosPessoais)
                .WithMany(p => p.Doacoes)
                .HasForeignKey(d => d.DadosPessoaisId);

            builder.HasOne(d => d.EnderecoCobranca)
                .WithMany(e => e.Doacoes)
                .HasForeignKey(d => d.EnderecoCobrancaId);
s
            builder.Ignore(e => e.FormaPagamento);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Voluntariado");
        }
    }
}