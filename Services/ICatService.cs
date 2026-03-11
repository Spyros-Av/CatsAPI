using CatsAPI.Models.Entities;

namespace CatsAPI.Services
{
    public interface ICatService
    {
        Task<List<Cat>> GetAllAsync(int pageNumber, int pageSize);
        Task<Cat> GetByIdAsync(int id);
        Task<List<Cat>> GetByTagAsync(string tag, int pageNumber, int pageSize);

    }
}
