﻿namespace Utilidades
{
    partial class FormBanco
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBanco));
			this.plQuery = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this.txtNomeClasse = new System.Windows.Forms.TextBox();
			this.txtQuery = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBanco = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboTabela = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.rbBanco = new System.Windows.Forms.RadioButton();
			this.rbQuery = new System.Windows.Forms.RadioButton();
			this.btnGerarModel = new System.Windows.Forms.Button();
			this.txtPacote = new System.Windows.Forms.TextBox();
			this.lbPacote = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comboDB = new System.Windows.Forms.ComboBox();
			this.comboLinguagem = new System.Windows.Forms.ComboBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtUsuario = new System.Windows.Forms.TextBox();
			this.txtSenha = new System.Windows.Forms.TextBox();
			this.txtServidor = new System.Windows.Forms.TextBox();
			this.comboTipoLogon = new System.Windows.Forms.ComboBox();
			this.btnDataBases = new System.Windows.Forms.Button();
			this.btnGerarRepository = new System.Windows.Forms.Button();
			this.plQuery.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// plQuery
			// 
			this.plQuery.Controls.Add(this.label8);
			this.plQuery.Controls.Add(this.txtNomeClasse);
			this.plQuery.Controls.Add(this.txtQuery);
			this.plQuery.Controls.Add(this.label7);
			this.plQuery.Location = new System.Drawing.Point(516, 13);
			this.plQuery.Name = "plQuery";
			this.plQuery.Size = new System.Drawing.Size(595, 472);
			this.plQuery.TabIndex = 37;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(3, 445);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(93, 16);
			this.label8.TabIndex = 39;
			this.label8.Text = "Nome Classe:";
			// 
			// txtNomeClasse
			// 
			this.txtNomeClasse.Enabled = false;
			this.txtNomeClasse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNomeClasse.Location = new System.Drawing.Point(137, 442);
			this.txtNomeClasse.Name = "txtNomeClasse";
			this.txtNomeClasse.Size = new System.Drawing.Size(364, 22);
			this.txtNomeClasse.TabIndex = 38;
			// 
			// txtQuery
			// 
			this.txtQuery.Enabled = false;
			this.txtQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQuery.Location = new System.Drawing.Point(6, 25);
			this.txtQuery.Multiline = true;
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.Size = new System.Drawing.Size(576, 411);
			this.txtQuery.TabIndex = 39;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(3, 4);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(44, 16);
			this.label7.TabIndex = 38;
			this.label7.Text = "Query";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(12, 184);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 16);
			this.label4.TabIndex = 19;
			this.label4.Text = "Servidor:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 212);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 22;
			this.label1.Text = "DataBase:";
			// 
			// comboBanco
			// 
			this.comboBanco.DisplayMember = "Name";
			this.comboBanco.Enabled = false;
			this.comboBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboBanco.FormattingEnabled = true;
			this.comboBanco.Location = new System.Drawing.Point(146, 209);
			this.comboBanco.Name = "comboBanco";
			this.comboBanco.Size = new System.Drawing.Size(364, 24);
			this.comboBanco.TabIndex = 21;
			this.comboBanco.ValueMember = "Name";
			this.comboBanco.SelectedIndexChanged += new System.EventHandler(this.comboBanco_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 242);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 16);
			this.label2.TabIndex = 24;
			this.label2.Text = "Tabela:";
			// 
			// comboTabela
			// 
			this.comboTabela.DisplayMember = "Name";
			this.comboTabela.Enabled = false;
			this.comboTabela.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboTabela.FormattingEnabled = true;
			this.comboTabela.Location = new System.Drawing.Point(146, 239);
			this.comboTabela.Name = "comboTabela";
			this.comboTabela.Size = new System.Drawing.Size(364, 24);
			this.comboTabela.TabIndex = 23;
			this.comboTabela.ValueMember = "Name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 16);
			this.label3.TabIndex = 25;
			this.label3.Text = "Linguagem:";
			// 
			// rbBanco
			// 
			this.rbBanco.AutoSize = true;
			this.rbBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbBanco.Location = new System.Drawing.Point(146, 9);
			this.rbBanco.Name = "rbBanco";
			this.rbBanco.Size = new System.Drawing.Size(65, 20);
			this.rbBanco.TabIndex = 26;
			this.rbBanco.Text = "Banco";
			this.rbBanco.UseVisualStyleBackColor = true;
			this.rbBanco.CheckedChanged += new System.EventHandler(this.rbBanco_CheckedChanged);
			// 
			// rbQuery
			// 
			this.rbQuery.AutoSize = true;
			this.rbQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbQuery.Location = new System.Drawing.Point(319, 9);
			this.rbQuery.Name = "rbQuery";
			this.rbQuery.Size = new System.Drawing.Size(62, 20);
			this.rbQuery.TabIndex = 27;
			this.rbQuery.Text = "Query";
			this.rbQuery.UseVisualStyleBackColor = true;
			this.rbQuery.CheckedChanged += new System.EventHandler(this.rbQuery_CheckedChanged);
			// 
			// btnGerarModel
			// 
			this.btnGerarModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGerarModel.Location = new System.Drawing.Point(12, 346);
			this.btnGerarModel.Name = "btnGerarModel";
			this.btnGerarModel.Size = new System.Drawing.Size(146, 29);
			this.btnGerarModel.TabIndex = 29;
			this.btnGerarModel.Text = "Gerar";
			this.btnGerarModel.UseVisualStyleBackColor = true;
			this.btnGerarModel.Click += new System.EventHandler(this.btnGerarModel_Click);
			// 
			// txtPacote
			// 
			this.txtPacote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPacote.Location = new System.Drawing.Point(146, 269);
			this.txtPacote.Name = "txtPacote";
			this.txtPacote.Size = new System.Drawing.Size(364, 22);
			this.txtPacote.TabIndex = 30;
			// 
			// lbPacote
			// 
			this.lbPacote.AutoSize = true;
			this.lbPacote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbPacote.Location = new System.Drawing.Point(12, 272);
			this.lbPacote.Name = "lbPacote";
			this.lbPacote.Size = new System.Drawing.Size(86, 16);
			this.lbPacote.TabIndex = 31;
			this.lbPacote.Text = "Namespace:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(12, 13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(114, 16);
			this.label5.TabIndex = 32;
			this.label5.Text = "Tipo da Geração:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(12, 38);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 16);
			this.label6.TabIndex = 33;
			this.label6.Text = "Tipo DB:";
			// 
			// comboDB
			// 
			this.comboDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboDB.FormattingEnabled = true;
			this.comboDB.Items.AddRange(new object[] {
            "Sql Server",
            "My Sql"});
			this.comboDB.Location = new System.Drawing.Point(146, 35);
			this.comboDB.Name = "comboDB";
			this.comboDB.Size = new System.Drawing.Size(364, 24);
			this.comboDB.TabIndex = 34;
			this.comboDB.SelectedIndexChanged += new System.EventHandler(this.comboDB_SelectedIndexChanged);
			// 
			// comboLinguagem
			// 
			this.comboLinguagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboLinguagem.FormattingEnabled = true;
			this.comboLinguagem.Items.AddRange(new object[] {
            "C#",
            "Java"});
			this.comboLinguagem.Location = new System.Drawing.Point(146, 65);
			this.comboLinguagem.Name = "comboLinguagem";
			this.comboLinguagem.Size = new System.Drawing.Size(364, 24);
			this.comboLinguagem.TabIndex = 35;
			this.comboLinguagem.SelectedIndexChanged += new System.EventHandler(this.comboLinguagem_SelectedIndexChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(2, 297);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(508, 192);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 36;
			this.pictureBox1.TabStop = false;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(12, 128);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(58, 16);
			this.label9.TabIndex = 39;
			this.label9.Text = "Usuario:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(12, 158);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(50, 16);
			this.label10.TabIndex = 41;
			this.label10.Text = "Senha:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(12, 98);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(80, 16);
			this.label11.TabIndex = 42;
			this.label11.Text = "Tipo Logon:";
			// 
			// txtUsuario
			// 
			this.txtUsuario.Enabled = false;
			this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUsuario.Location = new System.Drawing.Point(146, 125);
			this.txtUsuario.Name = "txtUsuario";
			this.txtUsuario.Size = new System.Drawing.Size(364, 22);
			this.txtUsuario.TabIndex = 45;
			// 
			// txtSenha
			// 
			this.txtSenha.Enabled = false;
			this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSenha.Location = new System.Drawing.Point(146, 153);
			this.txtSenha.Name = "txtSenha";
			this.txtSenha.PasswordChar = '*';
			this.txtSenha.Size = new System.Drawing.Size(364, 22);
			this.txtSenha.TabIndex = 46;
			// 
			// txtServidor
			// 
			this.txtServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtServidor.Location = new System.Drawing.Point(146, 181);
			this.txtServidor.Name = "txtServidor";
			this.txtServidor.Size = new System.Drawing.Size(364, 22);
			this.txtServidor.TabIndex = 47;
			// 
			// comboTipoLogon
			// 
			this.comboTipoLogon.DisplayMember = "Name";
			this.comboTipoLogon.Enabled = false;
			this.comboTipoLogon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboTipoLogon.FormattingEnabled = true;
			this.comboTipoLogon.Items.AddRange(new object[] {
            "Windows Authentication",
            "User/Senha"});
			this.comboTipoLogon.Location = new System.Drawing.Point(146, 95);
			this.comboTipoLogon.Name = "comboTipoLogon";
			this.comboTipoLogon.Size = new System.Drawing.Size(364, 24);
			this.comboTipoLogon.TabIndex = 48;
			this.comboTipoLogon.ValueMember = "Name";
			this.comboTipoLogon.SelectedIndexChanged += new System.EventHandler(this.comboTipoLogon_SelectedIndexChanged);
			// 
			// btnDataBases
			// 
			this.btnDataBases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDataBases.Location = new System.Drawing.Point(12, 311);
			this.btnDataBases.Name = "btnDataBases";
			this.btnDataBases.Size = new System.Drawing.Size(146, 29);
			this.btnDataBases.TabIndex = 49;
			this.btnDataBases.Text = "Buscar DataBases";
			this.btnDataBases.UseVisualStyleBackColor = true;
			this.btnDataBases.Click += new System.EventHandler(this.btnDataBases_Click);
			// 
			// btnGerarRepository
			// 
			this.btnGerarRepository.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGerarRepository.Location = new System.Drawing.Point(12, 381);
			this.btnGerarRepository.Name = "btnGerarRepository";
			this.btnGerarRepository.Size = new System.Drawing.Size(146, 29);
			this.btnGerarRepository.TabIndex = 50;
			this.btnGerarRepository.Text = "Gerar Repository";
			this.btnGerarRepository.UseVisualStyleBackColor = true;
			this.btnGerarRepository.Click += new System.EventHandler(this.btnGerarRepository_Click);
			// 
			// FormBanco
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1121, 522);
			this.Controls.Add(this.btnGerarRepository);
			this.Controls.Add(this.btnDataBases);
			this.Controls.Add(this.comboTipoLogon);
			this.Controls.Add(this.txtServidor);
			this.Controls.Add(this.txtSenha);
			this.Controls.Add(this.txtUsuario);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.plQuery);
			this.Controls.Add(this.comboLinguagem);
			this.Controls.Add(this.comboDB);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lbPacote);
			this.Controls.Add(this.txtPacote);
			this.Controls.Add(this.btnGerarModel);
			this.Controls.Add(this.rbQuery);
			this.Controls.Add(this.rbBanco);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboTabela);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBanco);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormBanco";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Gerador Model";
			this.plQuery.ResumeLayout(false);
			this.plQuery.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBanco;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboTabela;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbBanco;
        private System.Windows.Forms.RadioButton rbQuery;
        private System.Windows.Forms.Button btnGerarModel;
        private System.Windows.Forms.TextBox txtPacote;
        private System.Windows.Forms.Label lbPacote;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboDB;
        private System.Windows.Forms.ComboBox comboLinguagem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNomeClasse;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Panel plQuery;
        private System.Windows.Forms.ComboBox comboTipoLogon;
        private System.Windows.Forms.Button btnDataBases;
		private System.Windows.Forms.Button btnGerarRepository;
	}
}