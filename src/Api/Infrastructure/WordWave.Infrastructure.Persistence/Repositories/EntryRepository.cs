using WordWave.Api.Application.Interfaces.Repository;
using WordWave.Api.Domain.Models;
using WordWave.Infrastructure.Persistence.Context;

namespace WordWave.Infrastructure.Persistence.Repositories;

public class EntryRepository : GenericRepository<Entry>, IEntryRepository
{
    public EntryRepository(WordWaveContext dbContext) : base(dbContext)
    {
    }
}