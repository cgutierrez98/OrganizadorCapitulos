using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace organizadorCapitulos.UI.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

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
            btnCargarCarpetas = new Button();
            btnGuardarTodo = new Button();
            btnSettings = new Button();
            btnMetadata = new Button();
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
            listViewSeries.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            listViewSeries.Dock = DockStyle.Fill;
            listViewSeries.Font = new Font("Segoe UI", 10F);
            listViewSeries.FullRowSelect = true;
            listViewSeries.GridLines = true;
            listViewSeries.Location = new Point(15, 70);
            listViewSeries.Margin = new Padding(15, 10, 15, 10);
            listViewSeries.Name = "listViewSeries";
            listViewSeries.Size = new Size(1578, 598);
            listViewSeries.TabIndex = 0;
            listViewSeries.UseCompatibleStateImageBehavior = false;
            listViewSeries.View = View.Details;
            listViewSeries.ColumnClick += listViewSeries_ColumnClick;
            listViewSeries.SelectedIndexChanged += listViewSeries_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Nombre del archivo";
            columnHeader1.Width = 350;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Ruta completa";
            columnHeader2.Width = 1200;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Nuevo Nombre";
            columnHeader3.Width = 350;
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
            btnCargarCarpetas.Click += btnCargarCarpetas_Click;
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
            btnGuardarTodo.Text = "💾 Guardar todo";
            btnGuardarTodo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardarTodo.UseVisualStyleBackColor = false;
            btnGuardarTodo.Click += btnGuardarTodo_Click;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Right;
            btnSettings.BackColor = Color.FromArgb(107, 114, 128);
            btnSettings.Cursor = Cursors.Hand;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnSettings.ForeColor = Color.White;
            btnSettings.Location = new Point(1250, 12);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(120, 38);
            btnSettings.TabIndex = 6;
            btnSettings.Text = "⚙ Config";
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnMetadata
            // 
            btnMetadata.Anchor = AnchorStyles.Right;
            btnMetadata.BackColor = Color.FromArgb(245, 158, 11);
            btnMetadata.Cursor = Cursors.Hand;
            btnMetadata.FlatAppearance.BorderSize = 0;
            btnMetadata.FlatStyle = FlatStyle.Flat;
            btnMetadata.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnMetadata.ForeColor = Color.White;
            btnMetadata.Location = new Point(1000, 12);
            btnMetadata.Name = "btnMetadata";
            btnMetadata.Size = new Size(160, 38);
            btnMetadata.TabIndex = 7;
            btnMetadata.Text = "🔍 Metadatos";
            btnMetadata.UseVisualStyleBackColor = false;
            btnMetadata.Click += btnMetadata_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(243, 244, 246);
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(listViewSeries, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 2);
            tableLayoutPanel1.Controls.Add(panel3, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
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
            panel1.Controls.Add(groupBoxOpciones);
            panel1.Controls.Add(btnCargarCarpetas);
            panel1.Controls.Add(btnGuardarTodo);
            panel1.Controls.Add(btnSettings);
            panel1.Controls.Add(btnMetadata);
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
            radioCambiar.CheckedChanged += radioCambiar_CheckedChanged;
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
            radioMantener.CheckedChanged += radioMantener_CheckedChanged;
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
            btnGuardar.Click += btnGuardar_Click;
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
            txtCapitulo.KeyPress += txtNumerico_KeyPress;
            txtCapitulo.Validating += txtCapitulo_Validating;
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
            txtTemporada.KeyPress += txtNumerico_KeyPress;
            txtTemporada.Validating += txtTemporada_Validating;
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
            txtTitulo.KeyDown += onClickEnter;
            txtTitulo.Validating += txtTitulo_Validating;
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
            txtTituloEpisodio.KeyDown += onClickEnter;
            txtTituloEpisodio.Validating += txtTituloEpisodio_Validating;
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
            panel3.Location = new Point(3, 821);
            panel3.Name = "panel3";
            panel3.Size = new Size(1602, 34);
            panel3.TabIndex = 5;
            // 
            // btnUndo
            // 
            btnUndo.Anchor = AnchorStyles.Right;
            btnUndo.BackColor = Color.FromArgb(107, 114, 128);
            btnUndo.Cursor = Cursors.Hand;
            btnUndo.Enabled = false;
            btnUndo.FlatAppearance.BorderSize = 0;
            btnUndo.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 85, 99);
            btnUndo.FlatStyle = FlatStyle.Flat;
            btnUndo.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUndo.ForeColor = Color.White;
            btnUndo.Location = new Point(1320, 5);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(85, 26);
            btnUndo.TabIndex = 2;
            btnUndo.Text = "↶ Deshacer";
            btnUndo.UseVisualStyleBackColor = false;
            btnUndo.Click += btnUndo_Click;
            // 
            // btnRedo
            // 
            btnRedo.Anchor = AnchorStyles.Right;
            btnRedo.BackColor = Color.FromArgb(107, 114, 128);
            btnRedo.Cursor = Cursors.Hand;
            btnRedo.Enabled = false;
            btnRedo.FlatAppearance.BorderSize = 0;
            btnRedo.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 85, 99);
            btnRedo.FlatStyle = FlatStyle.Flat;
            btnRedo.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRedo.ForeColor = Color.White;
            btnRedo.Location = new Point(1415, 5);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(85, 26);
            btnRedo.TabIndex = 1;
            btnRedo.Text = "↷ Rehacer";
            btnRedo.UseVisualStyleBackColor = false;
            btnRedo.Click += btnRedo_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(107, 114, 128);
            lblStatus.Location = new Point(15, 10);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(120, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "✓ Listo para trabajar...";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1608, 868);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(900, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Organizador de Capítulos";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBoxOpciones.ResumeLayout(false);
            groupBoxOpciones.PerformLayout();
            panel2.ResumeLayout(false);
            groupBoxDetalles.ResumeLayout(false);
            groupBoxDetalles.PerformLayout();
            ((ISupportInitialize)errorProvider).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            btnMetadata.BringToFront();
            panel1.Controls.SetChildIndex(btnMetadata, 0);
            ResumeLayout(false);
        }



        #endregion

        private System.Windows.Forms.ListView listViewSeries;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnCargarCarpetas;
        private System.Windows.Forms.Button btnGuardarTodo;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnMetadata;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxOpciones;
        private System.Windows.Forms.RadioButton radioCambiar;
        private System.Windows.Forms.RadioButton radioMantener;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxDetalles;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtCapitulo;
        private System.Windows.Forms.TextBox txtTituloEpisodio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTemporada;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Label lblStatus;
    }
}