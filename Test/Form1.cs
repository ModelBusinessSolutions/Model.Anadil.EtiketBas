using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEtiketBas_Click(object sender, EventArgs e)
        {
            if (txtEBGuid.Text == "" || txtEBVersiyon.Text == "" || txtEBCmdStr.Text == "")
            {
                MessageBox.Show("Lütfen tüm parametrelere değerleri giriniz.");
                return;
            }

            Model.Anadil.EtiketBas.DotNetObject dotNetObject = new Model.Anadil.EtiketBas.DotNetObject();
            List<string> param = new List<string>();
            //param.Add(@"C:\Always\RaporSablonlari\CreateDonNetObjectDeneme\ReportColumns_05DA65EC-25A3-453E-971F-27E12DA7BF0C.xml");
            //Exec frm_ban_hesap_hareketleri 1320, 'ULS'
            param.Add("TPERHANI");
            param.Add("TPERHANI");
            param.Add("MODEL2022");
            param.Add("sa");
            param.Add("123");
            param.Add(txtEBGuid.Text);
            param.Add(txtEBVersiyon.Text);
            param.Add(txtEBCmdStr.Text);
            param.Add("program_no111111");
            //param.Add(":K:filtre_tanimi is null or filtre_tanimi = ''");
            //param.Add(":K:satir_belge_no is null or satir_belge_no = ''");
            //param.Add(":K:fis_turu is null or fis_turu = ''");
            List<string> ret = dotNetObject.CallMethod("EtiketBas", param).ToList();
            dotNetObject.Dispose();
            if (ret.Count > 1)
            {
                if (ret[0] == "FALSE" && ret[1].StartsWith(":H:"))
                {
                    MessageBox.Show(ret[1].Replace(":H:", ""), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnYaziciSec_Click(object sender, EventArgs e)
        {
            Model.Anadil.EtiketBas.DotNetObject dotNetObject = new Model.Anadil.EtiketBas.DotNetObject();
            List<string> param = new List<string>();
            param.Add("TPERHANI");
            param.Add("TPERHANI");
            param.Add("ULUSAL2021TEST");
            param.Add("sa");
            param.Add("123");
            param.Add(txtYSGuid.Text);
            List<string> ret = dotNetObject.CallMethod("YaziciSec", param).ToList();
            dotNetObject.Dispose();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Model.Anadil.EtiketBas.DotNetObject dotNetObject = new Model.Anadil.EtiketBas.DotNetObject();
                List<string> param = new List<string>();
                //param.Add(@"C:\Always\RaporSablonlari\CreateDonNetObjectDeneme\ReportColumns_05DA65EC-25A3-453E-971F-27E12DA7BF0C.xml");
                //Exec frm_ban_hesap_hareketleri 1320, 'ULS'
                //param.Add("TPERHANI");
                //param.Add("TPERHANI");
                //param.Add("ULUSAL2021TEST");
                //param.Add("sa");
                //param.Add("123");
                param.Add(textBox1.Text);
                //param.Add(":K:filtre_tanimi is null or filtre_tanimi = ''");
                //param.Add(":K:satir_belge_no is null or satir_belge_no = ''");
                //param.Add(":K:fis_turu is null or fis_turu = ''");
                List<string> ret = dotNetObject.CallMethod("EtiketBasSleep", param).ToList();
                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Model.Anadil.EtiketBas.DotNetObject dotNetObject = new Model.Anadil.EtiketBas.DotNetObject();
                List<string> param = new List<string>();
                //param.Add(@"C:\Always\RaporSablonlari\CreateDonNetObjectDeneme\ReportColumns_05DA65EC-25A3-453E-971F-27E12DA7BF0C.xml");
                //Exec frm_ban_hesap_hareketleri 1320, 'ULS'
                //param.Add("TPERHANI");
                //param.Add("TPERHANI");
                //param.Add("ULUSAL2021TEST");
                //param.Add("sa");
                //param.Add("123");
                param.Add(textBox2.Text);
                //param.Add(":K:filtre_tanimi is null or filtre_tanimi = ''");
                //param.Add(":K:satir_belge_no is null or satir_belge_no = ''");
                //param.Add(":K:fis_turu is null or fis_turu = ''");
                List<string> ret = dotNetObject.CallMethod("EtiketBasSleep", param).ToList();
                textBox2.Text = "";
                textBox2.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
