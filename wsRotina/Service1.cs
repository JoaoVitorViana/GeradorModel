using System;
using System.Globalization;
using System.ServiceProcess;
using System.Threading;

namespace wsRotina
{
    public partial class Service1 : ServiceBase
    {
        protected Thread thServico = null;
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            thServico = new Thread(new ThreadStart(Processa));
            thServico.Priority = ThreadPriority.Highest;
            thServico.Start();
        }
        protected override void OnStop()
        {
            thServico.Abort();
        }
        public static void Processa()
        {
            TimeSpan tempo = new TimeSpan(0, 5, 0);

            while (true)
            {
                try
                {
                    Processamento.Processar();
                }
                catch (Exception ex)
                {
                    Pragma.Util.GravaLog(ex, "Servico");
                }
                finally
                {
                    Thread.Sleep(tempo);
                }
            }
        }
    }
}