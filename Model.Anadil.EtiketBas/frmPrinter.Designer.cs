
namespace Model.Anadil.EtiketBas
{
    partial class frmPrinter
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnTamam = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.dtgridView = new System.Windows.Forms.DataGridView();
            this.rowstate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sablonAdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yaziciAdi = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSablonAdi = new System.Windows.Forms.TextBox();
            this.btnFiltrele = new System.Windows.Forms.Button();
            this.txtYaziciAdi = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTamam
            // 
            this.btnTamam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTamam.Location = new System.Drawing.Point(410, 492);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(100, 30);
            this.btnTamam.TabIndex = 4;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.UseVisualStyleBackColor = true;
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
            // 
            // btnIptal
            // 
            this.btnIptal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnIptal.Location = new System.Drawing.Point(516, 492);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(100, 30);
            this.btnIptal.TabIndex = 5;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // dtgridView
            // 
            this.dtgridView.AllowUserToAddRows = false;
            this.dtgridView.AllowUserToDeleteRows = false;
            this.dtgridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rowstate,
            this.rowid,
            this.guid,
            this.sablonAdi,
            this.yaziciAdi});
            this.dtgridView.Location = new System.Drawing.Point(12, 44);
            this.dtgridView.MultiSelect = false;
            this.dtgridView.Name = "dtgridView";
            this.dtgridView.RowHeadersVisible = false;
            this.dtgridView.RowHeadersWidth = 10;
            this.dtgridView.Size = new System.Drawing.Size(604, 442);
            this.dtgridView.TabIndex = 0;
            this.dtgridView.TabStop = false;
            this.dtgridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgridView_CellMouseDown);
            this.dtgridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgridView_CellValueChanged);
            this.dtgridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dtgridView_DataError);
            // 
            // rowstate
            // 
            this.rowstate.DataPropertyName = "rowstate";
            this.rowstate.HeaderText = "rowstate";
            this.rowstate.Name = "rowstate";
            this.rowstate.Visible = false;
            // 
            // rowid
            // 
            this.rowid.DataPropertyName = "rowid";
            this.rowid.HeaderText = "rowid";
            this.rowid.Name = "rowid";
            this.rowid.Visible = false;
            // 
            // guid
            // 
            this.guid.DataPropertyName = "guid";
            this.guid.HeaderText = "guid";
            this.guid.Name = "guid";
            this.guid.Visible = false;
            // 
            // sablonAdi
            // 
            this.sablonAdi.DataPropertyName = "sablonAdi";
            this.sablonAdi.HeaderText = "Şablon Adı";
            this.sablonAdi.Name = "sablonAdi";
            this.sablonAdi.ReadOnly = true;
            // 
            // yaziciAdi
            // 
            this.yaziciAdi.ContextMenuStrip = this.contextMenuStrip1;
            this.yaziciAdi.DisplayMember = "yaziciAdi";
            this.yaziciAdi.ValueMember = "yaziciAdi";
            this.yaziciAdi.DataPropertyName = "yaziciAdi";
            this.yaziciAdi.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.yaziciAdi.HeaderText = "Seçili Yazıcı";
            this.yaziciAdi.Name = "yaziciAdi";
            this.yaziciAdi.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.yaziciAdi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.yaziciAdi.Width = 470;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.pasteAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 70);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.copyToolStripMenuItem.Text = "Kopyala";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pasteToolStripMenuItem.Text = "Yapıştır";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // pasteAllToolStripMenuItem
            // 
            this.pasteAllToolStripMenuItem.Name = "pasteAllToolStripMenuItem";
            this.pasteAllToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.pasteAllToolStripMenuItem.Text = "Tümüne Yapıştır";
            this.pasteAllToolStripMenuItem.Click += new System.EventHandler(this.pasteAllToolStripMenuItem_Click);
            // 
            // txtSablonAdi
            // 
            this.txtSablonAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSablonAdi.ForeColor = System.Drawing.Color.DarkGray;
            this.txtSablonAdi.Location = new System.Drawing.Point(12, 10);
            this.txtSablonAdi.Name = "txtSablonAdi";
            this.txtSablonAdi.Size = new System.Drawing.Size(246, 26);
            this.txtSablonAdi.TabIndex = 1;
            this.txtSablonAdi.Text = "Aranacak şablon adı giriniz...";
            this.txtSablonAdi.Enter += new System.EventHandler(this.txtSablonAdi_Enter);
            this.txtSablonAdi.Leave += new System.EventHandler(this.txtSablonAdi_Leave);
            // 
            // btnFiltrele
            // 
            this.btnFiltrele.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnFiltrele.Location = new System.Drawing.Point(516, 8);
            this.btnFiltrele.Name = "btnFiltrele";
            this.btnFiltrele.Size = new System.Drawing.Size(100, 30);
            this.btnFiltrele.TabIndex = 3;
            this.btnFiltrele.Text = "Filtrele";
            this.btnFiltrele.UseVisualStyleBackColor = true;
            this.btnFiltrele.Click += new System.EventHandler(this.btnFiltrele_Click);
            // 
            // txtYaziciAdi
            // 
            this.txtYaziciAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtYaziciAdi.ForeColor = System.Drawing.Color.DarkGray;
            this.txtYaziciAdi.Location = new System.Drawing.Point(264, 10);
            this.txtYaziciAdi.Name = "txtYaziciAdi";
            this.txtYaziciAdi.Size = new System.Drawing.Size(246, 26);
            this.txtYaziciAdi.TabIndex = 2;
            this.txtYaziciAdi.Text = "Aranacak yazıcı adı giriniz...";
            this.txtYaziciAdi.Enter += new System.EventHandler(this.txtYaziciAdi_Enter);
            this.txtYaziciAdi.Leave += new System.EventHandler(this.txtYaziciAdi_Leave);
            // 
            // frmPrinter
            // 
            this.AcceptButton = this.btnFiltrele;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 526);
            this.Controls.Add(this.txtYaziciAdi);
            this.Controls.Add(this.btnFiltrele);
            this.Controls.Add(this.txtSablonAdi);
            this.Controls.Add(this.dtgridView);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnTamam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmPrinter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Etiket Şablonları";
            this.Load += new System.EventHandler(this.frmPrinter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTamam;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.DataGridView dtgridView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteAllToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowid;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowstate;
        private System.Windows.Forms.DataGridViewTextBoxColumn sablonAdi;
        private System.Windows.Forms.DataGridViewComboBoxColumn yaziciAdi;
        private System.Windows.Forms.TextBox txtSablonAdi;
        private System.Windows.Forms.Button btnFiltrele;
        private System.Windows.Forms.TextBox txtYaziciAdi;
    }
}