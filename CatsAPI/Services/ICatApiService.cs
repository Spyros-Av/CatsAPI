using CatsAPI.Models.ApiResponses;

namespace CatsAPI.Services
{
    public interface ICatApiService
    {
        Task<List<CatApiResponses>> FetchCatsFromApiAsync(int count = 25);
    }
}
