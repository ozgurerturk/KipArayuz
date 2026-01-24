using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KipCalistirArayuz
{
    public partial class ConfigForm : Form
    {
        private Configuration _config;

        public ConfigForm()
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            InitializeComponent();
        }

        private void btnKipExeYolu_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Kip EXE|kip.exe|Tüm EXE Dosyaları|*.exe",
                Title = "kip.exe'yi seç"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                textBoxKipExeYolu.Text = ofd.FileName;
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            textBoxKipExeYolu.Text = _config.AppSettings.Settings["KipCalistirExeYolu"]?.Value ?? string.Empty;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(!(MessageBox.Show("Ayarları kaydet?", "Bilgi", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK))
            {
                return;
            }

            try
            {
                // Save settings
                _config.AppSettings.Settings.Remove("KipCalistirExeYolu");
                _config.AppSettings.Settings.Add("KipCalistirExeYolu", textBoxKipExeYolu.Text);
                _config.Save(ConfigurationSaveMode.Modified);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ayarlar kaydedilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
