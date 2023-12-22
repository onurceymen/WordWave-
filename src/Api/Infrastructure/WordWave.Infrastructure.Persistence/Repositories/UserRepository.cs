using WordWave.Api.Application.Interfaces.Repository;
using WordWave.Api.Domain.Models;
using WordWave.Infrastructure.Persistence.Context;

namespace WordWave.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(WordWaveContext dbContext) : base(dbContext)
    {
    }
}