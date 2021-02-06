using System;
using System.Threading.Tasks;
using Equals.Case.Domain.Model;
using Equals.Case.Domain.Utils;
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
        const string BASE_DIRECTORY_FILES = "H://Case Equals";
        public ArquivoController(IEqualsCaseRepository repo)
        {
            this._repo = repo;
        }


        [HttpGet("GetAllArquivosByAdquirenteId/{adquirenteId}")]
        public async Task<IActionResult> Get(int adquirenteId)
        {
            try
            {
                var result = await _repo.GetAllArquivosByAdquirenteId(adquirenteId);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost("PrevisaoRecebimento")]
        public async Task<IActionResult> Post(int adquirenteId, string previsaoInicio, string previsaoFinal)
        {
            try
            {
                DateTime initialPeriod = DateTime.Parse(previsaoInicio);
                DateTime finalPeriod = DateTime.Parse(previsaoFinal);
                int qtdDias = (int)(finalPeriod - initialPeriod).TotalDays + 1;
                var adquirente = await _repo.GetAdquirenteWithArquivosByAdquirenteId(adquirenteId);
                if (adquirente != null)
                {
                    if (adquirente.PeriodicidadeId == 1)
                    {
                        // diário                        
                        for (int i = 0; i < qtdDias; i++)
                        {
                            Arquivo arquivo = new Arquivo()
                            {
                                AdquirenteId = adquirente.AdquirenteId,
                                DataPrevisaoRecebimento = initialPeriod.AddDays(i),
                                Baixado = false
                            };
                            _repo.Add(arquivo);
                        }
                    }
                    else if (adquirente.PeriodicidadeId == 2)
                    {
                        //semanal
                        for (int i = 0; i < qtdDias; i = i + 7)
                        {
                            Arquivo arquivo = new Arquivo()
                            {
                                AdquirenteId = adquirente.AdquirenteId,
                                DataPrevisaoRecebimento = initialPeriod.AddDays(i + 7),
                                Baixado = false
                            };
                            _repo.Add(arquivo);
                        }
                    }
                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/Adquirente/{adquirente.AdquirenteId}", adquirente);
                    }
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Requisição falhou!");
            }
            return BadRequest();
        }



        [HttpPost]
        public async Task<IActionResult> Post(Arquivo arquivo)
        {
            try
            {
                string fileName = "";
                string fileContent = "";
                if( arquivo.fileBytes != null && arquivo.fileBytes.Length > 0)
                {
                    fileName = ArquivoHelper.ConvertBytesToFile(BASE_DIRECTORY_FILES, arquivo.fileBytes);
                    fileContent = ArquivoHelper.ReadFileContent(BASE_DIRECTORY_FILES, fileName);
                }
                else
                {
                    fileContent = arquivo.Nome;
                }
                 
                
                var arquivoPrevisto = new Arquivo();
                var adquirente = new Adquirente();
                long nroSequencial = 0;
                string adquirenteNome = "";
                long estabelecimento = 0;
                DateTime dataProcessamento, periodoIni, periodoFim;
                if (!string.IsNullOrEmpty(fileContent))
                {
                    int tipo = int.Parse(fileContent.Substring(0, 1));
                    switch (tipo)
                    {
                        // tipo '0'
                        case 0:
                            estabelecimento = long.Parse(fileContent.Substring(1, 10));
                            dataProcessamento = new DateTime(int.Parse(fileContent.Substring(11, 4)),
                                                                        int.Parse(fileContent.Substring(15, 2)),
                                                                        int.Parse(fileContent.Substring(17, 2)));
                            periodoIni = new DateTime(int.Parse(fileContent.Substring(19, 4)),
                                                                       int.Parse(fileContent.Substring(23, 2)),
                                                                       int.Parse(fileContent.Substring(25, 2)));
                            periodoFim = new DateTime(int.Parse(fileContent.Substring(27, 4)),
                                                                        int.Parse(fileContent.Substring(31, 2)),
                                                                        int.Parse(fileContent.Substring(33, 2)));
                            nroSequencial = long.Parse(fileContent.Substring(35, 7));
                            adquirenteNome = fileContent.Substring(42, 8);
                            adquirente = await _repo.GetAdquirenteByName(adquirenteNome);
                            arquivoPrevisto = await _repo.GetArquivoByDataPrevisaoEAdquirenteId(dataProcessamento, adquirente.AdquirenteId);
                            if (adquirente != null && arquivoPrevisto != null && !string.IsNullOrEmpty(fileContent))
                            {
                                arquivoPrevisto.TipoRegistro = 0;
                                arquivoPrevisto.Estabelecimento = estabelecimento;
                                arquivoPrevisto.PeriodoInicial = periodoIni;
                                arquivoPrevisto.PeriodoFinal = periodoFim;
                                arquivoPrevisto.DataProcessamento = dataProcessamento;
                                arquivoPrevisto.Baixado = true;
                                arquivoPrevisto.ArquivoLocalPath = $"{BASE_DIRECTORY_FILES}/files/{fileName}";
                                arquivoPrevisto.ArquivoBackupPath = $"{BASE_DIRECTORY_FILES}/filesBackup/{fileName}";
                                arquivoPrevisto.NroSequencial = nroSequencial;
                            }
                            break;

                        // tipo '1'
                        case 1:
                            dataProcessamento = new DateTime(int.Parse(fileContent.Substring(1, 4)),
                                                                          int.Parse(fileContent.Substring(5, 2)),
                                                                          int.Parse(fileContent.Substring(7, 2)));
                            estabelecimento = long.Parse(fileContent.Substring(9, 8));
                            adquirenteNome = fileContent.Substring(17, 12);
                            nroSequencial = long.Parse(fileContent.Substring(29, 7));

                            adquirente = await _repo.GetAdquirenteByName(adquirenteNome);
                            arquivoPrevisto = await _repo.GetArquivoByDataPrevisaoEAdquirenteId(dataProcessamento, adquirente.AdquirenteId);

                            if (adquirente != null && arquivoPrevisto != null && !string.IsNullOrEmpty(fileContent))
                            {
                                arquivoPrevisto.TipoRegistro = 1;
                                arquivoPrevisto.Estabelecimento = estabelecimento;                                
                                arquivoPrevisto.DataProcessamento = dataProcessamento;
                                arquivoPrevisto.Baixado = true;
                                arquivoPrevisto.ArquivoLocalPath = $"{AppDomain.CurrentDomain.BaseDirectory}/files/{fileName}";
                                arquivoPrevisto.ArquivoBackupPath = $"{AppDomain.CurrentDomain.BaseDirectory}/filesBackup/{fileName}";
                                arquivoPrevisto.NroSequencial = nroSequencial;

                            }
                            break;
                    }



                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/Arquivo/{arquivoPrevisto.ArquivoId}", arquivoPrevisto);
                    }
                }

            }
            catch (System.Exception ex)
            {
                string message = ex.Message;
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Requisição falhou!");
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int arquivoId)
        {
            try
            {
                var arquivo = await _repo.GetArquivoById(arquivoId);
                if (arquivo == null) return NotFound();

                _repo.Update(arquivo);
                if (await _repo.SaveChangesAsync())
                    return Created($"/api/arquivo/{arquivo.ArquivoId}", arquivo);


            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int arquivoId)
        {
            try
            {
                var arquivo = await _repo.GetArquivoById(arquivoId);
                if (arquivo == null) return NotFound();

                _repo.Delete(arquivo);
                if (await _repo.SaveChangesAsync())
                    return Ok();


            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }
    }
}