using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAppApi.Services;
using MyAppApi.DTOs;
using MyAppApi.Models;

namespace MyAppApi.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class RiyosyaController : ControllerBase
    {
        private readonly IRiyosyaService _riyosyaService;
        public RiyosyaController(IRiyosyaService riyosyaService)
        {
            _riyosyaService = riyosyaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRiyosyaList()
        {
            List<Riyosya> riyosyaList = await _riyosyaService.GetRiyosyaListAsync();

            List<RiyosyaResponse> responseList = riyosyaList.Select(riyosya => new RiyosyaResponse
            {
                RiyosyaId = riyosya.Riyosha_Id,
                agriCode = riyosya.agri_code ?? 0,
                name = riyosya.name ?? string.Empty,
                lebel = riyosya.lebel?.ToString() ?? string.Empty,
                tel = riyosya.tel ?? string.Empty,
                uniKCode = riyosya.uni_k_code ?? 0,
                loginYear = riyosya.login_year ?? 0
            }).ToList();


            return Ok(responseList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRiyosyaById(string id)
        {
            var riyosya = await _riyosyaService.GetRiyosyaByIdAsync(id);
            if (riyosya == null)
            {
                return NotFound();
            }
            return Ok(new RiyosyaResponse
            {
                RiyosyaId = riyosya.Riyosha_Id,
                agriCode = riyosya.agri_code ?? 0,
                name = riyosya.name ?? string.Empty,
                lebel = riyosya.lebel?.ToString() ?? string.Empty,
                tel = riyosya.tel ?? string.Empty,
                uniKCode = riyosya.uni_k_code ?? 0,
                loginYear = riyosya.login_year ?? 0
            });
        }
    }
}
