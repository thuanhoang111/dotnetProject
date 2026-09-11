using Microsoft.EntityFrameworkCore;
using MyAppApi.Data;
using MyAppApi.Models;

namespace MyAppApi.Repository
{
    public class RiyosyaRepository : IRiyosyaRepository
    {
        private readonly AppDbContext _context;

        public RiyosyaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Riyosya>> GetRiyosyaListAsync()
        {
            return await _context.Riyosya
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Riyosya?> GetRiyosyaByIdAsync(string id)
        {
            return await _context.Riyosya
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Riyosha_Id == id);
        }

        public async Task AddAsync(Riyosya Riyosya)
        {
            await _context.Riyosya.AddAsync(Riyosya);

            await _context.SaveChangesAsync();
        }
    }
}
