using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Serasa
    {
        [Key]
        public Int64 Id { get; set; }
        [NotMapped]
        private string _situacao;
        public string CpfCnpj { get; set; }
        public string NomeRazao { get; set; }
        public string TipoPessoa { get; set; }
        public DateTime? DataNascimentoFundacao { get; set; }
        public DateTime DataConsulta { get; set; }
        public string ChaveConsulta { get; set; }
        public string Situacao
        {
            get
            {
                switch (_situacao)
                {
                    case "-1": return "CPF / CNPJ Não Existente Na Base Do Serasa Ate Esta Data";
                    case "1":
                        return "Nada Consta";
                    case "2":
                        return "Ocorrência de Confirmei";
                    case "3":
                        return "Ocorrência de Recheque";
                    case "4": return "Ocorrência de Pendência Interna / Financeira";
                    case "5": return "Ocorrência de CCF Varejo / Bacen";
                    case "6": return "Ocorrência de Contumácia";
                    case "7": return "Mais de um tipo de ocorrência";
                    case "8":
                        return "Ocorrência de Protestos";
                }
                return _situacao;
            }
            set { _situacao = value.Trim(); }
        }
        [NotMapped]
        public List<SerasaPendenciaFinan> PendenciasFinanceiras { get; set; }
        [NotMapped]
        public List<SerasaPendenciaCheque> PendenciasChequesSemFundo { get; set; }
        public string Arquivo { get; set; }
    }
}