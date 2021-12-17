namespace Gerador
{
    partial class FormGerar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGerar));
            this.comboDB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboTipoLogon = new System.Windows.Forms.ComboBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbPacote = new System.Windows.Forms.Label();
            this.txtPacote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboTabela = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBanco = new System.Windows.Forms.ComboBox();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDataBases = new System.Windows.Forms.Button();
            this.btnGerar = new System.Windows.Forms.Button();
            this.comboLinguagem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDataAnnotations = new System.Windows.Forms.CheckBox();
            this.cbRepositorio = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboDB
            // 
            this.comboDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboDB.FormattingEnabled = true;
            this.comboDB.Items.AddRange(new object[] {
            "Sql Server",
            "My Sql"});
            this.comboDB.Location = new System.Drawing.Point(149, 19);
            this.comboDB.Name = "comboDB";
            this.comboDB.Size = new System.Drawing.Size(364, 24);
            this.comboDB.TabIndex = 36;
            this.comboDB.SelectedIndexChanged += new System.EventHandler(this.comboDB_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Banco de Dados:";
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
            this.comboTipoLogon.Location = new System.Drawing.Point(149, 76);
            this.comboTipoLogon.Name = "comboTipoLogon";
            this.comboTipoLogon.Size = new System.Drawing.Size(364, 24);
            this.comboTipoLogon.TabIndex = 60;
            this.comboTipoLogon.ValueMember = "Name";
            this.comboTipoLogon.SelectedIndexChanged += new System.EventHandler(this.comboTipoLogon_SelectedIndexChanged);
            // 
            // txtSenha
            // 
            this.txtSenha.Enabled = false;
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(149, 132);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(364, 22);
            this.txtSenha.TabIndex = 59;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(149, 104);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(364, 22);
            this.txtUsuario.TabIndex = 58;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 16);
            this.label11.TabIndex = 57;
            this.label11.Text = "Tipo Logon:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 16);
            this.label10.TabIndex = 56;
            this.label10.Text = "Senha:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 16);
            this.label9.TabIndex = 55;
            this.label9.Text = "Usuario:";
            // 
            // lbPacote
            // 
            this.lbPacote.AutoSize = true;
            this.lbPacote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPacote.Location = new System.Drawing.Point(12, 267);
            this.lbPacote.Name = "lbPacote";
            this.lbPacote.Size = new System.Drawing.Size(85, 16);
            this.lbPacote.TabIndex = 54;
            this.lbPacote.Text = "Namespace:";
            // 
            // txtPacote
            // 
            this.txtPacote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacote.Location = new System.Drawing.Point(149, 264);
            this.txtPacote.Name = "txtPacote";
            this.txtPacote.Size = new System.Drawing.Size(364, 22);
            this.txtPacote.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 52;
            this.label2.Text = "Tabela:";
            // 
            // comboTabela
            // 
            this.comboTabela.DisplayMember = "Name";
            this.comboTabela.Enabled = false;
            this.comboTabela.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTabela.FormattingEnabled = true;
            this.comboTabela.Location = new System.Drawing.Point(149, 234);
            this.comboTabela.Name = "comboTabela";
            this.comboTabela.Size = new System.Drawing.Size(364, 24);
            this.comboTabela.TabIndex = 51;
            this.comboTabela.ValueMember = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 50;
            this.label1.Text = "DataBase:";
            // 
            // comboBanco
            // 
            this.comboBanco.DisplayMember = "Name";
            this.comboBanco.Enabled = false;
            this.comboBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBanco.FormattingEnabled = true;
            this.comboBanco.Location = new System.Drawing.Point(149, 199);
            this.comboBanco.Name = "comboBanco";
            this.comboBanco.Size = new System.Drawing.Size(364, 24);
            this.comboBanco.TabIndex = 49;
            this.comboBanco.ValueMember = "Name";
            this.comboBanco.SelectedIndexChanged += new System.EventHandler(this.comboBanco_SelectedIndexChanged);
            // 
            // txtServidor
            // 
            this.txtServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServidor.Location = new System.Drawing.Point(149, 48);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(364, 22);
            this.txtServidor.TabIndex = 62;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 61;
            this.label4.Text = "IP Servidor DB:";
            // 
            // btnDataBases
            // 
            this.btnDataBases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataBases.Location = new System.Drawing.Point(15, 160);
            this.btnDataBases.Name = "btnDataBases";
            this.btnDataBases.Size = new System.Drawing.Size(146, 29);
            this.btnDataBases.TabIndex = 49;
            this.btnDataBases.Text = "Buscar DataBases";
            this.btnDataBases.UseVisualStyleBackColor = true;
            this.btnDataBases.Click += new System.EventHandler(this.btnDataBases_Click);
            // 
            // btnGerar
            // 
            this.btnGerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerar.Location = new System.Drawing.Point(15, 366);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(146, 29);
            this.btnGerar.TabIndex = 64;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // comboLinguagem
            // 
            this.comboLinguagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboLinguagem.FormattingEnabled = true;
            this.comboLinguagem.Items.AddRange(new object[] {
            "C#",
            "Java"});
            this.comboLinguagem.Location = new System.Drawing.Point(149, 293);
            this.comboLinguagem.Name = "comboLinguagem";
            this.comboLinguagem.Size = new System.Drawing.Size(364, 24);
            this.comboLinguagem.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 65;
            this.label3.Text = "Linguagem:";
            // 
            // cbDataAnnotations
            // 
            this.cbDataAnnotations.AutoSize = true;
            this.cbDataAnnotations.Checked = true;
            this.cbDataAnnotations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDataAnnotations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDataAnnotations.Location = new System.Drawing.Point(15, 327);
            this.cbDataAnnotations.Name = "cbDataAnnotations";
            this.cbDataAnnotations.Size = new System.Drawing.Size(127, 20);
            this.cbDataAnnotations.TabIndex = 67;
            this.cbDataAnnotations.Text = "Data annotations";
            this.cbDataAnnotations.UseVisualStyleBackColor = true;
            // 
            // cbRepositorio
            // 
            this.cbRepositorio.AutoSize = true;
            this.cbRepositorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRepositorio.Location = new System.Drawing.Point(149, 327);
            this.cbRepositorio.Name = "cbRepositorio";
            this.cbRepositorio.Size = new System.Drawing.Size(92, 20);
            this.cbRepositorio.TabIndex = 68;
            this.cbRepositorio.Text = "Repository";
            this.cbRepositorio.UseVisualStyleBackColor = true;
            // 
            // FormGerar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 407);
            this.Controls.Add(this.cbRepositorio);
            this.Controls.Add(this.cbDataAnnotations);
            this.Controls.Add(this.comboLinguagem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txtServidor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboTipoLogon);
            this.Controls.Add(this.btnDataBases);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbPacote);
            this.Controls.Add(this.txtPacote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboTabela);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBanco);
            this.Controls.Add(this.comboDB);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormGerar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboDB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboTipoLogon;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbPacote;
        private System.Windows.Forms.TextBox txtPacote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboTabela;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBanco;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDataBases;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.ComboBox comboLinguagem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbDataAnnotations;
        private System.Windows.Forms.CheckBox cbRepositorio;
    }
}