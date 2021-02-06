
using System.Collections.Generic;

namespace Equals.Case.Domain.Model
{
    public class Adquirente
    {
        public int AdquirenteId { get; set; }
        public string Nome { get; set; }
        public int PeriodicidadeId { get; set; }
        public List<Arquivo> Arquivos { get; set; }

    }
}