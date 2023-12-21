using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordWave.Api.Domain.Models;
using WordWave.Infrastructure.Persistence.Context;

namespace WordWave.Infrastructure.Persistence.EntryComment.EntryComment;

public class EntryCommentVoteEntityConfiguration : BaseEntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycommentvote", WordWaveContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.EntryComment)
            .WithMany(i => i.EntryCommentVotes)
            .HasForeignKey(i => i.EntryCommentId);
    }
}