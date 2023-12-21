using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordWave.Infrastructure.Persistence.Context;

namespace WordWave.Infrastructure.Persistence.EntryComment.EntryComment;

public class EntryCommentEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryComments>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryComments> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycomment", WordWaveContext.DEFAULT_SCHEMA);


        builder.HasOne(i => i.CreatedBy)
            .WithMany(i => i.EntryComments)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Entry)
            .WithMany(i => i.EntryComments)
            .HasForeignKey(i => i.EntryId);
    }
}