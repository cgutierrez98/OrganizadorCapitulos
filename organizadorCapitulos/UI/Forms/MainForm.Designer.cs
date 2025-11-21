using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Font = System.Drawing.Font;

namespace organizadorCapitulos
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

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            listViewSeries = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            btnCargarCarpetas = new Button();
            btnGuardarTodo = new Button();
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
            listViewSeries.BackColor = Color.White;
            listViewSeries.BorderStyle = BorderStyle.FixedSingle;
            listViewSeries.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listViewSeries.Dock = DockStyle.Fill;
            listViewSeries.Font = new Font("Segoe UI", 9.75F);
            listViewSeries.FullRowSelect = true;
            listViewSeries.GridLines = true;
            listViewSeries.Location = new Point(10, 70);
            listViewSeries.Margin = new Padding(10);
            listViewSeries.Name = "listViewSeries";
            listViewSeries.Size = new Size(1588, 598);
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
            // btnCargarCarpetas
            // 
            btnCargarCarpetas.Anchor = AnchorStyles.Left;
            btnCargarCarpetas.BackColor = Color.SteelBlue;
            btnCargarCarpetas.FlatAppearance.BorderSize = 0;
            btnCargarCarpetas.FlatStyle = FlatStyle.Flat;
            btnCargarCarpetas.Font = new Font("Segoe UI", 9.75F);
            btnCargarCarpetas.ForeColor = Color.White;
            btnCargarCarpetas.ImageAlign = ContentAlignment.MiddleLeft;
            btnCargarCarpetas.Location = new Point(10, 12);
            btnCargarCarpetas.Name = "btnCargarCarpetas";
            btnCargarCarpetas.Size = new Size(150, 35);
            btnCargarCarpetas.TabIndex = 1;
            btnCargarCarpetas.Text = "  Cargar carpetas";
            btnCargarCarpetas.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCargarCarpetas.UseVisualStyleBackColor = false;
            btnCargarCarpetas.Click += btnCargarCarpetas_Click;
            // 
            // btnGuardarTodo
            // 
            btnGuardarTodo.Anchor = AnchorStyles.Right;
            btnGuardarTodo.BackColor = Color.SeaGreen;
            btnGuardarTodo.FlatAppearance.BorderSize = 0;
            btnGuardarTodo.FlatStyle = FlatStyle.Flat;
            btnGuardarTodo.Font = new Font("Segoe UI", 9.75F);
            btnGuardarTodo.ForeColor = Color.White;
            btnGuardarTodo.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardarTodo.Location = new Point(1442, 12);
            btnGuardarTodo.Name = "btnGuardarTodo";
            btnGuardarTodo.Size = new Size(150, 35);
            btnGuardarTodo.TabIndex = 2;
            btnGuardarTodo.Text = "  Guardar todo";
            btnGuardarTodo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardarTodo.UseVisualStyleBackColor = false;
            btnGuardarTodo.Click += btnGuardarTodo_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.Control;
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
            panel1.BackColor = Color.White;
            panel1.Controls.Add(groupBoxOpciones);
            panel1.Controls.Add(btnCargarCarpetas);
            panel1.Controls.Add(btnGuardarTodo);
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
            groupBoxOpciones.Font = new Font("Segoe UI", 9F);
            groupBoxOpciones.Location = new Point(650, 5);
            groupBoxOpciones.Name = "groupBoxOpciones";
            groupBoxOpciones.Size = new Size(300, 50);
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
            panel2.BackColor = Color.White;
            panel2.Controls.Add(groupBoxDetalles);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(10, 678);
            panel2.Margin = new Padding(10);
            panel2.Name = "panel2";
            panel2.Size = new Size(1588, 130);
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
            groupBoxDetalles.Font = new Font("Segoe UI", 9.75F);
            groupBoxDetalles.Location = new Point(0, 0);
            groupBoxDetalles.Name = "groupBoxDetalles";
            groupBoxDetalles.Size = new Size(1588, 130);
            groupBoxDetalles.TabIndex = 7;
            groupBoxDetalles.TabStop = false;
            groupBoxDetalles.Text = "Detalles del capítulo seleccionado";
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.BackColor = Color.DodgerBlue;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.Location = new Point(1420, 80);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(150, 35);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "  Guardar cambios";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtCapitulo
            // 
            txtCapitulo.BorderStyle = BorderStyle.FixedSingle;
            txtCapitulo.Location = new Point(240, 85);
            txtCapitulo.MaxLength = 3;
            txtCapitulo.Name = "txtCapitulo";
            txtCapitulo.Size = new Size(60, 25);
            txtCapitulo.TabIndex = 5;
            txtCapitulo.Text = "1";
            txtCapitulo.KeyPress += txtNumerico_KeyPress;
            txtCapitulo.Validating += txtCapitulo_Validating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(180, 88);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 4;
            label3.Text = "Capítulo:";
            // 
            // txtTemporada
            // 
            txtTemporada.BorderStyle = BorderStyle.FixedSingle;
            txtTemporada.Location = new Point(90, 85);
            txtTemporada.MaxLength = 2;
            txtTemporada.Name = "txtTemporada";
            txtTemporada.Size = new Size(60, 25);
            txtTemporada.TabIndex = 3;
            txtTemporada.Text = "1";
            txtTemporada.KeyPress += txtNumerico_KeyPress;
            txtTemporada.Validating += txtTemporada_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 88);
            label2.Name = "label2";
            label2.Size = new Size(78, 17);
            label2.TabIndex = 2;
            label2.Text = "Temporada:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 40);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 1;
            label1.Text = "Título:";
            // 
            // txtTitulo
            // 
            txtTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTitulo.BorderStyle = BorderStyle.FixedSingle;
            txtTitulo.Location = new Point(90, 37);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(1480, 25);
            txtTitulo.TabIndex = 0;
            txtTitulo.KeyDown += onClickEnter;
            txtTitulo.Validating += txtTitulo_Validating;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
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
            btnUndo.BackColor = Color.Gray;
            btnUndo.Enabled = false;
            btnUndo.FlatAppearance.BorderSize = 0;
            btnUndo.FlatStyle = FlatStyle.Flat;
            btnUndo.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUndo.ForeColor = Color.White;
            btnUndo.Location = new Point(1320, 5);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(80, 25);
            btnUndo.TabIndex = 2;
            btnUndo.Text = "Deshacer";
            btnUndo.UseVisualStyleBackColor = false;
            btnUndo.Click += btnUndo_Click;
            // 
            // btnRedo
            // 
            btnRedo.Anchor = AnchorStyles.Right;
            btnRedo.BackColor = Color.Gray;
            btnRedo.Enabled = false;
            btnRedo.FlatAppearance.BorderSize = 0;
            btnRedo.FlatStyle = FlatStyle.Flat;
            btnRedo.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRedo.ForeColor = Color.White;
            btnRedo.Location = new Point(1410, 5);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(80, 25);
            btnRedo.TabIndex = 1;
            btnRedo.Text = "Rehacer";
            btnRedo.UseVisualStyleBackColor = false;
            btnRedo.Click += btnRedo_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(10, 10);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(120, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Listo para trabajar...";
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
            ResumeLayout(false);
        }

        private ListView listViewSeries;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button btnCargarCarpetas;
        private Button btnGuardarTodo;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
        private TextBox txtTitulo;
        private Label label1;
        private TextBox txtCapitulo;
        private Label label3;
        private TextBox txtTemporada;
        private Label label2;
        private Button btnGuardar;
        private RadioButton radioCambiar;
        private RadioButton radioMantener;
        private GroupBox groupBoxOpciones;
        private GroupBox groupBoxDetalles;
        private ErrorProvider errorProvider;
        private Panel panel3;
        private Label lblStatus;
        private Button btnRedo;
        private Button btnUndo;
    }
}