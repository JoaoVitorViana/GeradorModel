using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class Cliente
    {
        [Key]
        public int ID { get; set; }
        public string NomeFatansia { get; set; }
        public string RazaoSocial { get; set; }
        public string TipoLogradouro { get; set; }
        public string NomeLogradouro { get; set; }
        public string NumeroLogradouro { get; set; }
        public string ComplementoLogradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string DDDTelefone { get; set; }
        public string Telefone { get; set; }
        public string DDDCelular { get; set; }
        public string Celular { get; set; }
        public string RG_IE { get; set; }
        public string CPF_CNPJ { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? IDTipoCliente { get; set; }
        public int? IDFilialSAP { get; set; }
        public int? IDFilialSGV { get; set; }
        public int IDTerritorio { get; set; }
        public int IDSistema { get; set; }
        public string ChaveSistema { get; set; }
        public string CodigoSAP88 { get; set; }
        public string CodigoSAP90 { get; set; }
        public string CodigoSAP91 { get; set; }
        public string CodigoClienteSGV { get; set; }
        public string Observacao { get; set; }
        public string Ativo { get; set; }
        public DateTime DataImportacao { get; set; }
        public DateTime? DataIntegracao { get; set; }
        public string Integrado { get; set; }
        public string DDDCEP { get; set; }
        public string DDDCidade { get; set; }
        public string DDDFilial { get; set; }
        public int? IDSegmentoSGV { get; set; }
        public int? ID_SLA_CLASSE { get; set; }
        public int? IDAplic { get; set; }
        public int? DiaVisita { get; set; }
        public int? OrdemVisita { get; set; }
        public string IDeFresh { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Precisao { get; set; }
        public DateTime? DataGps { get; set; }
        public string CurvaChip { get; set; }
        public string CurvaRecarga { get; set; }
        public string email { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public DateTime? DtUltimaVisita { get; set; }
        public string Origem { get; set; }
        public string SenhaOperador { get; set; }
        public string DiaSemanaCobranca { get; set; }
        public string SenhaEstabelecimentoSGV { get; set; }
        public string CodigoMuxx { get; set; }
        public string NomeContato { get; set; }
        public string CurvaPlanner { get; set; }
        public string CEPCorrigido { get; set; }
        public string CurvaAdquirencia { get; set; }
        public bool Adquirencia { get; set; }
        public string UsuarioCriacaoInterno { get; set; }
        public int? UsuarioCriacaoExterno { get; set; }
        public string UsuarioAtualizacaoInterno { get; set; }
        public int? UsuarioAtualizacaoExterno { get; set; }
        public string IpCriacaoInterno { get; set; }
        public string IpCriacaoExterno { get; set; }
        public string IpAtualizacaoInterno { get; set; }
        public string IpAtualizacaoExterno { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}