using CatsAPI.Data;
using CatsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatsAPI.Services
{
    public class SqlCatService : ICatService
    {
        private readonly CatsDbContext dbContext;
        private readonly ICatApiService catApiService;

        public SqlCatService(CatsDbContext dbContext, ICatApiService catApiService)
        {
            this.dbContext = dbContext;
            this.catApiService = catApiService;
        }

        public async Task<int> FetchAndSaveCatsAsync()
        {

            var catApiResponses = await catApiService.FetchCatsFromApiAsync(25);

            var savedCount = 0;

            foreach (var cat in catApiResponses)
            {
                var existingCat = await dbContext.Cats
                    .FirstOrDefaultAsync(c => c.CatId == cat.Id);

                if (existingCat != null)
                {
                    continue;
                }

                var newCat = new Cat
                {
                    CatId = cat.Id,
                    Image = cat.Url,
                    Width = cat.Width,
                    Height = cat.Height,
                    Created = DateTime.Now
                };

                if (cat.Breeds != null && cat.Breeds.Any())
                {
                    var temperament = cat.Breeds[0].Temperament;

                    if (!string.IsNullOrWhiteSpace(temperament))
                    {
                        var tagNames = temperament
                            .Split(',')
                            .Select(t => t.Trim())
                            .Where(t => !string.IsNullOrWhiteSpace(t))
                            .ToList();

                        foreach (var tagName in tagNames)
                        {
                            var existingTag = await dbContext.Tags
                                .FirstOrDefaultAsync(t => t.Name == tagName);

                            if (existingTag == null)
                            {
                                existingTag = new Tag
                                {
                                    Name = tagName,
                                    Created = DateTime.Now
                                };
                                dbContext.Tags.Add(existingTag);
                            }

                            newCat.Tags.Add(existingTag);
                        }
                    }
                }

                dbContext.Cats.Add(newCat);
                savedCount++;
            }
            await dbContext.SaveChangesAsync();
            return savedCount;
        }

        public async Task<List<Cat>> GetAllAsync(int pageNumber, int pageSize)
        {
            var skipResults = (pageNumber - 1) * pageSize;
            
            return await dbContext.Cats
                .Include(c => c.Tags)
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

        public async Task<List<Cat>> GetByTagAsync(string tag, int pageNumber, int pageSize)
        {
            var skipResults = (pageNumber - 1) * pageSize;

            return await dbContext.Cats
                .Include(c => c.Tags)
                .Where(c => c.Tags.Any(t => t.Name.ToLower() == tag.ToLower()))
                .Skip(skipResults)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
