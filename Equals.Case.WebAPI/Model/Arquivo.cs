using System;

namespace Equals.Case.WebAPI.Model
{
    public class Arquivo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime?  DataRecebimento { get; set; }
        public int AdquirenteId { get; set; }
        public string Conteudo { get; set; }
        public bool Recebido { get; set; }


    }
}