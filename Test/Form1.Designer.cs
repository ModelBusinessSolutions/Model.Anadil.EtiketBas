
namespace Test
{
    partial class Form1
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
            this.btnEtiketBas = new System.Windows.Forms.Button();
            this.txtEBGuid = new System.Windows.Forms.TextBox();
            this.txtEBVersiyon = new System.Windows.Forms.TextBox();
            this.txtEBCmdStr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnYaziciSec = new System.Windows.Forms.Button();
            this.txtYSGuid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEtiketBas
            // 
            this.btnEtiketBas.Location = new System.Drawing.Point(19, 117);
            this.btnEtiketBas.Name = "btnEtiketBas";
            this.btnEtiketBas.Size = new System.Drawing.Size(75, 23);
            this.btnEtiketBas.TabIndex = 0;
            this.btnEtiketBas.Text = "Etiket Bas";
            this.btnEtiketBas.UseVisualStyleBackColor = true;
            this.btnEtiketBas.Click += new System.EventHandler(this.btnEtiketBas_Click);
            // 
            // txtEBGuid
            // 
            this.txtEBGuid.Location = new System.Drawing.Point(76, 26);
            this.txtEBGuid.Name = "txtEBGuid";
            this.txtEBGuid.Size = new System.Drawing.Size(309, 20);
            this.txtEBGuid.TabIndex = 1;
            this.txtEBGuid.Text = "67747287-3661-435A-8EC0-C2062951A5B5";
            // 
            // txtEBVersiyon
            // 
            this.txtEBVersiyon.Location = new System.Drawing.Point(76, 52);
            this.txtEBVersiyon.Name = "txtEBVersiyon";
            this.txtEBVersiyon.Size = new System.Drawing.Size(43, 20);
            this.txtEBVersiyon.TabIndex = 2;
            this.txtEBVersiyon.Text = "4";
            // 
            // txtEBCmdStr
            // 
            this.txtEBCmdStr.Location = new System.Drawing.Point(76, 79);
            this.txtEBCmdStr.Name = "txtEBCmdStr";
            this.txtEBCmdStr.Size = new System.Drawing.Size(309, 20);
            this.txtEBCmdStr.TabIndex = 3;
            this.txtEBCmdStr.Text = "Exec frm_etiket_bas @cid=\'KRT\', @sGuid=\'7A843E48-A7C2-4C0D-AE99-FF7A988DE29E\',@se" +
    "rinodan=1,@ref_no=\'331414250\'";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Guid";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Versiyon";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "CmdStr";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEBCmdStr);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnEtiketBas);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEBGuid);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtEBVersiyon);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 146);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Etiket Bas";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnYaziciSec);
            this.groupBox2.Controls.Add(this.txtYSGuid);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 91);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Yazıcı Seç";
            // 
            // btnYaziciSec
            // 
            this.btnYaziciSec.Location = new System.Drawing.Point(19, 62);
            this.btnYaziciSec.Name = "btnYaziciSec";
            this.btnYaziciSec.Size = new System.Drawing.Size(75, 23);
            this.btnYaziciSec.TabIndex = 0;
            this.btnYaziciSec.Text = "Yazıcı Seç";
            this.btnYaziciSec.UseVisualStyleBackColor = true;
            this.btnYaziciSec.Click += new System.EventHandler(this.btnYaziciSec_Click);
            // 
            // txtYSGuid
            // 
            this.txtYSGuid.Location = new System.Drawing.Point(76, 26);
            this.txtYSGuid.Name = "txtYSGuid";
            this.txtYSGuid.Size = new System.Drawing.Size(309, 20);
            this.txtYSGuid.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Guid";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 288);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(125, 318);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(203, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Asenkron";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Asenkron Değil";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 364);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Test Ekranı";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEtiketBas;
        private System.Windows.Forms.TextBox txtEBGuid;
        private System.Windows.Forms.TextBox txtEBVersiyon;
        private System.Windows.Forms.TextBox txtEBCmdStr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnYaziciSec;
        private System.Windows.Forms.TextBox txtYSGuid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

