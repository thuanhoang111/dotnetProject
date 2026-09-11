using MyAppApi.Models;

namespace MyAppApi.Services
{
    public interface IRiyosyaService
    {
        Task<List<Riyosya>> GetRiyosyaListAsync();

        Task<Riyosya?> GetRiyosyaByIdAsync(string id);
    }
}
