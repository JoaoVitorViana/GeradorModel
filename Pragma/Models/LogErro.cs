using System;
using System.ComponentModel.DataAnnotations;

namespace Pragma.Models
{
    public partial class LogErro
    {
        [Key]
        public Int64 Id { get; set; }
        public string Projeto { get; set; }
        public DateTime? DataHora { get; set; }
        public string Erro { get; set; }
        public string Erro_Completo { get; set; }
    }
}