using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordWave.Infrastructure.Persistence.Context;

namespace WordWave.Infrastructure.Persistence.EntryComment.Entity;

public class EntryEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Entry>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
    {
        base.Configure(builder);

        builder.ToTable("entry", WordWaveContext.DEFAULT_SCHEMA);


        builder.HasOne(i => i.CreatedBy)
            .WithMany(i => i.Entries)
            .HasForeignKey(i => i.CreatedById);
    }
}