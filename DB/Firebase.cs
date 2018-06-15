using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{
    public class Firebase
    {
        private ChildQuery FireQuery;

        public Firebase(string Url, string Usuario)
        {
            this.FireQuery = new FirebaseClient(Url).Child(Usuario);
        }

        public void DeletarBase()
        {
            this.FireQuery.DeleteAsync().Wait();
        }

        public void Deletar<T>(string pCodigo)
        {
            this.FireQuery.Child(typeof(T).Name).Child(pCodigo).DeleteAsync().Wait();
        }

        public void Salvar<T>(string pCodigo, T pObjeto)
        {
            this.FireQuery.Child(pObjeto.GetType().Name).Child(pCodigo).PutAsync(JsonConvert.SerializeObject(pObjeto)).Wait();
        }

        public List<T> GetSelect<T>()
        {
            List<T> ret = new List<T>();
            Task t = Task.Run(async () =>
            {
                var retorno = await this.FireQuery.Child(typeof(T).Name).OrderByKey().OnceAsync<object>();
                if (retorno != null)
                {
                    foreach (var item in retorno)
                    {
                        ret.Add(JsonConvert.DeserializeObject<T>(item.Object.ToString()));
                    }
                }

            });
            t.Wait();

            return ret;
        }
    }
}