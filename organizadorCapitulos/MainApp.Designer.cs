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
            listViewSeries = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            btnCargarCarpetas = new Button();
            btnGuardarTodo = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            panel2 = new Panel();
            btnGuardar = new Button();
            txtCapitulo = new TextBox();
            label3 = new Label();
            txtTemporada = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtTitulo = new TextBox();
            radioMantener = new RadioButton();
            radioCambiar = new RadioButton();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // listViewSeries
            // 
            listViewSeries.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listViewSeries.Dock = DockStyle.Fill;
            listViewSeries.FullRowSelect = true;
            listViewSeries.GridLines = true;
            listViewSeries.Location = new Point(3, 48);
            listViewSeries.Name = "listViewSeries";
            listViewSeries.Size = new Size(1602, 717);
            listViewSeries.TabIndex = 0;
            listViewSeries.UseCompatibleStateImageBehavior = false;
            listViewSeries.View = View.Details;
            listViewSeries.SelectedIndexChanged += listViewSeries_SelectedIndexChanged;
            listViewSeries.ColumnClick += listViewSeries_ColumnClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Nombre del archivo";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Ruta completa";
            columnHeader2.Width = 1200;
            // 
            // btnCargarCarpetas
            // 
            btnCargarCarpetas.Anchor = AnchorStyles.Left;
            btnCargarCarpetas.Location = new Point(3, 3);
            btnCargarCarpetas.Name = "btnCargarCarpetas";
            btnCargarCarpetas.Size = new Size(120, 35);
            btnCargarCarpetas.TabIndex = 1;
            btnCargarCarpetas.Text = "Cargar carpetas";
            btnCargarCarpetas.UseVisualStyleBackColor = true;
            btnCargarCarpetas.Click += btnCargarCarpetas_Click;
            // 
            // btnGuardarTodo
            // 
            btnGuardarTodo.Anchor = AnchorStyles.Right;
            btnGuardarTodo.Location = new Point(1465, 3);
            btnGuardarTodo.Name = "btnGuardarTodo";
            btnGuardarTodo.Size = new Size(120, 35);
            btnGuardarTodo.TabIndex = 2;
            btnGuardarTodo.Text = "Guardar todo";
            btnGuardarTodo.UseVisualStyleBackColor = true;
            btnGuardarTodo.Click += btnGuardarTodo_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(listViewSeries, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new Size(1608, 868);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(radioCambiar);
            panel1.Controls.Add(radioMantener);
            panel1.Controls.Add(btnCargarCarpetas);
            panel1.Controls.Add(btnGuardarTodo);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1602, 39);
            panel1.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(txtCapitulo);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtTemporada);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txtTitulo);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 771);
            panel2.Name = "panel2";
            panel2.Size = new Size(1602, 94);
            panel2.TabIndex = 4;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.Location = new Point(1465, 15);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 35);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtCapitulo
            // 
            txtCapitulo.Location = new Point(220, 49);
            txtCapitulo.Name = "txtCapitulo";
            txtCapitulo.Size = new Size(50, 23);
            txtCapitulo.TabIndex = 5;
            txtCapitulo.Text = "1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(160, 52);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 4;
            label3.Text = "Capítulo:";
            // 
            // txtTemporada
            // 
            txtTemporada.Location = new Point(90, 49);
            txtTemporada.Name = "txtTemporada";
            txtTemporada.Size = new Size(50, 23);
            txtTemporada.TabIndex = 3;
            txtTemporada.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 52);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 2;
            label2.Text = "Temporada:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 18);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 1;
            label1.Text = "Título:";
            // 
            // txtTitulo
            // 
            txtTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTitulo.Location = new Point(70, 15);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(1380, 23);
            txtTitulo.TabIndex = 0;
            // 
            // radioMantener
            // 
            radioMantener.AutoSize = true;
            radioMantener.Location = new Point(1064, 10);
            radioMantener.Name = "radioMantener";
            radioMantener.Size = new Size(76, 19);
            radioMantener.TabIndex = 3;
            radioMantener.TabStop = true;
            radioMantener.Text = "Mantener";
            radioMantener.UseVisualStyleBackColor = true;
            // 
            // radioCambiar
            // 
            radioCambiar.AutoSize = true;
            radioCambiar.Location = new Point(1187, 11);
            radioCambiar.Name = "radioCambiar";
            radioCambiar.Size = new Size(70, 19);
            radioCambiar.TabIndex = 4;
            radioCambiar.TabStop = true;
            radioCambiar.Text = "Cambiar";
            radioCambiar.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1608, 868);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(800, 500);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Organizador de Capítulos";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
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
    }
}