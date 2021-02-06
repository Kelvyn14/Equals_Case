using System.Threading.Tasks;
using Equals.Case.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Equals.Case.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        public readonly IEqualsCaseRepository _repo;
        public ArquivoController(IEqualsCaseRepository repo)
        {
            this._repo = repo;
        }

        // GET api/values/5
        [HttpGet("GetAllArquivosByAdquirenteId/{AdquirenteId}")]
        public async Task<IActionResult> Get(int AdquirenteId)
        {
            try
            {
                var result = await _repo.GetAllArquivosByAdquirenteId(AdquirenteId);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }
    }
}