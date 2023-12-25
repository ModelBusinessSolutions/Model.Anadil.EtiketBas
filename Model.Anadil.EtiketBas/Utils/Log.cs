using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Model.Anadil.EtiketBas.Utils
{
    public class EtiketLogger
    {
        public static void Log(string sql, string programNo, string reportGuid, string reportVersiyon)
        {
            try
            {
                using (SqlConnection connLog = new SqlConnection(GetConnectionString()))
                {
                    connLog.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string cmdText = "INSERT INTO MODEL_HISTORY.dbo.etiket_bas_log(sql,username,program_no,report_guid,report_versiyon) " +
                                         "VALUES(@sql,@username,@programNo,@reportGuid,@reportVersiyon)";
                        cmd.CommandText = cmdText;
                        cmd.Connection = connLog;
                        cmd.Parameters.AddWithValue("@sql", sql);
                        cmd.Parameters.AddWithValue("@username", DotNetObject.ErpKullaniciAdi);
                        cmd.Parameters.AddWithValue("@programNo", programNo);
                        cmd.Parameters.AddWithValue("@reportGuid", reportGuid);
                        cmd.Parameters.AddWithValue("@reportVersiyon", reportVersiyon);
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 600;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("EtiketLog LOG HATASI : " + ex.Message);
            }
        }

        private static string GetConnectionString()
        {
            return DotNetObject.ConStr;
        }
    }

}
