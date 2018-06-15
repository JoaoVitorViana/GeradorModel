namespace Utilidades
{
    partial class FormAndroid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAndroid));
            this.btnGerarRecyclerView = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjeto = new System.Windows.Forms.TextBox();
            this.txtClasse = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPkgObjeto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPkgObjeto = new System.Windows.Forms.CheckBox();
            this.btnSetImagem = new System.Windows.Forms.Button();
            this.btnGerarDB = new System.Windows.Forms.Button();
            this.comboTabelas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBanco = new System.Windows.Forms.Button();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnGerarFragment = new System.Windows.Forms.Button();
            this.btnGerarExpandableList = new System.Windows.Forms.Button();
            this.txtObjSecundario = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGerarArrayAdapter = new System.Windows.Forms.Button();
            this.btnGerarClasseDB = new System.Windows.Forms.Button();
            this.cbVariavelTabela = new System.Windows.Forms.CheckBox();
            this.txtVariavelTabela = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnUtil = new System.Windows.Forms.Button();
            this.btnThread = new System.Windows.Forms.Button();
            this.btnFireBase = new System.Windows.Forms.Button();
            this.txtClasseObjeto = new System.Windows.Forms.TextBox();
            this.btnDialog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGerarRecyclerView
            // 
            this.btnGerarRecyclerView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarRecyclerView.Location = new System.Drawing.Point(251, 290);
            this.btnGerarRecyclerView.Name = "btnGerarRecyclerView";
            this.btnGerarRecyclerView.Size = new System.Drawing.Size(182, 29);
            this.btnGerarRecyclerView.TabIndex = 1;
            this.btnGerarRecyclerView.Text = "Gerar RecyclerView";
            this.btnGerarRecyclerView.UseVisualStyleBackColor = true;
            this.btnGerarRecyclerView.Click += new System.EventHandler(this.btnGerarRecyclerView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(190, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Projeto:";
            // 
            // txtProjeto
            // 
            this.txtProjeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjeto.Location = new System.Drawing.Point(316, 12);
            this.txtProjeto.Name = "txtProjeto";
            this.txtProjeto.Size = new System.Drawing.Size(287, 22);
            this.txtProjeto.TabIndex = 3;
            // 
            // txtClasse
            // 
            this.txtClasse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClasse.Location = new System.Drawing.Point(316, 40);
            this.txtClasse.Name = "txtClasse";
            this.txtClasse.Size = new System.Drawing.Size(287, 22);
            this.txtClasse.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Classe:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(190, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Objeto Principal:";
            // 
            // txtPkgObjeto
            // 
            this.txtPkgObjeto.Enabled = false;
            this.txtPkgObjeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPkgObjeto.Location = new System.Drawing.Point(316, 68);
            this.txtPkgObjeto.Name = "txtPkgObjeto";
            this.txtPkgObjeto.Size = new System.Drawing.Size(287, 22);
            this.txtPkgObjeto.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(190, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Package Objeto:";
            // 
            // cbPkgObjeto
            // 
            this.cbPkgObjeto.AutoSize = true;
            this.cbPkgObjeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPkgObjeto.Location = new System.Drawing.Point(609, 71);
            this.cbPkgObjeto.Name = "cbPkgObjeto";
            this.cbPkgObjeto.Size = new System.Drawing.Size(77, 20);
            this.cbPkgObjeto.TabIndex = 13;
            this.cbPkgObjeto.Text = "Habilitar";
            this.cbPkgObjeto.UseVisualStyleBackColor = true;
            this.cbPkgObjeto.CheckedChanged += new System.EventHandler(this.cbPkgObjeto_CheckedChanged);
            // 
            // btnSetImagem
            // 
            this.btnSetImagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetImagem.Location = new System.Drawing.Point(627, 255);
            this.btnSetImagem.Name = "btnSetImagem";
            this.btnSetImagem.Size = new System.Drawing.Size(182, 29);
            this.btnSetImagem.TabIndex = 14;
            this.btnSetImagem.Text = "Set Image";
            this.btnSetImagem.UseVisualStyleBackColor = true;
            this.btnSetImagem.Click += new System.EventHandler(this.btnSetImagem_Click);
            // 
            // btnGerarDB
            // 
            this.btnGerarDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarDB.Location = new System.Drawing.Point(63, 255);
            this.btnGerarDB.Name = "btnGerarDB";
            this.btnGerarDB.Size = new System.Drawing.Size(182, 29);
            this.btnGerarDB.TabIndex = 15;
            this.btnGerarDB.Text = "Gerar DB";
            this.btnGerarDB.UseVisualStyleBackColor = true;
            this.btnGerarDB.Click += new System.EventHandler(this.btnGerarDB_Click);
            // 
            // comboTabelas
            // 
            this.comboTabelas.DisplayMember = "Name";
            this.comboTabelas.Enabled = false;
            this.comboTabelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTabelas.FormattingEnabled = true;
            this.comboTabelas.Location = new System.Drawing.Point(316, 153);
            this.comboTabelas.Name = "comboTabelas";
            this.comboTabelas.Size = new System.Drawing.Size(287, 24);
            this.comboTabelas.TabIndex = 16;
            this.comboTabelas.ValueMember = "Name";
            this.comboTabelas.SelectedIndexChanged += new System.EventHandler(this.comboTabelas_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(190, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Tabela:";
            // 
            // btnBanco
            // 
            this.btnBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBanco.Location = new System.Drawing.Point(746, 122);
            this.btnBanco.Name = "btnBanco";
            this.btnBanco.Size = new System.Drawing.Size(127, 29);
            this.btnBanco.TabIndex = 18;
            this.btnBanco.Text = "Procurar";
            this.btnBanco.UseVisualStyleBackColor = true;
            this.btnBanco.Click += new System.EventHandler(this.btnBanco_Click);
            // 
            // txtBanco
            // 
            this.txtBanco.Enabled = false;
            this.txtBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBanco.Location = new System.Drawing.Point(316, 125);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(424, 22);
            this.txtBanco.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(190, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Banco:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 23);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(172, 153);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // btnGerarFragment
            // 
            this.btnGerarFragment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarFragment.Location = new System.Drawing.Point(439, 255);
            this.btnGerarFragment.Name = "btnGerarFragment";
            this.btnGerarFragment.Size = new System.Drawing.Size(182, 29);
            this.btnGerarFragment.TabIndex = 22;
            this.btnGerarFragment.Text = "Gerar Fragment";
            this.btnGerarFragment.UseVisualStyleBackColor = true;
            this.btnGerarFragment.Click += new System.EventHandler(this.btnGerarFragment_Click);
            // 
            // btnGerarExpandableList
            // 
            this.btnGerarExpandableList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarExpandableList.Location = new System.Drawing.Point(251, 255);
            this.btnGerarExpandableList.Name = "btnGerarExpandableList";
            this.btnGerarExpandableList.Size = new System.Drawing.Size(182, 29);
            this.btnGerarExpandableList.TabIndex = 23;
            this.btnGerarExpandableList.Text = "Gerar Expandable List";
            this.btnGerarExpandableList.UseVisualStyleBackColor = true;
            this.btnGerarExpandableList.Click += new System.EventHandler(this.btnGerarExpandableList_Click);
            // 
            // txtObjSecundario
            // 
            this.txtObjSecundario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjSecundario.Location = new System.Drawing.Point(316, 185);
            this.txtObjSecundario.Name = "txtObjSecundario";
            this.txtObjSecundario.Size = new System.Drawing.Size(287, 22);
            this.txtObjSecundario.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(190, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 16);
            this.label7.TabIndex = 24;
            this.label7.Text = "Objeto Secundario:";
            // 
            // btnGerarArrayAdapter
            // 
            this.btnGerarArrayAdapter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarArrayAdapter.Location = new System.Drawing.Point(63, 290);
            this.btnGerarArrayAdapter.Name = "btnGerarArrayAdapter";
            this.btnGerarArrayAdapter.Size = new System.Drawing.Size(182, 29);
            this.btnGerarArrayAdapter.TabIndex = 26;
            this.btnGerarArrayAdapter.Text = "Gerar ArrayAdapter";
            this.btnGerarArrayAdapter.UseVisualStyleBackColor = true;
            this.btnGerarArrayAdapter.Click += new System.EventHandler(this.btnGerarArrayAdapter_Click);
            // 
            // btnGerarClasseDB
            // 
            this.btnGerarClasseDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarClasseDB.Location = new System.Drawing.Point(439, 290);
            this.btnGerarClasseDB.Name = "btnGerarClasseDB";
            this.btnGerarClasseDB.Size = new System.Drawing.Size(182, 29);
            this.btnGerarClasseDB.TabIndex = 27;
            this.btnGerarClasseDB.Text = "Conexão DB";
            this.btnGerarClasseDB.UseVisualStyleBackColor = true;
            this.btnGerarClasseDB.Click += new System.EventHandler(this.btnGerarClasseDB_Click);
            // 
            // cbVariavelTabela
            // 
            this.cbVariavelTabela.AutoSize = true;
            this.cbVariavelTabela.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVariavelTabela.Location = new System.Drawing.Point(609, 217);
            this.cbVariavelTabela.Name = "cbVariavelTabela";
            this.cbVariavelTabela.Size = new System.Drawing.Size(77, 20);
            this.cbVariavelTabela.TabIndex = 30;
            this.cbVariavelTabela.Text = "Habilitar";
            this.cbVariavelTabela.UseVisualStyleBackColor = true;
            this.cbVariavelTabela.CheckedChanged += new System.EventHandler(this.cbVariavelTabela_CheckedChanged);
            // 
            // txtVariavelTabela
            // 
            this.txtVariavelTabela.Enabled = false;
            this.txtVariavelTabela.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVariavelTabela.Location = new System.Drawing.Point(316, 214);
            this.txtVariavelTabela.Name = "txtVariavelTabela";
            this.txtVariavelTabela.Size = new System.Drawing.Size(287, 22);
            this.txtVariavelTabela.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(190, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 16);
            this.label8.TabIndex = 28;
            this.label8.Text = "Variável Tabela:";
            // 
            // btnUtil
            // 
            this.btnUtil.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUtil.Location = new System.Drawing.Point(627, 290);
            this.btnUtil.Name = "btnUtil";
            this.btnUtil.Size = new System.Drawing.Size(182, 29);
            this.btnUtil.TabIndex = 31;
            this.btnUtil.Text = "Conexão WS";
            this.btnUtil.UseVisualStyleBackColor = true;
            this.btnUtil.Click += new System.EventHandler(this.btnUtil_Click);
            // 
            // btnThread
            // 
            this.btnThread.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThread.Location = new System.Drawing.Point(63, 325);
            this.btnThread.Name = "btnThread";
            this.btnThread.Size = new System.Drawing.Size(182, 29);
            this.btnThread.TabIndex = 32;
            this.btnThread.Text = "Thread";
            this.btnThread.UseVisualStyleBackColor = true;
            this.btnThread.Click += new System.EventHandler(this.btnThread_Click);
            // 
            // btnFireBase
            // 
            this.btnFireBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFireBase.Location = new System.Drawing.Point(251, 325);
            this.btnFireBase.Name = "btnFireBase";
            this.btnFireBase.Size = new System.Drawing.Size(182, 29);
            this.btnFireBase.TabIndex = 33;
            this.btnFireBase.Text = "FireBase";
            this.btnFireBase.UseVisualStyleBackColor = true;
            this.btnFireBase.Click += new System.EventHandler(this.btnFireBase_Click);
            // 
            // txtClasseObjeto
            // 
            this.txtClasseObjeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClasseObjeto.Location = new System.Drawing.Point(316, 97);
            this.txtClasseObjeto.Name = "txtClasseObjeto";
            this.txtClasseObjeto.Size = new System.Drawing.Size(287, 22);
            this.txtClasseObjeto.TabIndex = 7;
            // 
            // btnDialog
            // 
            this.btnDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDialog.Location = new System.Drawing.Point(439, 325);
            this.btnDialog.Name = "btnDialog";
            this.btnDialog.Size = new System.Drawing.Size(182, 29);
            this.btnDialog.TabIndex = 34;
            this.btnDialog.Text = "Gerar Dialog";
            this.btnDialog.UseVisualStyleBackColor = true;
            this.btnDialog.Click += new System.EventHandler(this.btnDialog_Click);
            // 
            // FormAndroid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 372);
            this.Controls.Add(this.btnDialog);
            this.Controls.Add(this.btnFireBase);
            this.Controls.Add(this.btnThread);
            this.Controls.Add(this.btnUtil);
            this.Controls.Add(this.cbVariavelTabela);
            this.Controls.Add(this.txtVariavelTabela);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnGerarClasseDB);
            this.Controls.Add(this.btnGerarArrayAdapter);
            this.Controls.Add(this.txtObjSecundario);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGerarExpandableList);
            this.Controls.Add(this.btnGerarFragment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBanco);
            this.Controls.Add(this.btnBanco);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboTabelas);
            this.Controls.Add(this.btnGerarDB);
            this.Controls.Add(this.btnSetImagem);
            this.Controls.Add(this.cbPkgObjeto);
            this.Controls.Add(this.txtPkgObjeto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtClasseObjeto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtClasse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProjeto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGerarRecyclerView);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAndroid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerador Android";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGerarRecyclerView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjeto;
        private System.Windows.Forms.TextBox txtClasse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPkgObjeto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbPkgObjeto;
        private System.Windows.Forms.Button btnSetImagem;
        private System.Windows.Forms.Button btnGerarDB;
        private System.Windows.Forms.ComboBox comboTabelas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBanco;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnGerarFragment;
        private System.Windows.Forms.Button btnGerarExpandableList;
        private System.Windows.Forms.TextBox txtObjSecundario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGerarArrayAdapter;
        private System.Windows.Forms.Button btnGerarClasseDB;
        private System.Windows.Forms.CheckBox cbVariavelTabela;
        private System.Windows.Forms.TextBox txtVariavelTabela;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnUtil;
        private System.Windows.Forms.Button btnThread;
        private System.Windows.Forms.Button btnFireBase;
        private System.Windows.Forms.TextBox txtClasseObjeto;
        private System.Windows.Forms.Button btnDialog;
    }
}