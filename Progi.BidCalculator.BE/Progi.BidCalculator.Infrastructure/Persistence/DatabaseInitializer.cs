using Microsoft.EntityFrameworkCore;
using Progi.BidCalculator.Domain.Interfaces;

namespace Progi.BidCalculator.Infrastructure.Persistence;

public class DatabaseInitializer(BidCalculatorDbContext dbContext) : IDatabaseInitializer
{
    private readonly BidCalculatorDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public void Initialize()
    {
        _dbContext.Database.EnsureCreated();
    }
}

