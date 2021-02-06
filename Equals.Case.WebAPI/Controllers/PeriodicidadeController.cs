using System.Threading.Tasks;
using Equals.Case.Domain.Model;
using Equals.Case.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Equals.Case.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodicidadeController : ControllerBase
    {
        public readonly IEqualsCaseRepository _repo;

        public PeriodicidadeController(IEqualsCaseRepository repo)
        {
            _repo = repo;
        }



        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllPeriodicidades();
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{periodicidadeId}")]
        public async Task<IActionResult> Get(int periodicidadeId)
        {
            try
            {
                var results = await _repo.GetPeriodicidadeById(periodicidadeId);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Periodicidade periodicidade)
        {
            try
            {
                _repo.Add(periodicidade);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/Periodicidade/{periodicidade.PeriodicidadeId}", periodicidade);
                }

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

    }
}