using System.Threading.Tasks;
using Equals.Case.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Equals.Case.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdquirenteController : ControllerBase
    {
        public readonly IEqualsCaseRepository _repo;
        public AdquirenteController(IEqualsCaseRepository repo)
        {
            _repo = repo;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllAdquirentes();
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        // GET api/values/5
        [HttpGet("{AdquirenteId}")]
        public async Task<IActionResult> Get(int AdquirenteId)
        {
            try
            {
                var result = await _repo.GetAllAdquirentesWithArquivosByAdquirenteId(AdquirenteId);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

    }
}