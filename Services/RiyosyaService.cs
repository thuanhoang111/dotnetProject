using MyAppApi.Models;
using MyAppApi.Repository;

namespace MyAppApi.Services
{
    public class RiyosyaService : IRiyosyaService
    {
        private readonly IRiyosyaRepository _repository;


        public RiyosyaService(IRiyosyaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Riyosya?> GetRiyosyaByIdAsync(string id)
        {
            return await _repository.GetRiyosyaByIdAsync(id);
        }

        public async Task<List<Riyosya>> GetRiyosyaListAsync()
        {
            return await _repository.GetRiyosyaListAsync();
        }
    }
}
