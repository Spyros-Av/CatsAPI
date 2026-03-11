using AutoMapper;
using CatsAPI.Models.DTO;
using CatsAPI.Models.Entities;
using CatsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ICatService catService;
        private readonly IMapper mapper;

        public CatsController(ICatService catService, IMapper mapper)
        {
            this.catService = catService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? tag, [FromQuery] int page = 1, [FromQuery] int pagesize = 10)
        {
            var catEntity = new List<Cat>();

            if (string.IsNullOrEmpty(tag))
            {
                catEntity = await catService.GetAllAsync(page, pagesize);
            }
            else
            {
                catEntity = await catService.GetByTagAsync(tag, page, pagesize);
            }

            if (catEntity == null || catEntity.Count == 0)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<CatDto>>(catEntity));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var catEntity = await catService.GetByIdAsync(id);

            if (catEntity == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CatDto>(catEntity));
        }
    }
}
