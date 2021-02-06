using System.Linq;
using System.Threading.Tasks;
using Equals.Case.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Equals.Case.Repository
{
    public class EqualsCaseRepository : IEqualsCaseRepository
    {
        public readonly EqualsCaseContext _context;

        public EqualsCaseRepository(EqualsCaseContext context)
        {
            _context = context;
        }

        //GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //ARQUIVO
         public async Task<Arquivo[]> GetAllArquivosByAdquirenteId(int AdquirenteId)
        {
            IQueryable<Arquivo> query = _context.Arquivos.Where(x => x.AdquirenteId == AdquirenteId);

            return await query.ToArrayAsync();
        }

        //ADQUIRENTE
         public async Task<Adquirente[]> GetAllAdquirentes()
        {
            IQueryable<Adquirente> query = _context.Adquirentes
                                        .Include(a => a.Arquivos);
            return await query.ToArrayAsync();
        }
        public async Task<Adquirente[]> GetAllAdquirentesWithArquivosByAdquirenteId(int AdquirenteId)
        {
            IQueryable<Adquirente> query = _context.Adquirentes
                                        .Include(a => a.Arquivos)
                                        .Where(x => x.AdquirenteId == AdquirenteId);

            return await query.ToArrayAsync();
        }

    }
}