using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordWave.Api.Domain.Models;
using WordWave.Infrastructure.Persistence.Context;

namespace WordWave.Infrastructure.Persistence.EntryComment.Entity;

public class EntryVoteEntityConfiguration : BaseEntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entryvote", WordWaveContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.Entry)
            .WithMany(i => i.EntryVotes)
            .HasForeignKey(i => i.EntryId);
    }
}