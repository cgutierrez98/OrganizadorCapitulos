using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OrganizadorCapitulos.WinForms.UI.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        
        // Controles declarados por el diseñador
        private System.Windows.Forms.ListView listViewSeries;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnCargarCarpetas;
        private System.Windows.Forms.Button btnGuardarTodo;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnMetadata;
        private System.Windows.Forms.Button btnAIAnalyze;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxOpciones;
        private System.Windows.Forms.RadioButton radioCambiar;
        private System.Windows.Forms.RadioButton radioMantener;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxDetalles;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtCapitulo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTemporada;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTituloEpisodio;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Label lblStatus;

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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            listViewSeries = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            btnCargarCarpetas = new Button();
            btnGuardarTodo = new Button();
            btnSettings = new Button();
            btnMetadata = new Button();
            btnAIAnalyze = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            groupBoxOpciones = new GroupBox();
            radioCambiar = new RadioButton();
            radioMantener = new RadioButton();
            panel2 = new Panel();
            groupBoxDetalles = new GroupBox();
            btnGuardar = new Button();
            txtCapitulo = new TextBox();
            label3 = new Label();
            txtTemporada = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtTitulo = new TextBox();
            errorProvider = new ErrorProvider(components);
            panel3 = new Panel();
            label4 = new Label();
            txtTituloEpisodio = new TextBox();
            btnUndo = new Button();
            btnRedo = new Button();
            lblStatus = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            groupBoxOpciones.SuspendLayout();
            panel2.SuspendLayout();
            groupBoxDetalles.SuspendLayout();
            ((ISupportInitialize)errorProvider).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // listViewSeries
            // 
            listViewSeries.BackColor = Color.FromArgb(250, 251, 252);
            listViewSeries.BorderStyle = BorderStyle.None;
            listViewSeries.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listViewSeries.Dock = DockStyle.Fill;
            listViewSeries.Font = new Font("Segoe UI", 10F);
            listViewSeries.FullRowSelect = true;
            listViewSeries.GridLines = true;
            listViewSeries.Location = new Point(15, 70);
            listViewSeries.Margin = new Padding(15, 10, 15, 10);
            listViewSeries.Name = "listViewSeries";
            listViewSeries.Size = new Size(800, 450);
            listViewSeries.TabIndex = 0;
            listViewSeries.UseCompatibleStateImageBehavior = false;
            listViewSeries.View = View.Details;
            listViewSeries.ColumnClick += ListViewSeries_ColumnClick;
            listViewSeries.SelectedIndexChanged += ListViewSeries_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Nombre del archivo";
            columnHeader1.Width = 400;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Ruta completa";
            columnHeader2.Width = 900;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Título Episodio";
            columnHeader3.Width = 300;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Vista Previa";
            columnHeader4.Width = 350;
            // 
            // btnCargarCarpetas
            // 
            btnCargarCarpetas.Anchor = AnchorStyles.Left;
            btnCargarCarpetas.BackColor = Color.FromArgb(59, 130, 246);
            btnCargarCarpetas.Cursor = Cursors.Hand;
            btnCargarCarpetas.FlatAppearance.BorderSize = 0;
            btnCargarCarpetas.FlatAppearance.MouseOverBackColor = Color.FromArgb(37, 99, 235);
            btnCargarCarpetas.FlatStyle = FlatStyle.Flat;
            btnCargarCarpetas.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnCargarCarpetas.ForeColor = Color.White;
            btnCargarCarpetas.ImageAlign = ContentAlignment.MiddleLeft;
            btnCargarCarpetas.Location = new Point(15, 12);
            btnCargarCarpetas.Name = "btnCargarCarpetas";
            btnCargarCarpetas.Size = new Size(165, 38);
            btnCargarCarpetas.TabIndex = 1;
            btnCargarCarpetas.Text = "📁 Cargar carpetas";
            btnCargarCarpetas.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCargarCarpetas.UseVisualStyleBackColor = false;
            btnCargarCarpetas.Click += BtnCargarCarpetas_Click;
            // 
            // btnGuardarTodo
            // 
            btnGuardarTodo.Anchor = AnchorStyles.Right;
            btnGuardarTodo.BackColor = Color.FromArgb(16, 185, 129);
            btnGuardarTodo.Cursor = Cursors.Hand;
            btnGuardarTodo.FlatAppearance.BorderSize = 0;
            btnGuardarTodo.FlatAppearance.MouseOverBackColor = Color.FromArgb(5, 150, 105);
            btnGuardarTodo.FlatStyle = FlatStyle.Flat;
            btnGuardarTodo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnGuardarTodo.ForeColor = Color.White;
            btnGuardarTodo.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarTodo.Location = new Point(1428, 12);
            btnGuardarTodo.Name = "btnGuardarTodo";
            btnGuardarTodo.Size = new Size(165, 38);
            btnGuardarTodo.TabIndex = 2;
            btnGuardarTodo.Text = "💾 Mover y Renombrar";
            btnGuardarTodo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardarTodo.UseVisualStyleBackColor = false;
            btnGuardarTodo.Click += BtnGuardarTodo_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(listViewSeries, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 2);
            tableLayoutPanel1.Controls.Add(panel3, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Padding = new Padding(10);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.Size = new Size(1608, 868);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 255, 255);
            panel1.Padding = new Padding(10);
            panel1.Controls.Add(groupBoxOpciones);
            panel1.Controls.Add(btnCargarCarpetas);
            panel1.Controls.Add(btnGuardarTodo);
            panel1.Controls.Add(btnSettings);
            panel1.Controls.Add(btnMetadata);
            panel1.Controls.Add(btnAIAnalyze);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1608, 60);
            panel1.TabIndex = 3;
            // 
            // groupBoxOpciones
            // 
            groupBoxOpciones.Anchor = AnchorStyles.Top;
            groupBoxOpciones.Controls.Add(radioCambiar);
            groupBoxOpciones.Controls.Add(radioMantener);
            groupBoxOpciones.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            groupBoxOpciones.ForeColor = Color.FromArgb(55, 65, 81);
            groupBoxOpciones.Location = new Point(650, 5);
            groupBoxOpciones.Name = "groupBoxOpciones";
            groupBoxOpciones.Size = new Size(310, 50);
            groupBoxOpciones.TabIndex = 5;
            groupBoxOpciones.TabStop = false;
            groupBoxOpciones.Text = "Modo de renombrado";
            // 
            // radioCambiar
            // 
            radioCambiar.AutoSize = true;
            radioCambiar.Location = new Point(170, 20);
            radioCambiar.Name = "radioCambiar";
            radioCambiar.Size = new Size(126, 19);
            radioCambiar.TabIndex = 4;
            radioCambiar.Text = "Cambiar estructura";
            radioCambiar.UseVisualStyleBackColor = true;
            radioCambiar.CheckedChanged += RadioCambiar_CheckedChanged;
            // 
            // radioMantener
            // 
            radioMantener.AutoSize = true;
            radioMantener.Checked = true;
            radioMantener.Location = new Point(20, 20);
            radioMantener.Name = "radioMantener";
            radioMantener.Size = new Size(132, 19);
            radioMantener.TabIndex = 3;
            radioMantener.TabStop = true;
            radioMantener.Text = "Mantener estructura";
            radioMantener.UseVisualStyleBackColor = true;
            radioMantener.CheckedChanged += RadioMantener_CheckedChanged;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(255, 255, 255);
            panel2.Controls.Add(groupBoxDetalles);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(15, 678);
            panel2.Margin = new Padding(15, 10, 15, 10);
            panel2.Name = "panel2";
            panel2.Size = new Size(1578, 130);
            panel2.TabIndex = 4;
            // 
            // groupBoxDetalles
            // 
            groupBoxDetalles.Controls.Add(label4);
            groupBoxDetalles.Controls.Add(txtTituloEpisodio);
            groupBoxDetalles.Controls.Add(btnGuardar);
            groupBoxDetalles.Controls.Add(txtCapitulo);
            groupBoxDetalles.Controls.Add(label3);
            groupBoxDetalles.Controls.Add(txtTemporada);
            groupBoxDetalles.Controls.Add(label2);
            groupBoxDetalles.Controls.Add(label1);
            groupBoxDetalles.Controls.Add(txtTitulo);
            groupBoxDetalles.Dock = DockStyle.Fill;
            groupBoxDetalles.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            groupBoxDetalles.ForeColor = Color.FromArgb(55, 65, 81);
            groupBoxDetalles.Location = new Point(0, 0);
            groupBoxDetalles.Name = "groupBoxDetalles";
            groupBoxDetalles.Size = new Size(1578, 130);
            groupBoxDetalles.TabIndex = 7;
            groupBoxDetalles.TabStop = false;
            groupBoxDetalles.Text = "Detalles del capítulo seleccionado";
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.BackColor = Color.FromArgb(99, 102, 241);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(79, 70, 229);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.Location = new Point(1405, 80);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(160, 38);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "✓ Guardar cambios";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += BtnGuardar_Click;
            // 
            // txtCapitulo
            // 
            txtCapitulo.BorderStyle = BorderStyle.FixedSingle;
            txtCapitulo.Font = new Font("Segoe UI", 10F);
            txtCapitulo.Location = new Point(240, 85);
            txtCapitulo.MaxLength = 3;
            txtCapitulo.Name = "txtCapitulo";
            txtCapitulo.Size = new Size(70, 25);
            txtCapitulo.TabIndex = 5;
            txtCapitulo.Text = "1";
            txtCapitulo.KeyPress += TxtNumerico_KeyPress;
            txtCapitulo.Validating += TxtCapitulo_Validating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F);
            label3.ForeColor = Color.FromArgb(75, 85, 99);
            label3.Location = new Point(180, 88);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 4;
            label3.Text = "Capítulo:";
            // 
            // txtTemporada
            // 
            txtTemporada.BorderStyle = BorderStyle.FixedSingle;
            txtTemporada.Font = new Font("Segoe UI", 10F);
            txtTemporada.Location = new Point(90, 85);
            txtTemporada.MaxLength = 2;
            txtTemporada.Name = "txtTemporada";
            txtTemporada.Size = new Size(70, 25);
            txtTemporada.TabIndex = 3;
            txtTemporada.Text = "1";
            txtTemporada.KeyPress += TxtNumerico_KeyPress;
            txtTemporada.Validating += TxtTemporada_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F);
            label2.ForeColor = Color.FromArgb(75, 85, 99);
            label2.Location = new Point(20, 88);
            label2.Name = "label2";
            label2.Size = new Size(78, 17);
            label2.TabIndex = 2;
            label2.Text = "Temporada:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F);
            label1.ForeColor = Color.FromArgb(75, 85, 99);
            label1.Location = new Point(20, 30);
            label1.Name = "label1";
            label1.Size = new Size(130, 17);
            label1.TabIndex = 1;
            label1.Text = "Nombre de la Serie:";
            // 
            // txtTitulo
            // 
            txtTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTitulo.BorderStyle = BorderStyle.FixedSingle;
            txtTitulo.Font = new Font("Segoe UI", 10F);
            txtTitulo.Location = new Point(160, 27);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(1395, 25);
            txtTitulo.TabIndex = 0;
            txtTitulo.KeyDown += OnClickEnter;
            txtTitulo.Validating += TxtTitulo_Validating;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9.75F);
            label4.ForeColor = Color.FromArgb(75, 85, 99);
            label4.Location = new Point(20, 63);
            label4.Name = "label4";
            label4.Size = new Size(130, 17);
            label4.TabIndex = 7;
            label4.Text = "Título del Episodio:";
            // 
            // txtTituloEpisodio
            // 
            txtTituloEpisodio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTituloEpisodio.BorderStyle = BorderStyle.FixedSingle;
            txtTituloEpisodio.Font = new Font("Segoe UI", 10F);
            txtTituloEpisodio.Location = new Point(160, 60);
            txtTituloEpisodio.Name = "txtTituloEpisodio";
            txtTituloEpisodio.Size = new Size(1395, 25);
            txtTituloEpisodio.TabIndex = 8;
            txtTituloEpisodio.KeyDown += OnClickEnter;
            txtTituloEpisodio.Validating += TxtTituloEpisodio_Validating;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(249, 250, 251);
            panel3.Controls.Add(btnUndo);
            panel3.Controls.Add(btnRedo);
            panel3.Controls.Add(lblStatus);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 828);
            panel3.Name = "panel3";
            panel3.Size = new Size(1608, 40);
            panel3.TabIndex = 5;
            // 
            // btnUndo
            // 
            btnUndo.Anchor = AnchorStyles.Left;
            btnUndo.BackColor = Color.FromArgb(229, 231, 235);
            btnUndo.Cursor = Cursors.Hand;
            btnUndo.FlatStyle = FlatStyle.Flat;
            btnUndo.Font = new Font("Segoe UI", 9F);
            btnUndo.Location = new Point(20, 4);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(80, 32);
            btnUndo.TabIndex = 0;
            btnUndo.Text = "Deshacer";
            btnUndo.UseVisualStyleBackColor = false;
            btnUndo.Click += BtnUndo_Click;
            // 
            // btnRedo
            // 
            btnRedo.Anchor = AnchorStyles.Left;
            btnRedo.BackColor = Color.FromArgb(229, 231, 235);
            btnRedo.Cursor = Cursors.Hand;
            btnRedo.FlatStyle = FlatStyle.Flat;
            btnRedo.Font = new Font("Segoe UI", 9F);
            btnRedo.Location = new Point(110, 4);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(80, 32);
            btnRedo.TabIndex = 1;
            btnRedo.Text = "Rehacer";
            btnRedo.UseVisualStyleBackColor = false;
            btnRedo.Click += BtnRedo_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Left;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.FromArgb(75, 85, 99);
            lblStatus.Location = new Point(210, 10);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(600, 20);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Listo";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            MinimumSize = new Size(1000, 700);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "OrganizadorCapitulos";
            this.Resize += MainForm_Resize;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBoxOpciones.ResumeLayout(false);
            groupBoxOpciones.PerformLayout();
            panel2.ResumeLayout(false);
            groupBoxDetalles.ResumeLayout(false);
            groupBoxDetalles.PerformLayout();
            ((ISupportInitialize)errorProvider).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
