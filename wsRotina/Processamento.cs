using Pragma;
using System;

namespace wsRotina
{
	public class Processamento
	{
		public static void Processar()
		{
			try
			{

			}
			catch (Exception ex)
			{
				Util.GravarLog(ex.ToString(), "Processar");
			}
		}
	}
}