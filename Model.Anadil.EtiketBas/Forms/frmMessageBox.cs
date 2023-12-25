using System;
using System.Media;
using System.Windows.Forms;

namespace Model.Anadil.EtiketBas
{
    public partial class frmMessageBox : Form
    {
        public frmMessageBox(string mesaj)
        {
            InitializeComponent();
            this.label1.Text = mesaj;
            SoundPlayer errorSound = new SoundPlayer(DotNetObject.AlwaysRoot + "\\hata.wav");
            errorSound.Play();
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
