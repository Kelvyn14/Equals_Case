using System;

namespace Equals.Case.Domain.Model
{
    public class Arquivo
    {
        public int ArquivoId { get; set; }
        public string Nome { get; set; }
        public byte[] fileBytes { get; set; }
        public DateTime?  DataProcessamento { get; set; }
        public DateTime  DataPrevisaoRecebimento { get; set; }
        public int AdquirenteId { get; set; }
        public string Conteudo { get; set; }
        public bool Baixado { get; set; }
        public string ArquivoLocalPath { get; set; }
        public string ArquivoBackupPath { get; set; }
        public int TipoRegistro { get; set; }
        public long Estabelecimento { get; set; }

        public DateTime? PeriodoInicial { get; set; }
        public DateTime? PeriodoFinal { get; set; }
        public long NroSequencial { get; set; }

    }
}