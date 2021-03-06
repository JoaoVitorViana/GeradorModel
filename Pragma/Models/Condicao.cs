﻿using Pragma.Models;
using System.Collections.Generic;

namespace Pragma
{
    public class Condicao
	{
        public string Where { get; set; }
        public string Identificador { get; set; }
        public string Campos { get; set; }
        public List<Parametro> pParametros { get; set; }
		
		public Condicao(List<Campos> pCampos)
        {
            string Parametros = string.Empty;
            string ParametrosWhere = string.Empty;
            string Id = string.Empty;
            pParametros = new List<Parametro>();
            if (pCampos != null && pCampos.Count > 0)
            {
                for (int i = 0; i < pCampos.Count; i++)
                {
                    Id += IO.ToTitleCase(pCampos[i].Nome);
                    string _parametro = Java.GetParametroJava(pCampos[i]);
                    ParametrosWhere += Java.GetTypeParametroJava(pCampos[i], _parametro);
                    Parametros += pCampos[i].Tipo.Java + " " + _parametro + ", ";
                    pParametros.Add(new Parametro
					{
                        Campos = pCampos[i].Nome,
                        Valor = Java.GetTypeParametroJava(pCampos[i], _parametro).Replace(",", "").Trim()
                    });
                }

                this.Identificador = Id;
                this.Campos = Parametros.Substring(0, Parametros.Length - 2);
                this.Where = ParametrosWhere.Substring(0, ParametrosWhere.Length - 2);
            }
        }
    }
}