using DB;
using Pragma.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Pragma
{
	public class Mobile
    {
        /// <summary>
        /// Método responsável pela geração da classe de banco de dados do Mobile
        /// </summary>
        /// <param name="pProjeto">Projeto Android</param>
        /// <param name="pClasse"></param>
        /// <param name="pTabela"></param>
        /// <param name="pBanco"></param>
        /// <param name="pObjeto"></param>
        /// <param name="pPackageObjeto"></param>
        /// <param name="pVariavelTabela"></param>
        public static void GerarDB(string pProjeto, string pClasse, string pTabela, string pBanco, string pObjeto, string pPackageObjeto, string pVariavelTabela)
        {
            string nome_classe = pClasse;
            if (!pClasse.StartsWith("DB"))
                nome_classe = "DB" + pClasse;

            Tabela tabela = GetTabelaInfo(pTabela, pBanco);
            var chaves = tabela.Campos.Where(i => i.Chave == true).ToList();

            if (string.IsNullOrWhiteSpace(pObjeto))
                pObjeto = "Object";

            string nome_tabela = pVariavelTabela;
            if (string.IsNullOrWhiteSpace(pVariavelTabela))
                nome_tabela = "mTabela";

			Condicao parametros = new Condicao(chaves);
            string nome_getLista = "get" + pObjeto;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("package " + pProjeto.ToLower() + ".bd;");
            sb.AppendLine("");
            sb.AppendLine("import android.database.sqlite.SQLiteDatabase;");
            sb.AppendLine("");
            sb.AppendLine("import " + pProjeto.ToLower() + "." + pPackageObjeto + "." + pObjeto + ";");
            sb.AppendLine("import com.viana.androidutil.db.Generic;");
            sb.AppendLine("import com.viana.androidutil.db.Operador;");
            sb.AppendLine("import com.viana.androidutil.db.Parametro;");
            sb.AppendLine("import com.viana.androidutil.mobile.Util;");
            sb.AppendLine("");
            sb.AppendLine("import java.util.ArrayList;");
            sb.AppendLine("");
            sb.Append(Util.GetUser());
            sb.AppendLine("");
            sb.AppendLine("public class " + nome_classe + " {");
            sb.AppendLine("    private SQLiteDatabase mSqLiteDatabase;");
            sb.AppendLine("    private String " + nome_tabela + " = \"" + tabela.Nome + "\";");
            sb.AppendLine("    private Generic mGeneric;");
            sb.AppendLine("");
            sb.AppendLine("    public " + nome_classe + "(SQLiteDatabase pSqLiteDatabase) {");
            sb.AppendLine("        this.mSqLiteDatabase = pSqLiteDatabase;");
            sb.AppendLine("        this.mGeneric = new Generic(pSqLiteDatabase);");
            sb.AppendLine("    }");

            if (tabela.ExisteChave)
            {
                sb.AppendLine();
                sb.AppendLine("    public " + pObjeto + " getBy" + parametros.Identificador + "(" + parametros.Campos + ") {");
                sb.AppendLine("        try {");
                sb.AppendLine("            ArrayList<Parametro> pmts = new ArrayList<>();");
                parametros.pParametros.ForEach((parametro) =>
                {
                    sb.AppendLine("            pmts.add(new Parametro(\"" + parametro.Campos + "\", " + parametro.Valor + ", Operador.Igual));");
                });
                sb.AppendLine("            return Util.firstOrDefault((this.mGeneric.returnList(Motivo.class, pmts)));");
                sb.AppendLine("        } catch (Exception ex) {");
                sb.AppendLine("            ex.printStackTrace();");
                sb.AppendLine("            return null;");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine();
                sb.AppendLine("    public void deleteBy" + parametros.Identificador + "(" + parametros.Campos + ") {");
                sb.AppendLine("        this.mSqLiteDatabase.delete(" + nome_tabela + "," + tabela.CamposValores + " , new String[]{" + parametros.Where + "});");
                sb.AppendLine("    }");
            }
            sb.AppendLine();
            sb.AppendLine("    public void deleteAll() {");
            sb.AppendLine("        this.mSqLiteDatabase.delete(" + nome_tabela + ", null, null);");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    public ArrayList<" + pObjeto + "> " + nome_getLista + "() {");
            sb.AppendLine("        try {");
            sb.AppendLine("            ArrayList<Parametro> pmts = null;");
            sb.AppendLine("            //TODO informar parametrôs");
            sb.AppendLine("");
            sb.AppendLine("            return this.mGeneric.returnList(" + pObjeto + ".class, pmts);");
            sb.AppendLine("        } catch (Exception ex) {");
            sb.AppendLine("            ex.printStackTrace();");
            sb.AppendLine("            return new ArrayList<>();");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.Append("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), nome_classe);
        }

        public static Tabela GetTabelaInfo(string pTabela, string pBanco)
        {
            Tabela tabela = new Tabela();
            tabela.Nome = pTabela.Trim();

            SqlLite dbMobile = new SqlLite(pBanco);
            DataTable dt = dbMobile.ExecuteDataTable(string.Format("PRAGMA table_info('{0}')", tabela.Nome));

            List<Campos> campos = new List<Campos>();
            string campoChave = string.Empty;
            string valoresChaves = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                Campos campo = new Campos();
                campo.Nome = dr[1].ToString();
                campo.NotNull = Convert.ToBoolean(dr[3]);
                campo.Tipo = new TipoBanco(dr[2].ToString(), campo.NotNull);
                campo.Chave = Convert.ToBoolean(dr[5]);
                campos.Add(campo);

                if (campo.Chave)
                {
                    campoChave += "\"" + campo.Nome + "\", ";
                    valoresChaves += "[" + campo.Nome + "]" + "=? AND ";
                }
            }

            tabela.ExisteChave = campos.Where(i => i.Chave == true).Any();
            if (tabela.ExisteChave)
            {
                if (campoChave.Length > 0)
                    tabela.CamposChaves = campoChave.Substring(0, campoChave.Length - 2);
                if (valoresChaves.Length > 0)
                    tabela.CamposValores = "\"" + valoresChaves.Substring(0, valoresChaves.Length - 5) + "\"";
            }

            tabela.Campos = campos;
            return tabela;
        }

        public static void GerarLayout(string pNome, TipoLayout pTipoLayout)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            switch (pTipoLayout)
            {
                case TipoLayout.LinearVertical:
                    sb.AppendLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    sb.AppendLine("    android:layout_width=\"match_parent\"");
                    sb.AppendLine("    android:layout_height=\"wrap_content\"");
                    sb.AppendLine("    android:orientation=\"vertical\">");
                    sb.AppendLine("");
                    sb.AppendLine("</LinearLayout>");
                    break;
                case TipoLayout.LinearVerticalDialog:
                    sb.AppendLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    sb.AppendLine("    android:layout_width=\"wrap_content\"");
                    sb.AppendLine("    android:layout_height=\"wrap_content\"");
                    sb.AppendLine("    android:background=\"@drawable/dialog\"");
                    sb.AppendLine("    android:minWidth=\"350dp\"");
                    sb.AppendLine("    android:orientation=\"vertical\"");
                    sb.AppendLine("    android:padding=\"20dp\"");
                    sb.AppendLine("    android:weightSum=\"1\">");
                    sb.AppendLine("");
                    sb.AppendLine("</LinearLayout>");
                    break;
            }

            Arquivos.Gerar(sb.ToString(), pNome);
        }

        public static void GerarRecyclerView(string pProjeto, string pClasse, string pClasseObjeto, string pPkgObjeto)
        {
            string nomeClasse = pClasse;
            if (!pClasse.EndsWith("Adapter"))
                nomeClasse += "Adapter";

            string nomeHolder = nomeClasse.Replace("Adapter", "Holder");
            if (!nomeHolder.StartsWith("View"))
                nomeHolder = "View" + nomeHolder;
            string nomeLayout = "item_" + pClasse.ToLower().Replace("adapter", "");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("package " + pProjeto.ToLower() + ".adapters;");
            sb.AppendLine("");
            sb.AppendLine("import android.content.Context;");
            sb.AppendLine("import android.support.v7.widget.RecyclerView;");
            sb.AppendLine("import android.view.LayoutInflater;");
            sb.AppendLine("import android.view.View;");
            sb.AppendLine("import android.view.ViewGroup;");
            sb.AppendLine("");
            sb.AppendLine("import " + pProjeto.ToLower() + ".R;");
            sb.AppendLine("import " + pProjeto.ToLower() + "." + pPkgObjeto + "." + pClasseObjeto + ";");
            sb.AppendLine("");
            sb.AppendLine("import java.util.ArrayList;");
            sb.AppendLine("");
            sb.Append(Util.GetUser());
            sb.AppendLine("");
            sb.AppendLine("public class " + nomeClasse + " extends RecyclerView.Adapter<" + nomeClasse + "." + nomeHolder + "> {");
            sb.AppendLine("    private ArrayList<" + pClasseObjeto + "> mLista;");
            sb.AppendLine("    private Context mContext;");
            sb.AppendLine("");
            sb.AppendLine("    public " + nomeClasse + "(ArrayList<" + pClasseObjeto + "> pList, Context pContext) {");
            sb.AppendLine("        mLista = pList;");
            sb.AppendLine("        mContext = pContext;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public " + nomeHolder + " onCreateViewHolder(ViewGroup parent, int viewType) {");
            sb.AppendLine("        View itemView = LayoutInflater.from(mContext).inflate(R.layout." + nomeLayout + ", parent, false);");
            sb.AppendLine("        return new " + nomeHolder + "(itemView);");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public void onBindViewHolder(" + nomeHolder + " holder, int position) {");
            sb.AppendLine("        " + pClasseObjeto + " mItem = mLista.get(position);");
            sb.AppendLine("        //TODO Setar Valores");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public int getItemCount() {");
            sb.AppendLine("        return mLista.size();");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    private void removeAt(int position) {");
            sb.AppendLine("        mLista.remove(position);");
            sb.AppendLine("        notifyItemRemoved(position);");
            sb.AppendLine("        notifyItemRangeChanged(position, mLista.size());");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    protected class " + nomeHolder + " extends RecyclerView.ViewHolder {");
            sb.AppendLine("       //TODO Declarar Objetos da Tela");
            sb.AppendLine("");
            sb.AppendLine("        " + nomeHolder + "(View itemView) {");
            sb.AppendLine("            super(itemView);");
            sb.AppendLine("            //TODO instanciar da tela");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.Append("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), nomeClasse);
            GerarLayout(nomeLayout, TipoLayout.LinearVertical);
        }

        public static void GerarSetImage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("//TODO ImageDrawable");
            sb.AppendLine("if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP)");
            sb.AppendLine("    holder.imgOperadora.setImageDrawable(mContext.getResources().getDrawable(R.drawable.vivo, mContext.getTheme()));");
            sb.AppendLine("else");
            sb.AppendLine("    holder.imgOperadora.setImageDrawable(mContext.getResources().getDrawable(R.drawable.vivo));");
            sb.AppendLine();
            sb.AppendLine("imgStatus.setImageResource(R.mipmap.ic_icone_estoque);");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), "Exemplo Set");
        }

        /// <summary>
        /// Retorna Tabelas
        /// </summary>
        /// <param name="pBanco">Base de dados</param>
        /// <returns></returns>
        public static DataTable GetTabelas(string pBanco)
        {
            SqlLite dbSql = new SqlLite(pBanco);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT name AS Name");
            sb.AppendLine("FROM sqlite_master");
            sb.AppendLine("WHERE type = 'table'");
            sb.AppendLine("AND name != 'android_metadata'");
            sb.AppendLine("ORDER BY name");

            return dbSql.ExecuteDataTable(sb.ToString());
        }

        public static void GerarExpandableList(string pProjeto, string pClasse, string pClasseObjeto, string pPkgObjeto, string pClasseObjetoSecundario)
        {
            string nomeClasse = pClasse;
            if (!pClasse.EndsWith("Adapter"))
                nomeClasse += "Adapter";

            string layoutItensPai = "item_layout_pai";
            string layoutItensFilho = "item_layout_filho";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("package " + pProjeto.ToLower() + ".adapters;");
            sb.AppendLine("");
            sb.AppendLine("import android.app.Activity;");
            sb.AppendLine("import android.content.Context;");
            sb.AppendLine("import android.view.LayoutInflater;");
            sb.AppendLine("import android.view.View;");
            sb.AppendLine("import android.view.ViewGroup;");
            sb.AppendLine("import android.widget.BaseExpandableListAdapter;");
            sb.AppendLine("");
            sb.AppendLine("import " + pProjeto.ToLower() + ".R;");
            sb.AppendLine("import " + pProjeto.ToLower() + "." + pPkgObjeto + "." + pClasseObjeto + ";");
            sb.AppendLine("import " + pProjeto.ToLower() + "." + pPkgObjeto + "." + pClasseObjetoSecundario + ";");
            sb.AppendLine("");
            sb.AppendLine("import java.util.ArrayList;");
            sb.AppendLine("");
            sb.Append(Util.GetUser());
            sb.AppendLine("");
            sb.AppendLine("public class " + nomeClasse + " extends BaseExpandableListAdapter {");
            sb.AppendLine("    private Context mContext;");
            sb.AppendLine("    private ArrayList<" + pClasseObjeto + "> mLista;");
            sb.AppendLine("");
            sb.AppendLine("    public " + nomeClasse + "(ArrayList<" + pClasseObjeto + "> pLista, Context pContext) {");
            sb.AppendLine("        mLista = pLista;");
            sb.AppendLine("        mContext = pContext;");
            sb.AppendLine("    }");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public int getGroupCount() {");
            sb.AppendLine("        return mLista.size();");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public int getChildrenCount(int groupPosition) {");
            sb.AppendLine("        //TODO retornar quantidade filhos Exemplo: return mLista.get(groupPosition).getCodigosList().size();");
            sb.AppendLine("        return 0;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public Object getGroup(int groupPosition) {");
            sb.AppendLine("        return mLista.get(groupPosition);");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public Object getChild(int groupPosition, int childPosition) {");
            sb.AppendLine("        //TODO retornar o filho Exemplo: return mLista.get(groupPosition).getCodigosList().get(childPosition); ");
            sb.AppendLine("        return null;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public long getGroupId(int groupPosition) {");
            sb.AppendLine("        //TODO retornar o ID do pai Exemplo: return mLista.get(groupPosition).getId();");
            sb.AppendLine("        return 0;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public long getChildId(int groupPosition, int childPosition) {");
            sb.AppendLine("        //TODO retornar o ID do filho Exemplo: return mLista.get(groupPosition).getCodigosList().get(childPosition).getId();");
            sb.AppendLine("        return 0;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public boolean hasStableIds() {");
            sb.AppendLine("        return false;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public View getGroupView(int groupPosition, boolean isExpanded, View convertView, ViewGroup parent) {");
            sb.AppendLine("        //TODO set valores com layout pai");
            sb.AppendLine("        " + pClasseObjeto + " mObjeto = mLista.get(groupPosition);");
            sb.AppendLine("        if (convertView == null) {");
            sb.AppendLine("            LayoutInflater inflater = ((Activity) mContext).getLayoutInflater();");
            sb.AppendLine("            convertView = inflater.inflate(R.layout." + layoutItensPai + ", parent, false);");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        return convertView;");
            sb.AppendLine("    }    ");
            sb.AppendLine("	");
            sb.AppendLine("	@Override");
            sb.AppendLine("    public View getChildView(int groupPosition, int childPosition, boolean isLastChild, View convertView, ViewGroup parent) {");
            sb.AppendLine("        //TODO set valores com layout filho");
            sb.AppendLine("        " + pClasseObjetoSecundario + " mObjeto = mLista.get(groupPosition).getCodigosList().get(childPosition);");
            sb.AppendLine("        if (convertView == null)");
            sb.AppendLine("            convertView = View.inflate(mContext, R.layout." + layoutItensFilho + ", null);");
            sb.AppendLine("");
            sb.AppendLine("        return convertView;");
            sb.AppendLine("    }");
            sb.AppendLine("    ");
            sb.AppendLine("");
            sb.AppendLine("	@Override");
            sb.AppendLine("    public boolean isChildSelectable(int groupPosition, int childPosition) {");
            sb.AppendLine("        return false;");
            sb.AppendLine("    }");
            sb.Append("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), nomeClasse);
        }

        public static void GerarArrayAdapter(string pProjeto, string pClasse, string pClasseObjeto, string pPkgObjeto)
        {
            string nomeClasse = pClasse;
            if (!pClasse.EndsWith("Adapter"))
                nomeClasse += "Adapter";


            string layout = "item_" + pClasseObjeto.ToLower();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("package " + pProjeto.ToLower() + ".adapters;");
            sb.AppendLine("");
            sb.AppendLine("import android.app.Activity;");
            sb.AppendLine("import android.content.Context;");
            sb.AppendLine("import android.view.LayoutInflater;");
            sb.AppendLine("import android.view.View;");
            sb.AppendLine("import android.view.ViewGroup;");
            sb.AppendLine("import android.widget.ArrayAdapter;");
            sb.AppendLine("");
            sb.AppendLine("import " + pProjeto.ToLower() + ".R;");
            sb.AppendLine("import " + pProjeto.ToLower() + "." + pPkgObjeto + "." + pClasseObjeto + ";");
            sb.AppendLine("");
            sb.AppendLine("import java.util.ArrayList;");
            sb.AppendLine("");
            sb.Append(Util.GetUser());
            sb.AppendLine("");
            sb.AppendLine("public class " + nomeClasse + " extends ArrayAdapter<" + pClasseObjeto + "> {");
            sb.AppendLine("    private Context mContext;");
            sb.AppendLine("    private ArrayList<" + pClasseObjeto + "> mLista;");
            sb.AppendLine("");
            sb.AppendLine("    public " + nomeClasse + "(Context pContext, int resourceId, ArrayList<" + pClasseObjeto + "> pLista) {");
            sb.AppendLine("        super(pContext, resourceId, pLista);");
            sb.AppendLine("        mContext = pContext;");
            sb.AppendLine("        mLista = pLista;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    public View getView(int position, View convertView, ViewGroup parent) {");
            sb.AppendLine("        //TODO set valores");
            sb.AppendLine("        " + pClasseObjeto + " mObjeto = mLista.get(position);");
            sb.AppendLine("        if (convertView == null) {");
            sb.AppendLine("            LayoutInflater inflater = ((Activity) mContext).getLayoutInflater();");
            sb.AppendLine("            convertView = inflater.inflate(R.layout." + layout + ", parent, false);");
            sb.AppendLine("        }");
            sb.AppendLine("        ");
            sb.AppendLine("        return convertView;");
            sb.AppendLine("    }");
            sb.Append("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), nomeClasse);
        }

        public static void GerarClassDB(string pProjeto)
        {
            StringBuilder sbMult = new StringBuilder();
            sbMult.AppendLine("package " + pProjeto + ".db;");
            sbMult.AppendLine("");
            sbMult.AppendLine("import java.util.Map;");
            sbMult.AppendLine("import java.util.WeakHashMap;");
            sbMult.AppendLine("");
            sbMult.AppendLine("import android.content.Context;");
            sbMult.AppendLine("import android.database.sqlite.SQLiteDatabase;");
            sbMult.AppendLine("import android.database.sqlite.SQLiteDatabase.CursorFactory;");
            sbMult.AppendLine("import android.database.sqlite.SQLiteOpenHelper;");
            sbMult.AppendLine("");
            sbMult.AppendLine("/**");
            sbMult.AppendLine(" * MultiThreadSQLiteOpenHelper:<br>");
            sbMult.AppendLine(" * enhanced SQLiteOpenHelper for android applications where several threads might access and close the same database<br>");
            sbMult.AppendLine(" * <p>");
            sbMult.AppendLine(" * With SQLiteOpenHelper, if one thread is closing database, then other threads will crash while accessing a closed database.<br>");
            sbMult.AppendLine(" * With MultiThreadSQLiteOpenHelper, a thread does not close the database anymore, but asks for a close with the closeIfNeeded method.");
            sbMult.AppendLine(" * It then verifies that each thread asked for closing before really closing the database.");
            sbMult.AppendLine(" *");
            sbMult.AppendLine(" * @author d4rxh4wx");
            sbMult.AppendLine(" *");
            sbMult.AppendLine(" */");
            sbMult.AppendLine("public abstract class MultiThreadSQLiteOpenHelper extends SQLiteOpenHelper {");
            sbMult.AppendLine("");
            sbMult.AppendLine("    private final static String TAG = \"MULTI-THREAD-DB-HELPER\";");
            sbMult.AppendLine("");
            sbMult.AppendLine("    // Tells for each thread if it requiresMultiThreadSQLiteOpenHelper that database should be opened or not opened (closed)");
            sbMult.AppendLine("    // Using WeakHashMap so that thread can be released by GC when needed (no strong references on threads)");
            sbMult.AppendLine("    private WeakHashMap<Thread, Boolean> states = new WeakHashMap<Thread, Boolean>();");
            sbMult.AppendLine("");
            sbMult.AppendLine("    public MultiThreadSQLiteOpenHelper(Context context, String name,");
            sbMult.AppendLine("                                       CursorFactory factory, int version) {");
            sbMult.AppendLine("        super(context, name, factory, version);");
            sbMult.AppendLine("        Logger.INSTANCE.debug(TAG, \"database helper built\");");
            sbMult.AppendLine("    }");
            sbMult.AppendLine("");
            sbMult.AppendLine("    public SQLiteDatabase getWritableDatabase() {");
            sbMult.AppendLine("        // synchronized because it may be accessible by multi threads (all dbHelper methods are synchronized)");
            sbMult.AppendLine("        // and synchronized on the object because open/close are related on each other");
            sbMult.AppendLine("        synchronized(this) {");
            sbMult.AppendLine("            Thread currentThread = Thread.currentThread();");
            sbMult.AppendLine("");
            sbMult.AppendLine("            states.put(currentThread, true);  // this thread requires that this database should be opened");
            sbMult.AppendLine("");
            sbMult.AppendLine("            Logger.INSTANCE.debug(TAG, \"getting database\");");
            sbMult.AppendLine("");
            sbMult.AppendLine("            return super.getWritableDatabase();");
            sbMult.AppendLine("        }");
            sbMult.AppendLine("    }");
            sbMult.AppendLine("");
            sbMult.AppendLine("    /**");
            sbMult.AppendLine("     * Close database if all threads dont need the database anymore");
            sbMult.AppendLine("     * @return true if closed, false otherwise");
            sbMult.AppendLine("     */");
            sbMult.AppendLine("    public boolean closeIfNeeded() {");
            sbMult.AppendLine("        // synchronized because it may be accessible by multi threads (all dbHelper methods are synchronized)");
            sbMult.AppendLine("        // and synchronized on the object because open/close are related on each other");
            sbMult.AppendLine("        synchronized(this) {");
            sbMult.AppendLine("            Thread currentThread = Thread.currentThread();");
            sbMult.AppendLine("");
            sbMult.AppendLine("            Logger.INSTANCE.debug(TAG, \"requesting closing\");");
            sbMult.AppendLine("");
            sbMult.AppendLine("            states.put(currentThread, false); // this thread requires that this database should be closed");
            sbMult.AppendLine("");
            sbMult.AppendLine("            boolean mustBeClosed = true;");
            sbMult.AppendLine("");
            sbMult.AppendLine("            // if all threads asked for closing database, then close it");
            sbMult.AppendLine("            Boolean opened = null;");
            sbMult.AppendLine("            Thread thread = null;");
            sbMult.AppendLine("            for (Map.Entry<Thread, Boolean> entry : states.entrySet()) {");
            sbMult.AppendLine("                thread = entry.getKey();");
            sbMult.AppendLine("                opened = entry.getValue();");
            sbMult.AppendLine("                if (thread != null && opened != null) {");
            sbMult.AppendLine("                    Logger.INSTANCE.debug(TAG, String.format(\"Thread[% s] requires database must be % s\", thread.getId(), opened.booleanValue() ? \"OPENED\" : \"CLOSED\"));");
            sbMult.AppendLine("                    if (opened.booleanValue()) {");
            sbMult.AppendLine("                        // one thread still requires that database should be opened");
            sbMult.AppendLine("                        mustBeClosed = false;");
            sbMult.AppendLine("                    }");
            sbMult.AppendLine("                }");
            sbMult.AppendLine("            }");
            sbMult.AppendLine("");
            sbMult.AppendLine("            Logger.INSTANCE.debug(TAG, String.format(mustBeClosed ? \"database must be closed\" : \"database still needs to be opened\"));");
            sbMult.AppendLine("");
            sbMult.AppendLine("            if (mustBeClosed) {");
            sbMult.AppendLine("                super.close();");
            sbMult.AppendLine("                Logger.INSTANCE.debug(TAG, \"database is closed\");");
            sbMult.AppendLine("            }");
            sbMult.AppendLine("");
            sbMult.AppendLine("            return mustBeClosed;");
            sbMult.AppendLine("        }");
            sbMult.AppendLine("    }");
            sbMult.Append("}");

            StringBuilder sbDB = new StringBuilder();
            sbDB.AppendLine("package " + pProjeto + ".db; ");
            sbDB.AppendLine("");
            sbDB.AppendLine("import android.content.Context;");
            sbDB.AppendLine("import android.database.sqlite.SQLiteDatabase;");
            sbDB.AppendLine("");
            sbDB.Append(Util.GetUser());
            sbDB.AppendLine("");
            sbDB.AppendLine("public class SimpleDbHelper {");
            sbDB.AppendLine("    private final static String TAG = \"MULTI-THREAD-DB-HELPER\";");
            sbDB.AppendLine("    private MultiThreadSQLiteOpenHelper dbHelper;");
            sbDB.AppendLine("    public static final SimpleDbHelper INSTANCE = new SimpleDbHelper();");
            sbDB.AppendLine("");
            sbDB.AppendLine("    private SimpleDbHelper() {");
            sbDB.AppendLine("    }");
            sbDB.AppendLine("");
            sbDB.AppendLine("    public SQLiteDatabase open(Context context) {");
            sbDB.AppendLine("    synchronized (this) {");
            sbDB.AppendLine("        if (dbHelper == null) {");
            sbDB.AppendLine("            dbHelper = new MyMultiThreadSQLiteOpenHelper(context);");
            sbDB.AppendLine("        }");
            sbDB.AppendLine("        return dbHelper.getWritableDatabase(); // getting a cached database");
            sbDB.AppendLine("    }");
            sbDB.AppendLine("}");
            sbDB.AppendLine("");
            sbDB.AppendLine("    public void close() {");
            sbDB.AppendLine("        synchronized (this) {");
            sbDB.AppendLine("            if (dbHelper != null) {");
            sbDB.AppendLine("                // Ask for closing database");
            sbDB.AppendLine("                if (dbHelper.closeIfNeeded()) {");
            sbDB.AppendLine("                    dbHelper = null; // database closed: free resource for GC");
            sbDB.AppendLine("                }");
            sbDB.AppendLine("            }");
            sbDB.AppendLine("        }");
            sbDB.AppendLine("    }");
            sbDB.AppendLine("}");

            StringBuilder sbMyMulti = new StringBuilder();
            sbMyMulti.AppendLine("package " + pProjeto + ".db;");
            sbMyMulti.AppendLine("");
            sbMyMulti.AppendLine("import android.content.Context;");
            sbMyMulti.AppendLine("import android.database.sqlite.SQLiteDatabase;");
            sbMyMulti.AppendLine("");
            sbMyMulti.Append(Util.GetUser());
            sbMyMulti.AppendLine("");
            sbMyMulti.AppendLine("public class MyMultiThreadSQLiteOpenHelper extends MultiThreadSQLiteOpenHelper {");
            sbMyMulti.AppendLine("    private static final int DATABASE_VERSION = 1;");
            sbMyMulti.AppendLine("    private static final String DATABASE_NAME = \"Promo.db\";");
            sbMyMulti.AppendLine("    private String comando;");
            sbMyMulti.AppendLine("");
            sbMyMulti.AppendLine("    public MyMultiThreadSQLiteOpenHelper(Context context) {");
            sbMyMulti.AppendLine("        super(context, DATABASE_NAME, null, DATABASE_VERSION);");
            sbMyMulti.AppendLine("    }");
            sbMyMulti.AppendLine("");
            sbMyMulti.AppendLine("    public void onCreate(SQLiteDatabase db) {");
            sbMyMulti.AppendLine("        createBaseVersao(db);");
            sbMyMulti.AppendLine("        //updateBaseVersao2(db);");
            sbMyMulti.AppendLine("    }");
            sbMyMulti.AppendLine("");
            sbMyMulti.AppendLine("    @Override");
            sbMyMulti.AppendLine("    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {");
            sbMyMulti.AppendLine("//        switch (oldVersion) {");
            sbMyMulti.AppendLine("//            case 1:");
            sbMyMulti.AppendLine("//                updateBaseVersao2(db);");
            sbMyMulti.AppendLine("//                break;");
            sbMyMulti.AppendLine("//        }");
            sbMyMulti.AppendLine("    }");
            sbMyMulti.AppendLine("");
            sbMyMulti.AppendLine("    private void updateBaseVersao2(SQLiteDatabase db) {");
            sbMyMulti.AppendLine("//        comando = \"ALTER TABLE [Promocoes] ADD COLUMN [visualizado] INTEGER DEFAULT 0\";");
            sbMyMulti.AppendLine("//        try {");
            sbMyMulti.AppendLine("//            db.execSQL(comando);");
            sbMyMulti.AppendLine("//        } catch (Exception ex) {");
            sbMyMulti.AppendLine("//            ex.printStackTrace();");
            sbMyMulti.AppendLine("//        }");
            sbMyMulti.AppendLine("    }");
            sbMyMulti.AppendLine("");
            sbMyMulti.AppendLine("    private void createBaseVersao(SQLiteDatabase db) {");
            sbMyMulti.AppendLine("        comando = \"CREATE TABLE IF NOT EXISTS Promocoes(\n\" +");
            sbMyMulti.AppendLine("                \"[id] INTEGER NOT NULL PRIMARY KEY,\n\" +");
            sbMyMulti.AppendLine("                \"[url] VARCHAR(1000),\n\" +");
            sbMyMulti.AppendLine("                \"[urlShort] VARCHAR(300),\n\" +");
            sbMyMulti.AppendLine("                \"[data] DATETIME DEFAULT (datetime('now','localtime')),\n\" +");
            sbMyMulti.AppendLine("                \"[tipo] VARCHAR(15),\n\" +");
            sbMyMulti.AppendLine("                \"[visualizado] INTEGER DEFAULT 0,\n\" +");
            sbMyMulti.AppendLine("                \"[descricao] VARCHAR(800))\";");
            sbMyMulti.AppendLine("        try {");
            sbMyMulti.AppendLine("            db.execSQL(comando);");
            sbMyMulti.AppendLine("        } catch (Exception ex) {");
            sbMyMulti.AppendLine("            ex.printStackTrace();");
            sbMyMulti.AppendLine("        }");
            sbMyMulti.AppendLine("    }");
            sbMyMulti.AppendLine("}");

            StringBuilder sbUtil = new StringBuilder();
            sbUtil.AppendLine("public class Util_DB {");
            sbUtil.AppendLine("    public static boolean isCadastrado(Context pContext, String pTabela, String pCampo, String pValor) throws Exception {");
            sbUtil.AppendLine("        return isCadastrado(pContext, pTabela, new String[]{pCampo}, new String[]{pValor});");
            sbUtil.AppendLine("    }");
            sbUtil.AppendLine("");
            sbUtil.AppendLine("    public static boolean isCadastrado(Context pContext, String pTabela, String[] pCampos, String[] pValores) throws Exception {");
            sbUtil.AppendLine("        Util_IO.StringBuilder sbCondicao = new Util_IO.StringBuilder();");
            sbUtil.AppendLine("        for (int i = 0; i < pCampos.length; i++) {");
            sbUtil.AppendLine("            sbCondicao.append(String.format(\"%1s = ?\", pCampos[i]));");
            sbUtil.AppendLine("            if (i < pCampos.length - 1)");
            sbUtil.AppendLine("                sbCondicao.append(\" AND \");");
            sbUtil.AppendLine("        }");
            sbUtil.AppendLine("");
            sbUtil.AppendLine("        Cursor cursor = SimpleDbHelper.INSTANCE.open(pContext).query(pTabela, pCampos, sbCondicao.toString(), pValores, null, null, null, null);");
            sbUtil.AppendLine("        try {");
            sbUtil.AppendLine("            return (cursor != null && cursor.getCount() > 0);");
            sbUtil.AppendLine("        } finally {");
            sbUtil.AppendLine("            if (cursor != null)");
            sbUtil.AppendLine("                cursor.close();");
            sbUtil.AppendLine("        }");
            sbUtil.AppendLine("    }");
            sbUtil.AppendLine("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sbMult.ToString(), "MultiThreadSQLiteOpenHelper");
            Arquivos.Gerar(sbUtil.ToString(), "Util_DB");
            Arquivos.Gerar(sbDB.ToString(), "SimpleDbHelper");
            Arquivos.Gerar(sbMyMulti.ToString(), "MyMultiThreadSQLiteOpenHelper");
        }

        public static void GerarConexao()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SuppressWarnings(\"TryWithIdenticalCatches\")");
            sb.AppendLine("public static <T> T getObject(String pUrl, Class<T> pObjectClass) throws IOException {");
            sb.AppendLine("    try {");
            sb.AppendLine("        String resultString = getRegistros(pUrl);");
            sb.AppendLine("        if (resultString != null)");
            sb.AppendLine("            return new GsonBuilder().setDateFormat(Config.FormatDateTimeStringBancoJson).create().fromJson(resultString, pObjectClass);");
            sb.AppendLine("    } catch (IOException ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        throw ex;");
            sb.AppendLine("    } catch (Exception ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        throw ex;");
            sb.AppendLine("    }");
            sb.AppendLine("    return null;");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("public static String getRegistros(String pUrl) throws IOException {");
            sb.AppendLine("    return getRegistros(pUrl, true);");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("@SuppressWarnings(\"TryWithIdenticalCatches\")");
            sb.AppendLine("public static String getRegistros(String pUrl, boolean pNewLine) throws IOException {");
            sb.AppendLine("    URL url1 = new URL(pUrl);");
            sb.AppendLine("    String resultString = null;");
            sb.AppendLine("    try {");
            sb.AppendLine("        HttpURLConnection urlConnection = (HttpURLConnection) url1.openConnection();");
            sb.AppendLine("        try {");
            sb.AppendLine("            InputStream inputStream = new BufferedInputStream(urlConnection.getInputStream());");
            sb.AppendLine("            resultString = convertStreamToString(inputStream, pNewLine);");
            sb.AppendLine("            inputStream.close();");
            sb.AppendLine("        } catch (UnsupportedEncodingException ex) {");
            sb.AppendLine("            ex.printStackTrace();");
            sb.AppendLine("            resultString = null;");
            sb.AppendLine("        } catch (IOException ex) {");
            sb.AppendLine("            ex.printStackTrace();");
            sb.AppendLine("            resultString = null;");
            sb.AppendLine("        } finally {");
            sb.AppendLine("            urlConnection.disconnect();");
            sb.AppendLine("        }");
            sb.AppendLine("    } catch (SocketTimeoutException ex) {");
            sb.AppendLine("        resultString = null;");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("    } catch (IOException ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        resultString = null;");
            sb.AppendLine("    } catch (Exception ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        resultString = null;");
            sb.AppendLine("    }");
            sb.AppendLine("    return resultString;");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("@SuppressWarnings(\"TryWithIdenticalCatches\")");
            sb.AppendLine("public static String postRegistros(URL pUrl, String pJson) {");
            sb.AppendLine("    String resultString;");
            sb.AppendLine("    try {");
            sb.AppendLine("        HttpURLConnection conn = (HttpURLConnection) pUrl.openConnection();");
            sb.AppendLine("        conn.setRequestMethod(\"POST\");");
            sb.AppendLine("        conn.setDoInput(true);");
            sb.AppendLine("        conn.setDoOutput(true);");
            sb.AppendLine("        conn.setRequestProperty(\"Content-Type\", \"application/json; charset=UTF-8\");");
            sb.AppendLine("        conn.setRequestProperty(\"Accept\", \"application/json\");");
            sb.AppendLine("        conn.connect();");
            sb.AppendLine("");
            sb.AppendLine("        DataOutputStream wr = new DataOutputStream(conn.getOutputStream());");
            sb.AppendLine("        wr.writeBytes(pJson);");
            sb.AppendLine("        wr.flush();");
            sb.AppendLine("        wr.close();");
            sb.AppendLine("");
            sb.AppendLine("        BufferedReader in;");
            sb.AppendLine("        in = new BufferedReader(new InputStreamReader(conn.getInputStream(), \"UTF-8\"));");
            sb.AppendLine("");
            sb.AppendLine("        String inputLine;");
            sb.AppendLine("        StringBuffer response = new StringBuffer();");
            sb.AppendLine("        while ((inputLine = in.readLine()) != null) {");
            sb.AppendLine("            response.append(inputLine);");
            sb.AppendLine("        }");
            sb.AppendLine("        in.close();");
            sb.AppendLine("");
            sb.AppendLine("        resultString = response.toString();");
            sb.AppendLine("    } catch (UnsupportedEncodingException ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        resultString = null;");
            sb.AppendLine("    } catch (IOException ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        resultString = null;");
            sb.AppendLine("    } catch (Exception ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        resultString = null;");
            sb.AppendLine("    }");
            sb.AppendLine("    return resultString;");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("@SuppressWarnings(\"TryWithIdenticalCatches\")");
            sb.AppendLine("public static <T> T[] getArrayObject(String pUrl, Class<T[]> pArrayObjectClass) throws IOException {");
            sb.AppendLine("    try {");
            sb.AppendLine("        String resultString = getRegistros(pUrl);");
            sb.AppendLine("        if (resultString != null)");
            sb.AppendLine("            return new GsonBuilder().setDateFormat(Config.FormatDateTimeStringBancoJson).create().fromJson(resultString, pArrayObjectClass);");
            sb.AppendLine("    } catch (IOException ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        throw ex;");
            sb.AppendLine("    } catch (Exception ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("        throw ex;");
            sb.AppendLine("    }");
            sb.AppendLine("    return null;");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("@SuppressWarnings(\"TryWithIdenticalCatches\")");
            sb.AppendLine("public static String convertStreamToString(InputStream pInputStream, boolean pNewLine) {");
            sb.AppendLine("    StringBuilder stringBuilder = new StringBuilder();");
            sb.AppendLine("    try {");
            sb.AppendLine("        BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(pInputStream));");
            sb.AppendLine("        String line;");
            sb.AppendLine("        while ((line = bufferedReader.readLine()) != null) {");
            sb.AppendLine("            if (pNewLine)");
            sb.AppendLine("                stringBuilder.append(line).append(\"\n\");");
            sb.AppendLine("            else");
            sb.AppendLine("                stringBuilder.append(line);");
            sb.AppendLine("        }");
            sb.AppendLine("    } catch (OutOfMemoryError ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("    } catch (IOException ex) {");
            sb.AppendLine("        ex.printStackTrace();");
            sb.AppendLine("    } finally {");
            sb.AppendLine("        try {");
            sb.AppendLine("            pInputStream.close();");
            sb.AppendLine("        } catch (IOException ex) {");
            sb.AppendLine("            ex.printStackTrace();");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("    return stringBuilder.toString();");
            sb.AppendLine("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), "Conexao Android");
        }

        public static void GerarThread()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("new Thread(new Runnable() {");
            sb.AppendLine("    @Override");
            sb.AppendLine("    public void run() {");
            sb.AppendLine("        runOnUiThread(new Runnable() {");
            sb.AppendLine("            @Override");
            sb.AppendLine("            public void run() {");
            sb.AppendLine("            }");
            sb.AppendLine("        });");
            sb.AppendLine("    }");
            sb.AppendLine("}).start();");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), "Thread Android");
        }

        public static void GerarFireBase()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("public class FireBaseUtil {");
            sb.AppendLine("    private FirebaseDatabase mFirebaseInstance;");
            sb.AppendLine("    private DatabaseReference mDatabase;");
            sb.AppendLine("");
            sb.AppendLine("    public FireBaseUtil() {");
            sb.AppendLine("        mFirebaseInstance = FirebaseDatabase.getInstance();");
            sb.AppendLine("        getDb();");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    public DatabaseReference getDb() {");
            sb.AppendLine("        if (mDatabase == null) {");
            sb.AppendLine("            mFirebaseInstance.setPersistenceEnabled(true);");
            sb.AppendLine("            mDatabase = mFirebaseInstance.getReference();");
            sb.AppendLine("        }");
            sb.AppendLine("        return mDatabase;");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine("    public <T> T salvar(T pObjeto) {");
            sb.AppendLine("        String id = mDatabase.push().getKey();");
            sb.AppendLine("        mDatabase.child(\"Joao\").child(pObjeto.getClass().getSimpleName()).child(id).setValue(pObjeto);");
            sb.AppendLine("        return pObjeto;");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), "FireBase Android");
        }

        public static void GerarDialog(string pProjeto, string pClasse)
        {
            string nomeClasse = pClasse;
            if (!pClasse.EndsWith("Dialog"))
                nomeClasse += "Dialog";

            string nomeLayout = "item_" + pClasse.ToLower().Replace("dialog", "");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"package {pProjeto.ToLower()}.dialog;");
            sb.AppendLine("");
            sb.AppendLine("import android.os.Bundle;");
            sb.AppendLine("import android.support.v4.app.DialogFragment;");
            sb.AppendLine("import android.view.LayoutInflater;");
            sb.AppendLine("import android.view.View;");
            sb.AppendLine("import android.view.ViewGroup;");
            sb.AppendLine("import android.view.Window;");
            sb.AppendLine("");
            sb.AppendLine($"import {pProjeto.ToLower()}.R;");
            sb.AppendLine("");
            sb.Append(Util.GetUser());
            sb.AppendLine("");
            sb.AppendLine("public class " + nomeClasse + " extends DialogFragment {");
            sb.AppendLine("");
            sb.AppendLine("    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {");
            sb.AppendLine("        getDialog().requestWindowFeature(Window.FEATURE_NO_TITLE);");
            sb.AppendLine("        View view = inflater.inflate(R.layout." + nomeLayout + ", container);");
            sb.AppendLine("");
            sb.AppendLine("        try {");
            sb.AppendLine("            setCancelable(false);");
            sb.AppendLine("            criarObjetos(view);");
            sb.AppendLine("            criarEventos();");
            sb.AppendLine("        } catch (Exception ex) {");
            sb.AppendLine("            //TODO Error");
            sb.AppendLine("        }");
            sb.AppendLine("        return view;");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    private void criarEventos() {");
            sb.AppendLine();
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    private void criarObjetos(View view) {");
            sb.AppendLine();
            sb.AppendLine("    }");
            sb.Append("}");

            Arquivos.Deletar();
            Arquivos.Gerar(sb.ToString(), nomeClasse);
            GerarLayout(nomeLayout, TipoLayout.LinearVerticalDialog);
        }
    }
}