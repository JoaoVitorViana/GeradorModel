namespace Model
{
    public class Retorno
    {
        public bool Erro { get; set; }
        public string Mensagem { get; set; }
        public Retorno()
        {
            this.Erro = false;
            this.Mensagem = string.Empty;
        }
    }
}