using WordWave.Api.Application.Interfaces.Repository;
using WordWave.Api.Domain.Models;
using WordWave.Infrastructure.Persistence.Context;

namespace WordWave.Infrastructure.Persistence.Repositories;

public class EntryCommentRepository : GenericRepository<EntryComments>, IEntryCommentRepository
{
    public EntryCommentRepository(WordWaveContext dbContext) : base(dbContext)
    {
    }
}