using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class SmsEnvio
    {
        [Key]
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Mensagem { get; set; }
        public DateTime? DataHora { get; set; }
        public DateTime? DataHoraEnvio { get; set; }
        public bool? Enviado { get; set; }
        public DateTime? DataHoraErro { get; set; }
        public string Erro { get; set; }
        public int? Tentativa { get; set; }
    }
}
