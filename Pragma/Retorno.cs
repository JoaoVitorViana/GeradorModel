using System;

namespace Pragma
{
    public class Retorno
    {
        public bool Ok { get; set; }
        public string Erro { get; set; }
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public Retorno()
        {
            this.Ok = true;
            this.Erro = string.Empty;
            this.Id = string.Empty;
            this.Data = DateTime.Now;
        }
    }
}