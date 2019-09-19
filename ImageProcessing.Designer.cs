namespace ImageProcessing
{
    partial class ImageProcessing
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirArquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.desfazer = new System.Windows.Forms.ToolStripMenuItem();
            this.operaçõesAritméticasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adicaoLimiar = new System.Windows.Forms.ToolStripMenuItem();
            this.adicaoMedia = new System.Windows.Forms.ToolStripMenuItem();
            this.subtracaoLimiar = new System.Windows.Forms.ToolStripMenuItem();
            this.subtracaoMedia = new System.Windows.Forms.ToolStripMenuItem();
            this.multiplicacao = new System.Windows.Forms.ToolStripMenuItem();
            this.divisao = new System.Windows.Forms.ToolStripMenuItem();
            this.operacoesLogicas = new System.Windows.Forms.ToolStripMenuItem();
            this.not = new System.Windows.Forms.ToolStripMenuItem();
            this.logicOr = new System.Windows.Forms.ToolStripMenuItem();
            this.LogicAnd = new System.Windows.Forms.ToolStripMenuItem();
            this.logicXor = new System.Windows.Forms.ToolStripMenuItem();
            this.LogicSub = new System.Windows.Forms.ToolStripMenuItem();
            this.filtrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtroMedia = new System.Windows.Forms.ToolStripMenuItem();
            this.filtroMediana = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.histograma = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirArquivoToolStripMenuItem,
            this.salvarToolStripMenuItem,
            this.fecharToolStripMenuItem,
            this.toolStripMenuItem1,
            this.sairToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.menuToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirArquivoToolStripMenuItem
            // 
            this.abrirArquivoToolStripMenuItem.Name = "abrirArquivoToolStripMenuItem";
            this.abrirArquivoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirArquivoToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.abrirArquivoToolStripMenuItem.Text = "Abrir";
            this.abrirArquivoToolStripMenuItem.Click += new System.EventHandler(this.AbrirArquivo_Click);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.salvarToolStripMenuItem.Text = "Salvar";
            this.salvarToolStripMenuItem.Click += new System.EventHandler(this.SalvarArquivo_Click);
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.fecharToolStripMenuItem.Text = "Fechar";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(142, 6);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.Sair_Click);
            // 
            // editarMenu
            // 
            this.editarMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desfazer,
            this.operaçõesAritméticasToolStripMenuItem,
            this.operacoesLogicas,
            this.filtrosToolStripMenuItem});
            this.editarMenu.Name = "editarMenu";
            this.editarMenu.Size = new System.Drawing.Size(49, 20);
            this.editarMenu.Text = "Editar";
            // 
            // desfazer
            // 
            this.desfazer.Name = "desfazer";
            this.desfazer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.desfazer.Size = new System.Drawing.Size(193, 22);
            this.desfazer.Text = "Desfazer";
            this.desfazer.Click += new System.EventHandler(this.Desfazer_Click);
            // 
            // operaçõesAritméticasToolStripMenuItem
            // 
            this.operaçõesAritméticasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adicaoLimiar,
            this.adicaoMedia,
            this.subtracaoLimiar,
            this.subtracaoMedia,
            this.multiplicacao,
            this.divisao});
            this.operaçõesAritméticasToolStripMenuItem.Name = "operaçõesAritméticasToolStripMenuItem";
            this.operaçõesAritméticasToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.operaçõesAritméticasToolStripMenuItem.Text = "Operações Aritméticas";
            // 
            // adicaoLimiar
            // 
            this.adicaoLimiar.Name = "adicaoLimiar";
            this.adicaoLimiar.Size = new System.Drawing.Size(163, 22);
            this.adicaoLimiar.Text = "Adição Limiar";
            this.adicaoLimiar.Click += new System.EventHandler(this.Adicao_Click);
            // 
            // adicaoMedia
            // 
            this.adicaoMedia.Name = "adicaoMedia";
            this.adicaoMedia.Size = new System.Drawing.Size(163, 22);
            this.adicaoMedia.Text = "Adição Média";
            this.adicaoMedia.Click += new System.EventHandler(this.AdicaoMedia_Click);
            // 
            // subtracaoLimiar
            // 
            this.subtracaoLimiar.Name = "subtracaoLimiar";
            this.subtracaoLimiar.Size = new System.Drawing.Size(163, 22);
            this.subtracaoLimiar.Text = "Subtração Limiar";
            this.subtracaoLimiar.Click += new System.EventHandler(this.SubtracaoLimiar_Click);
            // 
            // subtracaoMedia
            // 
            this.subtracaoMedia.Name = "subtracaoMedia";
            this.subtracaoMedia.Size = new System.Drawing.Size(163, 22);
            this.subtracaoMedia.Text = "Subtração Média";
            this.subtracaoMedia.Click += new System.EventHandler(this.SubtracaoMedia_Click);
            // 
            // multiplicacao
            // 
            this.multiplicacao.Name = "multiplicacao";
            this.multiplicacao.Size = new System.Drawing.Size(163, 22);
            this.multiplicacao.Text = "Multiplicação";
            this.multiplicacao.Click += new System.EventHandler(this.Multiplicacao_Click);
            // 
            // divisao
            // 
            this.divisao.Name = "divisao";
            this.divisao.Size = new System.Drawing.Size(163, 22);
            this.divisao.Text = "Divisão";
            this.divisao.Click += new System.EventHandler(this.Divisao_Click);
            // 
            // operacoesLogicas
            // 
            this.operacoesLogicas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.not,
            this.logicOr,
            this.LogicAnd,
            this.logicXor,
            this.LogicSub});
            this.operacoesLogicas.Name = "operacoesLogicas";
            this.operacoesLogicas.Size = new System.Drawing.Size(193, 22);
            this.operacoesLogicas.Text = "Operações Lógicas";
            // 
            // not
            // 
            this.not.Name = "not";
            this.not.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.not.Size = new System.Drawing.Size(137, 22);
            this.not.Text = "Not";
            this.not.Click += new System.EventHandler(this.LogicNot_Click);
            // 
            // logicOr
            // 
            this.logicOr.Name = "logicOr";
            this.logicOr.Size = new System.Drawing.Size(137, 22);
            this.logicOr.Text = "Or";
            this.logicOr.Click += new System.EventHandler(this.LogicOr_Click);
            // 
            // LogicAnd
            // 
            this.LogicAnd.Name = "LogicAnd";
            this.LogicAnd.Size = new System.Drawing.Size(137, 22);
            this.LogicAnd.Text = "And";
            this.LogicAnd.Click += new System.EventHandler(this.LogicAnd_Click);
            // 
            // logicXor
            // 
            this.logicXor.Name = "logicXor";
            this.logicXor.Size = new System.Drawing.Size(137, 22);
            this.logicXor.Text = "Xor";
            this.logicXor.Click += new System.EventHandler(this.LogicXor_Click);
            // 
            // LogicSub
            // 
            this.LogicSub.Name = "LogicSub";
            this.LogicSub.Size = new System.Drawing.Size(137, 22);
            this.LogicSub.Text = "Sub";
            this.LogicSub.Click += new System.EventHandler(this.LogicSub_Click);
            // 
            // filtrosToolStripMenuItem
            // 
            this.filtrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtroMedia,
            this.filtroMediana,
            this.histograma});
            this.filtrosToolStripMenuItem.Name = "filtrosToolStripMenuItem";
            this.filtrosToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.filtrosToolStripMenuItem.Text = "Filtros";
            // 
            // filtroMedia
            // 
            this.filtroMedia.Name = "filtroMedia";
            this.filtroMedia.Size = new System.Drawing.Size(180, 22);
            this.filtroMedia.Text = "Média";
            this.filtroMedia.Click += new System.EventHandler(this.FiltroMedia_Click);
            // 
            // filtroMediana
            // 
            this.filtroMediana.Name = "filtroMediana";
            this.filtroMediana.Size = new System.Drawing.Size(180, 22);
            this.filtroMediana.Text = "Mediana";
            this.filtroMediana.Click += new System.EventHandler(this.FiltroMediana_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.editarMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 422);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // histograma
            // 
            this.histograma.Name = "histograma";
            this.histograma.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.histograma.Size = new System.Drawing.Size(180, 22);
            this.histograma.Text = "Histograma";
            this.histograma.Click += new System.EventHandler(this.Histograma_Click);
            // 
            // ImageProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ImageProcessing";
            this.Text = "Processamento de Imagens";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirArquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarMenu;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem operaçõesAritméticasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adicaoLimiar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem adicaoMedia;
        private System.Windows.Forms.ToolStripMenuItem subtracaoLimiar;
        private System.Windows.Forms.ToolStripMenuItem subtracaoMedia;
        private System.Windows.Forms.ToolStripMenuItem multiplicacao;
        private System.Windows.Forms.ToolStripMenuItem divisao;
        private System.Windows.Forms.ToolStripMenuItem operacoesLogicas;
        private System.Windows.Forms.ToolStripMenuItem not;
        private System.Windows.Forms.ToolStripMenuItem filtrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtroMedia;
        private System.Windows.Forms.ToolStripMenuItem filtroMediana;
        private System.Windows.Forms.ToolStripMenuItem logicOr;
        private System.Windows.Forms.ToolStripMenuItem LogicAnd;
        private System.Windows.Forms.ToolStripMenuItem logicXor;
        private System.Windows.Forms.ToolStripMenuItem LogicSub;
        private System.Windows.Forms.ToolStripMenuItem desfazer;
        private System.Windows.Forms.ToolStripMenuItem histograma;
    }
}

