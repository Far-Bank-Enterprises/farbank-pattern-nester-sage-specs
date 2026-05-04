public class PatternNesterService
{
    private readonly MyDbContext _dbContext;
    public PatternNesterService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DoSomethingAsync()
    {
        // Business logic using _dbContext
    }
}