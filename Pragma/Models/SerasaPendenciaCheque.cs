using System;
using System.ComponentModel.DataAnnotations;

namespace Pragma.Models
{
    public class SerasaPendenciaCheque
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64? IdSerasa { get; set; }
        public string Banco { get; set; }
        public string Cheque { get; set; }
        public string QuantidadeCheques { get; set; }
        public string Agencia { get; set; }
        public DateTime? Data { get; set; }
        public double? Valor { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
    }
}