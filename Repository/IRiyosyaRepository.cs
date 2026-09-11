using MyAppApi.Models;

namespace MyAppApi.Repository
{
    public interface IRiyosyaRepository
    {
        Task<List<Riyosya>> GetRiyosyaListAsync();

        Task<Riyosya?> GetRiyosyaByIdAsync(string id);

        Task AddAsync (Riyosya riyosya);
    }
}
