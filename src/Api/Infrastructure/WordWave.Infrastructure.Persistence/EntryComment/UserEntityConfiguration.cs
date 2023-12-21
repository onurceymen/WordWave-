using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordWave.Api.Domain.Models;
using WordWave.Infrastructure.Persistence.Context;

namespace WordWave.Infrastructure.Persistence.EntryComment;

public class UserEntityConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("user", WordWaveContext.DEFAULT_SCHEMA);
    }
}