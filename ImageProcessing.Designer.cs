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
            this.abrirArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirVarios = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.desfazer = new System.Windows.Forms.ToolStripMenuItem();
            this.opBasicas = new System.Windows.Forms.ToolStripMenuItem();
            this.opQuantizacao = new System.Windows.Forms.ToolStripMenuItem();
            this.opMinMax = new System.Windows.Forms.ToolStripMenuItem();
            this.toTonsDeCinza = new System.Windows.Forms.ToolStripMenuItem();
            this.inverterCores = new System.Windows.Forms.ToolStripMenuItem();
            this.opAritmeticas = new System.Windows.Forms.ToolStripMenuItem();
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
            this.passaAltaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prewittToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bordasIsotropico = new System.Windows.Forms.ToolStripMenuItem();
            this.laplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretching = new System.Windows.Forms.ToolStripMenuItem();
            this.stLinear = new System.Windows.Forms.ToolStripMenuItem();
            this.stQuadrado = new System.Windows.Forms.ToolStripMenuItem();
            this.stRaizQuadrada = new System.Windows.Forms.ToolStripMenuItem();
            this.stLogarítmico = new System.Windows.Forms.ToolStripMenuItem();
            this.stNegativo = new System.Windows.Forms.ToolStripMenuItem();
            this.histograma = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaImagens = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirArquivo,
            this.abrirVarios,
            this.salvarArquivo,
            this.fecharArquivo,
            this.toolStripMenuItem1,
            this.sairToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.menuToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirArquivo
            // 
            this.abrirArquivo.Name = "abrirArquivo";
            this.abrirArquivo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirArquivo.Size = new System.Drawing.Size(209, 22);
            this.abrirArquivo.Text = "Abrir";
            this.abrirArquivo.Click += new System.EventHandler(this.AbrirArquivo_Click);
            // 
            // abrirVarios
            // 
            this.abrirVarios.Name = "abrirVarios";
            this.abrirVarios.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.abrirVarios.Size = new System.Drawing.Size(209, 22);
            this.abrirVarios.Text = "Abrir Vários";
            this.abrirVarios.Click += new System.EventHandler(this.AbrirVarios_Click);
            // 
            // salvarArquivo
            // 
            this.salvarArquivo.Name = "salvarArquivo";
            this.salvarArquivo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.salvarArquivo.Size = new System.Drawing.Size(209, 22);
            this.salvarArquivo.Text = "Salvar";
            this.salvarArquivo.Click += new System.EventHandler(this.SalvarArquivo_Click);
            // 
            // fecharArquivo
            // 
            this.fecharArquivo.Name = "fecharArquivo";
            this.fecharArquivo.Size = new System.Drawing.Size(209, 22);
            this.fecharArquivo.Text = "Fechar";
            this.fecharArquivo.Click += new System.EventHandler(this.FecharToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(206, 6);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.Sair_Click);
            // 
            // editarMenu
            // 
            this.editarMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desfazer,
            this.opBasicas,
            this.opAritmeticas,
            this.operacoesLogicas,
            this.filtrosToolStripMenuItem,
            this.stretching,
            this.histograma,
            this.mediaImagens});
            this.editarMenu.Name = "editarMenu";
            this.editarMenu.Size = new System.Drawing.Size(49, 20);
            this.editarMenu.Text = "Editar";
            // 
            // desfazer
            // 
            this.desfazer.Name = "desfazer";
            this.desfazer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.desfazer.Size = new System.Drawing.Size(263, 22);
            this.desfazer.Text = "Desfazer";
            this.desfazer.Click += new System.EventHandler(this.Desfazer_Click);
            // 
            // opBasicas
            // 
            this.opBasicas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opQuantizacao,
            this.opMinMax,
            this.toTonsDeCinza,
            this.inverterCores});
            this.opBasicas.Name = "opBasicas";
            this.opBasicas.Size = new System.Drawing.Size(263, 22);
            this.opBasicas.Text = "Operações Básicas";
            // 
            // opQuantizacao
            // 
            this.opQuantizacao.Name = "opQuantizacao";
            this.opQuantizacao.Size = new System.Drawing.Size(181, 22);
            this.opQuantizacao.Text = "Quantização";
            this.opQuantizacao.Click += new System.EventHandler(this.OpQuantizacao_Click);
            // 
            // opMinMax
            // 
            this.opMinMax.Name = "opMinMax";
            this.opMinMax.Size = new System.Drawing.Size(181, 22);
            this.opMinMax.Text = "Correção (Min-Max)";
            // 
            // toTonsDeCinza
            // 
            this.toTonsDeCinza.Name = "toTonsDeCinza";
            this.toTonsDeCinza.Size = new System.Drawing.Size(181, 22);
            this.toTonsDeCinza.Text = "Tons de Cinza";
            this.toTonsDeCinza.Click += new System.EventHandler(this.TonsDeCinzaToolStripMenuItem_Click);
            // 
            // inverterCores
            // 
            this.inverterCores.Name = "inverterCores";
            this.inverterCores.Size = new System.Drawing.Size(181, 22);
            this.inverterCores.Text = "Inverter Cores";
            this.inverterCores.Click += new System.EventHandler(this.InverterCores_Click);
            // 
            // opAritmeticas
            // 
            this.opAritmeticas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adicaoLimiar,
            this.adicaoMedia,
            this.subtracaoLimiar,
            this.subtracaoMedia,
            this.multiplicacao,
            this.divisao});
            this.opAritmeticas.Name = "opAritmeticas";
            this.opAritmeticas.Size = new System.Drawing.Size(263, 22);
            this.opAritmeticas.Text = "Operações Aritméticas";
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
            this.operacoesLogicas.Size = new System.Drawing.Size(263, 22);
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
            this.passaAltaToolStripMenuItem,
            this.sobelToolStripMenuItem,
            this.prewittToolStripMenuItem,
            this.robertsToolStripMenuItem,
            this.bordasIsotropico,
            this.laplaceToolStripMenuItem});
            this.filtrosToolStripMenuItem.Name = "filtrosToolStripMenuItem";
            this.filtrosToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.filtrosToolStripMenuItem.Text = "Convoluções";
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
            // passaAltaToolStripMenuItem
            // 
            this.passaAltaToolStripMenuItem.Name = "passaAltaToolStripMenuItem";
            this.passaAltaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.passaAltaToolStripMenuItem.Text = "Passa Alta";
            // 
            // sobelToolStripMenuItem
            // 
            this.sobelToolStripMenuItem.Name = "sobelToolStripMenuItem";
            this.sobelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sobelToolStripMenuItem.Text = "Sobel";
            this.sobelToolStripMenuItem.Click += new System.EventHandler(this.SobelToolStripMenuItem_Click);
            // 
            // prewittToolStripMenuItem
            // 
            this.prewittToolStripMenuItem.Name = "prewittToolStripMenuItem";
            this.prewittToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.prewittToolStripMenuItem.Text = "Prewitt";
            this.prewittToolStripMenuItem.Click += new System.EventHandler(this.PrewittToolStripMenuItem_Click);
            // 
            // robertsToolStripMenuItem
            // 
            this.robertsToolStripMenuItem.Name = "robertsToolStripMenuItem";
            this.robertsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.robertsToolStripMenuItem.Text = "Roberts";
            this.robertsToolStripMenuItem.Click += new System.EventHandler(this.RobertsToolStripMenuItem_Click);
            // 
            // bordasIsotropico
            // 
            this.bordasIsotropico.Name = "bordasIsotropico";
            this.bordasIsotropico.Size = new System.Drawing.Size(180, 22);
            this.bordasIsotropico.Text = "Isotrópico";
            this.bordasIsotropico.Click += new System.EventHandler(this.BordasIsotropico_Click);
            // 
            // laplaceToolStripMenuItem
            // 
            this.laplaceToolStripMenuItem.Name = "laplaceToolStripMenuItem";
            this.laplaceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.laplaceToolStripMenuItem.Text = "Laplace";
            this.laplaceToolStripMenuItem.Click += new System.EventHandler(this.LaplaceToolStripMenuItem_Click);
            // 
            // stretching
            // 
            this.stretching.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stLinear,
            this.stQuadrado,
            this.stRaizQuadrada,
            this.stLogarítmico,
            this.stNegativo});
            this.stretching.Name = "stretching";
            this.stretching.Size = new System.Drawing.Size(263, 22);
            this.stretching.Text = "Stretching";
            // 
            // stLinear
            // 
            this.stLinear.Name = "stLinear";
            this.stLinear.Size = new System.Drawing.Size(150, 22);
            this.stLinear.Text = "Linear";
            // 
            // stQuadrado
            // 
            this.stQuadrado.Name = "stQuadrado";
            this.stQuadrado.Size = new System.Drawing.Size(150, 22);
            this.stQuadrado.Text = "Quadrado";
            // 
            // stRaizQuadrada
            // 
            this.stRaizQuadrada.Name = "stRaizQuadrada";
            this.stRaizQuadrada.Size = new System.Drawing.Size(150, 22);
            this.stRaizQuadrada.Text = "Raiz Quadrada";
            // 
            // stLogarítmico
            // 
            this.stLogarítmico.Name = "stLogarítmico";
            this.stLogarítmico.Size = new System.Drawing.Size(150, 22);
            this.stLogarítmico.Text = "Logarítmico";
            // 
            // stNegativo
            // 
            this.stNegativo.Name = "stNegativo";
            this.stNegativo.Size = new System.Drawing.Size(150, 22);
            this.stNegativo.Text = "Negativo";
            // 
            // histograma
            // 
            this.histograma.Name = "histograma";
            this.histograma.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.histograma.Size = new System.Drawing.Size(263, 22);
            this.histograma.Text = "Equalização de  Histograma";
            // 
            // mediaImagens
            // 
            this.mediaImagens.Name = "mediaImagens";
            this.mediaImagens.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.mediaImagens.Size = new System.Drawing.Size(263, 22);
            this.mediaImagens.Text = "Média de Imagens";
            this.mediaImagens.Click += new System.EventHandler(this.MediaImagens_Click);
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
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 422);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
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
        private System.Windows.Forms.ToolStripMenuItem abrirArquivo;
        private System.Windows.Forms.ToolStripMenuItem salvarArquivo;
        private System.Windows.Forms.ToolStripMenuItem fecharArquivo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarMenu;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opAritmeticas;
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
        private System.Windows.Forms.ToolStripMenuItem opBasicas;
        private System.Windows.Forms.ToolStripMenuItem opQuantizacao;
        private System.Windows.Forms.ToolStripMenuItem opMinMax;
        private System.Windows.Forms.ToolStripMenuItem passaAltaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prewittToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem robertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bordasIsotropico;
        private System.Windows.Forms.ToolStripMenuItem laplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stretching;
        private System.Windows.Forms.ToolStripMenuItem stLinear;
        private System.Windows.Forms.ToolStripMenuItem stQuadrado;
        private System.Windows.Forms.ToolStripMenuItem stRaizQuadrada;
        private System.Windows.Forms.ToolStripMenuItem stLogarítmico;
        private System.Windows.Forms.ToolStripMenuItem stNegativo;
        private System.Windows.Forms.ToolStripMenuItem histograma;
        private System.Windows.Forms.ToolStripMenuItem abrirVarios;
        private System.Windows.Forms.ToolStripMenuItem mediaImagens;
        private System.Windows.Forms.ToolStripMenuItem toTonsDeCinza;
        private System.Windows.Forms.ToolStripMenuItem inverterCores;
    }
}

