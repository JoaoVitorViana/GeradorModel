using System;
using System.ComponentModel.DataAnnotations;

namespace Pragma.Models
{
    public partial class ErroSync
    {
        [Key]
        public Int64 ErroSyncId { get; set; }
        public bool? Enviado { get; set; }
        public Int32? IdColaborador { get; set; }
        public string Entidade { get; set; }
        public Int32? IdAppMobile { get; set; }
        public DateTime? DataHora { get; set; }
    }
}