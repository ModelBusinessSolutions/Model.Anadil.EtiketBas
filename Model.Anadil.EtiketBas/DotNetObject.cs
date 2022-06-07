using DevExpress.XtraReports.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Model.Anadil.EtiketBas
{
    public class DotNetObject : Model.Anadil.Interop.IDotNetObject
    {
        private Model.Anadil.Interop.DotNetObject netobj;
        private string MesajHata = ":H:";//HATA
        private string MesajUyari = ":U:";//UYARI
        //private string MesajBilgi = ":B:";//BİLGİ
        static string SQLServerIp, SQLServerName, SQLDatabase, SQLUser, SQLPassword;

        Queue queue;
        int queueCount = 0;
        bool _gueueInit;
        bool close;

        Form frmInvoke;

        private static string myConStr = "";
        public static string ConStr
        {
            get
            {
                if (SQLServerIp == "" || SQLServerIp == null)
                {
                    SQLServerIp = SQLServerName;
                }

                myConStr = String.Format("Server={0};Initial Catalog={1};User Id={2};Password={3}", SQLServerIp, SQLDatabase, SQLUser, SQLPassword);
                return myConStr;
            }
        }

        private static string myAlwaysRoot = null;
        public static string AlwaysRoot
        {
            get
            {
                if (myAlwaysRoot != null)
                    return myAlwaysRoot;

                RegistryKey root = (SystemInformation.TerminalServerSession ? Registry.CurrentUser : Registry.LocalMachine);
                RegistryKey key = null;
                try
                {
                    key = root.OpenSubKey("Software\\Model\\Always\\InstalledSystems", false);
                    if (key == null)
                    {
                        MessageBox.Show("Software\\Model\\Always\\InstalledSystems not found. Probably, Always is not installed");
                        throw new ApplicationException("Software\\Model\\Always\\InstalledSystems not found. Probably, Always is not installed");
                    }
                }
                catch (Exception se)
                {
                    MessageBox.Show("Cannot access registry to find Always root. Please check your security settings");
                    throw se;
                }
                try
                {
                    myAlwaysRoot = (string)key.GetValue("%AlwaysRoot%");
                    if (myAlwaysRoot == null)
                    {
                        MessageBox.Show("%AlwaysRoot% not found.");
                        throw new ApplicationException("%AlwaysRoot% not found.");
                    }
                    // The path must end with \\
                    if (!myAlwaysRoot.EndsWith("\\"))
                        myAlwaysRoot = myAlwaysRoot + "\\";
                    return myAlwaysRoot;
                }
                finally
                {
                    key.Close();
                }

            }
        }

        private static string myErpKullaniciAdi = "";
        public static string ErpKullaniciAdi
        {
            get
            {
                myErpKullaniciAdi = (string)Registry.CurrentUser.OpenSubKey("Software\\Model\\Always\\CurrentUser\\", false).GetValue("UserName");
                return myErpKullaniciAdi;
            }
        }

        public bool Create(Model.Anadil.Interop.DotNetObject netobj)
        {
            Trace.WriteLine("Model.Anadil.Interop.DotNetObject NESNESİ OLUŞTURULDU YENİ!!!");
            this.netobj = netobj;
            return true;
        }

        public void Dispose()
        {
            this.netobj = null;
        }

        public IList<string> CallMethod(string method, IList<string> parameters)
        {
            bool methodCheck = false;//method.Equals tan en az birine girmesi gerekmektedir. Eğer girmiyorsa o methotta sıkıntı var demektir. Bu sebeple bu tarz methodlar yakalansın diye eklendi.
            IList<string> ret = new List<string>();

            try
            {
                #region Queue
                if (method.Equals("QueueInit"))
                {
                    methodCheck = true;
                    QueueInit();
                    ret.Add("TRUE");
                }

                if (method.Equals("QueueAdd"))
                {
                    methodCheck = true;
                    ret = QueueAdd(parameters.ToList());
                }

                if (method.Equals("QueueCount"))
                {
                    methodCheck = true;
                    ret = QueueCount(parameters.ToList());
                }

                if (method.Equals("QueueClose"))
                {
                    methodCheck = true;
                    ret = QueueClose();
                }
                #endregion

                if (method.Equals("EtiketBas"))
                {
                    methodCheck = true;
                    ret = EtiketBas(parameters);
                }

                if (method.Equals("YaziciSec"))
                {
                    methodCheck = true;
                    ret = YaziciSec(parameters);
                }

                #region return
                if (ret.Count == 0 || !methodCheck)
                {
                    ret.Add("FALSE");
                    ret.Add(MesajHata + "Çalıştırılan parametreyle ilgili sistemsel sıkıntı bulunmaktadır. Lütfen sistem yöneticiniz ile görüşünüz.");
                    ret.Add(MesajHata + "CALLMETHOD = " + method);
                }

                ret.Add("C1789FFB-9CB3-4506-B63B-0E741CE9F68B_FINISH");
                for (int i = 0; i < ret.Count; i++)
                {
                    if (ret[i].Length >= 250)
                    {
                        ret[i] = ret[i].Substring(0, 250);
                    }
                }
                return ret;
                #endregion
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                #region return
                ret.Add("FALSE");
                ret.Add(MesajHata + ex.Message);
                ret.Add(MesajHata + "Sistem hatası. Lütfen yetkiliyle görüşüp tanımlarınızı kontrol ettiriniz.");
                ret.Add("C1789FFB-9CB3-4506-B63B-0E741CE9F68B_FINISH");
                for (int i = 0; i < ret.Count; i++)
                {
                    if (ret[i].Length >= 250)
                    {
                        ret[i] = ret[i].Substring(0, 250);
                    }
                }
                return ret;
                #endregion
            }
        }

        // CallMethod
        void QueueInit()
        {
            frmInvoke = new Form();
            frmInvoke.Visible = true;
            frmInvoke.CreateControl();
            frmInvoke.Visible = false;
            _gueueInit = true;
            this.queue = new Queue();
            var t = new Thread(() =>
            {
                int i = 0;
                while (true)
                {
                    i++;
                    //File.AppendAllText(@"C:\Always\Etiket\log.log", "INFO:2021-03-18 17:51:01Z:RS32:QUEUE" + i.ToString() + "\r\n");
                    Thread.Sleep(100);
                    if (this.close)
                    {
                        break;
                    }
                    if (this.queue.Any())
                    {
                        List<string> prm = this.queue.Pop().parameters;
                        //File.AppendAllText(@"C:\Always\Etiket\log.log", "INFO:2021-03-18 17:51:01Z:RS32:PARAM:" + prm[7] + "\r\n");
                        EtiketBas(prm);
                        if (queueCount > 0)
                            queueCount--;
                    }
                }
            });
            t.Start();
        }

        public IList<string> QueueClose()
        {
            IList<string> ret = new List<string>();
            this.close = true;
            ret.Add("TRUE");
            return ret;
        }

        public IList<string> QueueAdd(List<string> param)
        {
            IList<string> ret = new List<string>();
            this.queue.Add(new PrintItem(param));
            queueCount++;
            ret.Add("TRUE");
            return ret;
        }

        public IList<string> QueueCount(List<string> param)
        {
            IList<string> ret = new List<string>();
            ret.Add(queueCount.ToString());
            return ret;
        }

        /// <summary>
        /// Akabinde belirtilen şablonun kullanıcının belirlemiş olduğu öndeğer yazıcıdan çıktı alması sağlanmaktadır.<br/>
        /// :K: ile başlayacak parametrelerde eğer verilen kriter sonucunda bir kayıt gelirse mesaj çıkartılır ve çıktı alınmaz.<br/>
        /// Örnek: ":K:filtre_tanimi is null or filtre_tanimi = ''" şeklinde gönderim yapılırsa eğer dönen datatable da filtre_tanimi buna uygun bir kayıt varsa; <br/>
        /// "Çıktı alırken filtre_tanimi alanı boş bırakılamaz." şeklinde messagebox çıkartılacaktır. filtre_tanimi alanını :K: dan sonra yakalanan ilk boşluğa kadar ki değerler alınarak elde ediliyor.
        /// </summary>
        /// <param name="parameters">parameters[0] = pGuid, parameters[1] = pVersiyon, parameters[2] = pCmdStr,parameter[3] = ":K:kriter sorgusu"</param>
        /// <returns>
        /// <b>Başarılı, Yazıcı seçilmiş    :</b>ret[0]="TRUE"
        /// <br/>
        /// <b>Başarılı, Yazıcı seçilmemiş  :</b>ret[0]="TRUE",   ret[1]=":U:Yazıcı seçimi yapmalısınız."
        /// <br/>
        /// <b>Hatalı                       :</b>ret[0]="FALSE",  ret[1]=":H:HataAçıklaması"
        /// </returns>
        public IList<string> EtiketBas(IList<string> parameters)
        {
            IList<string> ret = new List<string>();
            string layoutFilePath = "";
            try
            {
                SQLServerIp = parameters[0];
                SQLServerName = parameters[1];
                SQLDatabase = parameters[2];
                SQLUser = parameters[3];
                SQLPassword = parameters[4];
                string pGuid = parameters[5], pVersiyon = parameters[6], pCmdStr = parameters[7];
                string pProgramNo = parameters[8];
                List<string> pCheckList = parameters.Where(x => x.StartsWith(":K:")).Select(x => x.Remove(0, 3)).ToList();

                string printerName = SeciliYaziciAdiGetir(pGuid);
                if (printerName == "" || printerName == null)
                {
                    List<string> param = new List<string>();
                    param.Add(SQLServerIp);
                    param.Add(SQLServerName);
                    param.Add(SQLDatabase);
                    param.Add(SQLUser);
                    param.Add(SQLPassword);
                    param.Add(pGuid);
                    YaziciSec(param);
                    printerName = SeciliYaziciAdiGetir(pGuid);
                }
                if (printerName != "" && printerName != null)//Printer seçilmemişse bir işlem yapmıyoruz.
                {
                    while (GetPrintQueuesNumberOfJobs(printerName) > 0)
                    {
                        System.Threading.Thread.Sleep(100);
                        //Trace.WriteLine("BEKLIYOR...");
                        //EtiketLog(pCmdStr, "BEKLIYOR", Guid.NewGuid().ToString(), "1");
                    }

                    layoutFilePath = AlwaysRoot + "RaporSablonlari\\" + pGuid + "_v" + pVersiyon + ".repx"; //Gelen parametreler ile ilgili şablonun dizinde varlığını kontrol etmek için en başta pathi oluşturuyoruz.
                    using (SqlConnection conn = new SqlConnection(ConStr))
                    {
                        conn.Open();
                        if (!File.Exists(layoutFilePath)) //Eğer dosya varsa tekrardan report_file selectinin çekmeye gerek yok. 4,5MB lara ulaşabiliyor. Bu sebeple gereksiz data trafiği oluşması engelleniyor.
                        {
                            //Eğer dosya dizinde yoksa veritabanından report_file çekilip dosya olarak makineye kayıt edilmesi sağlanıyor.
                            MemoryStream msLayout = new MemoryStream();
                            using (SqlCommand cmd = new SqlCommand("SELECT report_file,versiyon FROM USERSDATABASES.dbo.utl_devx_report_templates with(nolock) WHERE [guid]=@guid", conn))
                            {
                                cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = pGuid;
                                cmd.CommandTimeout = 1000;

                                using (IDataReader rdr = cmd.ExecuteReader())
                                {
                                    if (rdr.Read())
                                        if (rdr["report_file"] != null && rdr["report_file"].ToString() != "")
                                        {
                                            layoutFilePath = AlwaysRoot + "RaporSablonlari\\" + pGuid + "_v" + rdr["versiyon"].ToString() + ".repx";
                                            System.IO.File.WriteAllBytes(layoutFilePath, Encoding.UTF8.GetBytes((string)rdr["report_file"]));
                                        }
                                    rdr.Close();
                                }
                            }
                        }
                        using (XtraReport res_report = XtraReport.FromFile(layoutFilePath, true))
                        {
                            using (SqlDataAdapter daData = new SqlDataAdapter(pCmdStr, conn))
                            {
                                using (DataSet dsData = new DataSet("Db"))
                                {
                                    using (DataTable dtData = new DataTable("Table"))
                                    {
                                        dsData.Tables.Add(dtData);
                                        daData.Fill(dtData);
                                        if (pCheckList.Count > 0)
                                        {
                                            string retStr = DataCheck(res_report, dtData, pCheckList);
                                            if (retStr != "")
                                            {
                                                ret.Add("FALSE");
                                                ret.Add(retStr);
                                                return ret;
                                            }
                                        }
                                        EtiketLog(pCmdStr, pProgramNo, pGuid, pVersiyon);
                                        res_report.DataAdapter = daData;
                                        res_report.DataSource = dsData;
                                        res_report.DataMember = "Table";
                                        res_report.PrinterName = printerName;
                                        res_report.DisplayName = res_report.DisplayName + "_" + Guid.NewGuid().ToString();
                                        res_report.ShowPrintStatusDialog = false;

                                        try
                                        {
                                            if (System.IO.Directory.Exists(AlwaysRoot + "RaporSablonlari\\PDF\\"))
                                            {
                                                string pdfFilePath = AlwaysRoot + "RaporSablonlari\\PDF\\" + res_report.DisplayName + ".pdf";
                                                res_report.ExportToPdf(pdfFilePath);
                                                Trace.WriteLine("UYARI!!! : Şablon PDF olarak kayıt edildi. Etiket basma hızının düşmemesi için en yakın zamanda RaporSablonlari altındaki PDF klasörünü siliniz.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Trace.WriteLine("PDF olarak kayıt edilemedi : " + ex.Message);
                                        }
                                        res_report.Print();

                                    }
                                }
                            }
                        }
                        conn.Close();
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    ret.Add("TRUE");
                }
                else
                {
                    ret.Add("FALSE");
                    ret.Add(MesajUyari + "Yazıcı seçimi yapmalısınız.");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    //bir şekilde yukarıdan hata fırlatılırsa mevcut şablonda sorun var diye düşünülüp ilgili .repx dosyası dizinde siliniyor ki tekrardan veritabanındaki güncel hali kayıt edilsin.
                    if (File.Exists(layoutFilePath))
                    {
                        System.IO.File.Delete(layoutFilePath);
                    }
                }
                catch (Exception ex1)
                {
                    Trace.WriteLine("Dosya silerken : " + ex1.Message);
                }

                ret.Add("FALSE");
                ret.Add(MesajHata + ex.Message);
            }
            return ret;
        }

        public static int GetPrintQueuesNumberOfJobs(string printer)
        {
            int numberOfJobs = 0;
            //Trace.WriteLine("Utils.cs : GetPrintQueuesNumberOfJobs : Printer : " + printer);
            try
            {
                System.Printing.PrintServer ps = new System.Printing.PrintServer(printer);
                System.Printing.PrintQueueCollection queues = ps.GetPrintQueues();
                foreach (System.Printing.PrintQueue queue in queues)
                {
                    if (queue.Name != printer)
                    {
                        continue;
                    }
                    numberOfJobs += queue.NumberOfJobs;
                    /*
                    Trace.WriteLine("Utils.cs : GetPrintQueuesNumberOfJobs : "
                        + "Queue Name : " + queue.Name + " : "
                        + "NumberOfJobs : " + queue.NumberOfJobs.ToString());
                    */
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Utils.cs : GetPrintQueuesNumberOfJobs : Exception : " + ex.Message); ;
            }
            return numberOfJobs;
        }

        /// <summary>
        /// Şablona ilişkin öndeğer yazıcının seçilmesi sağlanır. Açılan form ekranında seçilen yazıcı, o şablon için her zaman kullanılacak yazıcıdır.
        /// </summary>
        /// <param name="parameters">parameters[0] = pGuid</param>
        /// <returns>
        /// <b>Başarılı :</b>ret[0]="TRUE",    ret[1]=":B:YazıcıAdı"
        /// <br/>
        /// <b>Hatalı   :</b>ret[0]="FALSE",   ret[1]=":H:HataAçıklaması"
        /// </returns>
        public IList<string> YaziciSec(IList<string> parameters)
        {
            IList<string> ret = new List<string>();
            try
            {
                SQLServerIp = parameters[0];
                SQLServerName = parameters[1];
                SQLDatabase = parameters[2];
                SQLUser = parameters[3];
                SQLPassword = parameters[4];
                string pGuid = "";
                if (parameters.Count == 6)
                {
                    pGuid = parameters[5];
                }

                if (_gueueInit)
                {
                    frmInvoke.Invoke((MethodInvoker)delegate
                    {
                        frmPrinter frmPrinter = new frmPrinter();
                        frmPrinter.StartPosition = FormStartPosition.CenterScreen;
                        frmPrinter.GReportGuid = pGuid;
                        frmPrinter.ShowDialog();
                    });
                }
                else
                {
                    frmPrinter frmPrinter = new frmPrinter();
                    frmPrinter.StartPosition = FormStartPosition.CenterScreen;
                    frmPrinter.GReportGuid = pGuid;
                    frmPrinter.ShowDialog();
                }
                ret.Add("TRUE");
            }
            catch (Exception ex)
            {
                ret.Add("FALSE");
                ret.Add(MesajHata + ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// Şablona ilişkin öndeğer yazıcının getirilmesi sağlanır. Eğer yoksa yazıcı seçimi yapılması için YaziciSec methodu tetiklenir.
        /// Kullanıcının seçtiği yazıcı geriye döndürülür. Eğer kullanıcı seçim yapmadan İptal yaparsa geriye boş değer dönmektedir.
        /// </summary>
        /// <param name="guid">Şablonun guid değeri</param>
        /// <returns>
        /// Yazıcı Adı geri dönmektedir.
        /// </returns>
        public static string SeciliYaziciAdiGetir(string guid)
        {
            string yaziciAdi = "";
            try
            {
                yaziciAdi = (string)Registry.CurrentUser.OpenSubKey("Software\\Model\\ReportDesigner\\", false).GetValue(guid);
            }
            catch (Exception)
            {
            }

            return yaziciAdi;
        }

        /// <summary>
        /// Şablona ilişkin yazıcının "CurrentUser\SOFTWARE\MODEL\ReportDesigner" altına yazılması sağlanıyor.
        /// </summary>
        /// <param name="guid">Şablonun guid değeri</param>
        /// <param name="printerName">Şablonda öndeğer kullanılacak yazıcı adı</param>
        private void SetDefaultPrinterName(string guid, string printerName)
        {
            try
            {
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

        private string DataCheck(XtraReport xrReport, DataTable dataTable, List<string> checkList)
        {
            string retStr = "";
            foreach (Band band in xrReport.Bands)
            {
                foreach (XRControl control in band.Controls)
                {
                    XRBindingCollection binds = control.DataBindings;
                    string fieldName = "";
                    if ((binds != null) && (binds.Count > 0))
                    {
                        //Trace.WriteLine("<<<<<<<<< DATABINDINGS >>>>>>>>>>>>>");
                        XRBinding bind = binds[0];
                        string dataMember = bind.DataMember;
                        //Trace.WriteLine("dataMember = " + dataMember);
                        string[] names = dataMember.Split('.');//table.column
                        if (names.Length >= 1)
                        {
                            //Trace.WriteLine("names.Length = " + names.Length.ToString());
                            //field name
                            fieldName = names[names.Length - 1];
                            //Trace.WriteLine("fieldName = " + fieldName);
                        }
                    }
                    else
                    {
                        //Trace.WriteLine("<<<<<<<<< TEXT >>>>>>>>>>>>>");
                        if (control.Text.IndexOf("[") > -1 && control.Text.IndexOf("]") > -1)
                        {
                            fieldName = control.Text.Replace("[", string.Empty).Replace("]", string.Empty);
                        }
                    }

                    if (fieldName != "" && fieldName != null)
                    {
                        for (int i = 0; i < checkList.Count; i++)
                        {
                            //int iii = checkList[i].IndexOf(fieldName);
                            if (checkList[i].IndexOf("[" + fieldName + "]") > -1)
                            {
                                DataRow[] dataRows = dataTable.Select(checkList[i]);
                                if (dataRows.Length > 0)
                                {
                                    string mesaj = "Çıktı alırken " + checkList[i].Substring(0, checkList[i].IndexOf(' ')) + " alanı boş olamaz.";
                                    //MessageBox.Show(mesaj, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (_gueueInit)
                                    {
                                        frmInvoke.Invoke((MethodInvoker)delegate
                                        {
                                            frmMessageBox fmsg = new frmMessageBox(mesaj);
                                            fmsg.ShowDialog();
                                        });
                                    }
                                    else
                                    {
                                        frmMessageBox fmsg = new frmMessageBox(mesaj);
                                        fmsg.ShowDialog();
                                    }
                                    retStr = MesajUyari + mesaj;
                                    return retStr;
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }

        public static void EtiketLog(string sql, string programNo, string reportGuid, string reportVersiyon)
        {
            try
            {
                using (SqlConnection connLog = new SqlConnection(ConStr))
                {
                    connLog.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string cmdText = "INSERT INTO MODEL_HISTORY.dbo.etiket_bas_log(sql,username,program_no,report_guid,report_versiyon) VALUES(@sql,'" + ErpKullaniciAdi + "','" + programNo + "','" + reportGuid + "','" + reportVersiyon + "')";
                        cmd.CommandText = cmdText;
                        cmd.Connection = connLog;
                        //= new SqlCommand(cmdText, connLog);
                        cmd.Parameters.AddWithValue("@sql", sql);
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 600;
                        cmd.ExecuteNonQuery();
                        connLog.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("EtiketLog LOG HATASI : " + ex.Message);
            }
        }
    }

    public class PrintItem
    {
        public List<string> parameters = new List<string>();
        public PrintItem next;

        public PrintItem(List<string> param)
        {
            this.parameters = param;

        }
    }

    public class Queue
    {
        object sync = new object();
        PrintItem head;
        PrintItem tail;

        public void Add(PrintItem pi)
        {
            lock (this.sync)
            {
                if (this.head == null)
                {
                    this.head = pi;
                    this.tail = pi;
                }
                else
                {
                    this.tail.next = pi;
                    this.tail = pi;
                }
            }
        }

        public PrintItem Pop()
        {
            lock (this.sync)
            {
                var ret = this.head;
                this.head = this.head.next;
                return ret;
            }
        }

        public bool Any()
        {
            lock (this.sync)
            {
                return this.head != null;
            }
        }
    }
}
