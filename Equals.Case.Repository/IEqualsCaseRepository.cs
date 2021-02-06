using System;
using System.Threading.Tasks;
using Equals.Case.Domain.Model;

namespace Equals.Case.Repository
{
    public interface IEqualsCaseRepository
    {
        //GERAL
         void Add<T>(T entity) where T:class;
         void Update<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;

         Task<bool> SaveChangesAsync();

         Task<Arquivo[]> GetAllArquivosByAdquirenteId(int AdquirenteId);
         Task<Adquirente> GetAdquirenteWithArquivosByAdquirenteId(int AdquirenteId);
         Task<Adquirente[]> GetAllAdquirentes();
         Task<Adquirente> GetAdquirenteByName(string AdquirenteNome);
         Task<Periodicidade[]> GetAllPeriodicidades();
         Task<Periodicidade> GetPeriodicidadeById(int periodicidadeId);
         Task<Arquivo> GetArquivoById(int arquivoId);
         Task<Arquivo> GetArquivoByDataPrevisaoEAdquirenteId(DateTime DataPrevisaoProcessamento, int AdquirenteId);
    }
}