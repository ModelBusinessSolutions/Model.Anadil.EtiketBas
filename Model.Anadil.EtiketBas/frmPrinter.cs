using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace Model.Anadil.EtiketBas
{
    public partial class frmPrinter : Form
    {
        private string gReportGuid = "";
        public string GReportGuid { get => gReportGuid; set => gReportGuid = value; }
        private DataGridViewCell rightCell;
        static DataTable reportTable;
        bool allPaste;
        public frmPrinter()
        {
            //GReportGuid = pGuid;
            InitializeComponent();
            //this.Text = string.Format("Şablon Adı ({0})", text);
        }

        private void frmPrinter_Load(object sender, EventArgs e)
        {
            reportTable = new DataTable("Table1");// ds.Tables.Add("Table1");
            reportTable.Columns.Add("rowstate", typeof(bool));
            reportTable.Columns.Add("rowid", typeof(int));
            reportTable.Columns.Add("guid", typeof(string));
            reportTable.Columns.Add("sablonAdi", typeof(string));
            reportTable.Columns.Add("yaziciAdi", typeof(string));

            string defaultPrinterName = "";
            //List<string> printerListOrder = new List<string>();
            DataTable printerListOrder = new DataTable("Table1");// ds.Tables.Add("Table1");
            printerListOrder.Columns.Add("yaziciAdi", typeof(string));

            DataRow rowPrinter = printerListOrder.NewRow();
            rowPrinter["yaziciAdi"] = "";
            printerListOrder.Rows.Add(rowPrinter);

            PrinterSettings settings = new PrinterSettings();
            //printerListOrder.Add("");
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter && (defaultPrinterName == "" || defaultPrinterName == null))
                    defaultPrinterName = printer;

                rowPrinter = printerListOrder.NewRow();
                rowPrinter["yaziciAdi"] = printer;
                printerListOrder.Rows.Add(rowPrinter);
                //printerListOrder.Add(printer);
            }

            printerListOrder.DefaultView.Sort = "yaziciAdi ASC";
            yaziciAdi.DataSource = printerListOrder;

            if (GReportGuid != "" && GReportGuid != null)
            {
                SoundPlayer errorSound = new SoundPlayer(DotNetObject.AlwaysRoot + "\\hata.wav");
                errorSound.Play();
                if (Guid.TryParse(GReportGuid, out var newGuid))
                {
                    using (SqlConnection conn = new SqlConnection(Model.Anadil.EtiketBas.DotNetObject.ConStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT report_name FROM USERSDATABASES.dbo.utl_devx_report_templates with(nolock) WHERE (report_id like 'frm_etiket_bas%' OR report_id like 'MK_%' OR report_id like 'MT_%') AND [guid]=@guid", conn))
                        {
                            cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = GReportGuid;
                            //cmd.CommandTimeout = 1000;
                            using (IDataReader rdr = cmd.ExecuteReader())
                            {
                                if (rdr.Read())
                                    if (rdr["report_name"] != null && rdr["report_name"].ToString() != "")
                                    {
                                        DataRow row = reportTable.NewRow();
                                        row["rowid"] = 0;
                                        row["guid"] = GReportGuid;
                                        row["sablonAdi"] = (string)rdr["report_name"];
                                        row["yaziciAdi"] = (string)Registry.CurrentUser.OpenSubKey("Software\\Model\\ReportDesigner\\", false).GetValue(GReportGuid);
                                        reportTable.Rows.Add(row);
                                    }
                                rdr.Close();
                            }
                        }
                        conn.Close();
                    }
                }
            }
            else
            {
                int rowid = 0;
                foreach (var reportGuid in Registry.CurrentUser.OpenSubKey("Software\\Model\\ReportDesigner\\", false).GetValueNames())
                {
                    if (Guid.TryParse(reportGuid, out var newGuid))
                    {
                        using (SqlConnection conn = new SqlConnection(Model.Anadil.EtiketBas.DotNetObject.ConStr))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT report_name FROM USERSDATABASES.dbo.utl_devx_report_templates with(nolock) WHERE (report_id like 'frm_etiket_bas%' OR report_id like 'MK_%' OR report_id like 'MT_%') AND [guid]=@guid", conn))
                            {
                                cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = reportGuid;
                                //cmd.CommandTimeout = 1000;
                                using (IDataReader rdr = cmd.ExecuteReader())
                                {
                                    if (rdr.Read())
                                        if (rdr["report_name"] != null && rdr["report_name"].ToString() != "")
                                        {
                                            DataRow row = reportTable.NewRow();
                                            row["rowid"] = rowid;
                                            row["guid"] = reportGuid;
                                            row["sablonAdi"] = (string)rdr["report_name"];
                                            row["yaziciAdi"] = (string)Registry.CurrentUser.OpenSubKey("Software\\Model\\ReportDesigner\\", false).GetValue(reportGuid);
                                            reportTable.Rows.Add(row);
                                            rowid++;
                                        }
                                    rdr.Close();
                                }
                            }
                            conn.Close();
                        }
                    }
                }
            }
            reportTable.AcceptChanges();
            reportTable.DefaultView.Sort = "sablonAdi ASC";
            dtgridView.DataSource = reportTable;
        }

        private void dtgridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                if (dtgridView.Columns[e.ColumnIndex].Name == "yaziciAdi")
                    rightCell = dtgridView[e.ColumnIndex, e.RowIndex];
            }
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            foreach (DataRow item in (dtgridView.DataSource as DataTable).Select("rowstate=1"))
            {
                string yaziciAdi = "";
                if (item["yaziciAdi"] != DBNull.Value)
                { 
                    yaziciAdi = (string)item["yaziciAdi"];
                }
                SetPrinterName((string)item["guid"], yaziciAdi);
            }
            this.Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            //if (this.GReportGuid != "" && this.GReportGuid != null)
            //{
            //    string sprinterName = DotNetObject.SeciliYaziciAdiGetir(this.GReportGuid);
            //    if (string.IsNullOrEmpty(sprinterName))
            //    {
            //        if (MessageBox.Show("Yazıcı seçmeden işlem yapamazsınız. Çıktı alınmayacak. Devam etmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            //            return;
            //    }
            //}
            this.Visible = false;
            //this.Close();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allPaste = false;
            PasteClipboardValue();
        }

        private void pasteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allPaste = true;
            PasteClipboardValue();
            allPaste = false;
        }

        private void CopyToClipboard()
        {
            if (rightCell == null)
            {
                MessageBox.Show("Lütfen yazıcı kolonundan bir hücre seçin.", "Kopyala", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataObject dataObj = new DataObject(DataFormats.UnicodeText, rightCell.EditedFormattedValue);
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void PasteClipboardValue()
        {
            if (rightCell == null)
            {
                MessageBox.Show("Lütfen yazıcı kolonundan bir hücre seçin.", "Yapıştır", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            rightCell.Value = Clipboard.GetText();

            if (allPaste)
            {
                foreach (DataRowView item in (dtgridView.DataSource as DataTable).DefaultView)
                {
                    item["yaziciAdi"] = rightCell.Value;
                    item["rowstate"] = 1;
                }
                (dtgridView.DataSource as DataTable).AcceptChanges();
                dtgridView.Update();
            }
        }

        private void SetPrinterName(string guid, string printerName)
        {
            try
            {
                if (printerName == null)
                {
                    printerName = "";
                }
                RegistryKey registryKey = Registry.CurrentUser;

                RegistryKey subKey = registryKey.OpenSubKey("SOFTWARE\\MODEL\\ReportDesigner", true);

                if (subKey != null)
                {
                    subKey.SetValue(guid, printerName);
                }
                else
                {
                    MessageBox.Show("CurrentUser\\SOFTWARE\\MODEL\\ReportDesigner bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtgridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            (dtgridView.DataSource as DataTable).DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
            (dtgridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("sablonAdi like '%{0}%' AND yaziciAdi like '%{1}%'",
                txtSablonAdi.Text == "Aranacak şablon adı giriniz..." ? "" : txtSablonAdi.Text,
                txtYaziciAdi.Text == "Aranacak yazıcı adı giriniz..." ? "" : txtYaziciAdi.Text);
        }

        private void txtSablonAdi_Enter(object sender, EventArgs e)
        {
            if (txtSablonAdi.Text == "Aranacak şablon adı giriniz...")
            {
                txtSablonAdi.Text = "";
                txtSablonAdi.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtSablonAdi_Leave(object sender, EventArgs e)
        {
            if (txtSablonAdi.Text == "")
            {
                txtSablonAdi.Text = "Aranacak şablon adı giriniz...";
                txtSablonAdi.ForeColor = System.Drawing.Color.DarkGray;
            }
        }

        private void txtYaziciAdi_Enter(object sender, EventArgs e)
        {
            if (txtYaziciAdi.Text == "Aranacak yazıcı adı giriniz...")
            {
                txtYaziciAdi.Text = "";
                txtYaziciAdi.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtYaziciAdi_Leave(object sender, EventArgs e)
        {
            if (txtYaziciAdi.Text == "")
            {
                txtYaziciAdi.Text = "Aranacak yazıcı adı giriniz...";
                txtYaziciAdi.ForeColor = System.Drawing.Color.DarkGray;
            }
        }

        private void dtgridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!allPaste && e.RowIndex > -1)
            {
                dtgridView["rowstate", e.RowIndex].Value = true;
            }
        }
    }

    public class Report
    {
        public string guid { get; set; }
        public string sablonAdi { get; set; }
        public string yaziciAdi { get; set; }

    }
}
