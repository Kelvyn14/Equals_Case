using System;

namespace Equals.Case.Domain.Model
{
    public class Arquivo
    {
        public int? ArquivoId { get; set; }
        public string Nome { get; set; }
        public DateTime?  DataRecebimento { get; set; }
        public DateTime  DataPrevisaoRecebimento { get; set; }
        public int AdquirenteId { get; set; }
        public string Conteudo { get; set; }
        public bool Recebido { get; set; }
        public string ArquivoLocalPath { get; set; }
        public string ArquivoBackupPath { get; set; }


    }
}