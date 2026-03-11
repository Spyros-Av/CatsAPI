using CatsAPI.Data;
using CatsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatsAPI.Services
{
    public class SqlCatService : ICatService
    {
        private readonly CatsDbContext dbContext;

        public SqlCatService(CatsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Cat>> GetAllAsync(int pageNumber, int pageSize)
        {
            var skipResults = (pageNumber - 1) * pageSize;
            
            return await dbContext.Cats
                .Skip(skipResults)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Cat> GetByIdAsync(int id)
        {
            return await dbContext.Cats
                .Include(c => c.Tags)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Cat>> GetByTagAsync(string tag, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
