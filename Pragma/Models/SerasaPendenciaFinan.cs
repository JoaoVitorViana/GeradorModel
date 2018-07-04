using System;
using System.ComponentModel.DataAnnotations;

namespace Pragma.Models
{
    public class SerasaPendenciaFinan
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64? IdSerasa { get; set; }
        public string Contrato { get; set; }
        public string Modalidade { get; set; }
        public DateTime? Data { get; set; }
        public double? Valor { get; set; }
        public string Origem { get; set; }
    }
}