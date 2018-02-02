using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleNetCoreApi.Infrastructure;

namespace SimpleNetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Models")]
    public class ModelsController : Controller
    {
        private readonly DefaultContext _context;
        private readonly IMapper _mapper;

        public ModelsController(DefaultContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ModelDto[]), 200)]
        public async Task<IActionResult> Get()
        {
            var models = await _context.Models.ToArrayAsync();
            return Ok(_mapper.Map<ModelDto[]>(models));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Id == id);
            var dto = _mapper.Map<ModelDto>(model);
            return dto != null ? (IActionResult)Ok(dto) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post([FromBody]ModelDto dto)
        {
            var model = _mapper.Map<Model>(dto);
            await _context.Models.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(int id, [FromBody]ModelDto dto)
        {
            var model = _mapper.Map<Model>(dto);
            if (id != model.Id)
            {
                return BadRequest();
            }
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Models.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ModelDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ModelDto>(model));
        }
    }
}
