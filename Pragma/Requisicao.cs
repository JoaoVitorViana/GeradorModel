using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Pragma
{
	public class Requisicao
	{
		private byte[] JsonToByte(string pJson)
		{
			if (pJson != null)
				return new UTF8Encoding(false).GetBytes(pJson);
			return null;
		}

		public string RequisicaoWeb(string pUrl) => RequisicaoWeb(pUrl, null, null, null, null, 0);

		public string RequisicaoWeb(string pUrl, string pMetodo, NetworkCredential pCredencial, string pContentType, string pBody) => RequisicaoWeb(pUrl, pMetodo, pCredencial, pContentType, pBody, 0);

		public string RequisicaoWeb(string pUrl, string pMetodo, NetworkCredential pCredencial, string pContentType, string pBody, int pTimeOut)
		{
			try
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(pUrl);
				req.Timeout = (pTimeOut > 0) ? pTimeOut : 15000;
				if (pMetodo != null)
					req.Method = pMetodo;
				if (pCredencial != null)
					req.Credentials = pCredencial;
				if (pContentType != null)
					req.ContentType = pContentType;
				byte[] serializedJson = JsonToByte(pBody);
				if (serializedJson != null)
				{
					using (var sr = req.GetRequestStream())
					{
						sr.Write(serializedJson, 0, serializedJson.Length);
					}
				}

				string stream = string.Empty;
				try
				{
					HttpWebResponse res = (HttpWebResponse)req.GetResponse();
					stream = new StreamReader(res.GetResponseStream()).ReadToEnd();
					if (res.StatusCode != HttpStatusCode.OK)
						throw new Exception("Erro na requisição web");
				}
				catch (WebException ex)
				{
					if (ex != null && ex.Response != null)
						stream = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
					else
						stream = ex.Message;
					throw new Exception("Erro: " + stream);
				}
				return stream;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public T DeserializarObjeto<T>(string pJson) => JsonConvert.DeserializeObject<T>(pJson);

		public string SerializarObjeto<T>(T pObjeto) => JsonConvert.SerializeObject(pObjeto);
	}
}