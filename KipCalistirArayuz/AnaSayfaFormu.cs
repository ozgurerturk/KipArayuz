using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KipCalistirArayuz
{


    public partial class AnaSayfaFormu : Form
    {
        private Process _kipProcess;

        public AnaSayfaFormu()
        {
            InitializeComponent();
        }

        private async void btnCalistir_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string KipDefaultPath = config.AppSettings.Settings["KipCalistirExeYolu"]?.Value ?? string.Empty;

            if (_kipProcess != null && !_kipProcess.HasExited)
            {
                MessageBox.Show("Program zaten çalışıyor.");
                return;
            }

            if (string.IsNullOrWhiteSpace(richTextBoxKod.Text))
            {
                MessageBox.Show("Çalıştırılacak Kip kodu boş.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            richTextBoxCikis.Clear();

            var kipExePath = KipDefaultPath;
            var kipCode = richTextBoxKod.Text;

            var kipFilePath = Path.Combine(
            Path.GetTempPath(),
            $"kip_gui_{Guid.NewGuid():N}.kip"
            );

            var utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            await File.WriteAllTextAsync(kipFilePath, kipCode, utf8NoBom);

            var arguments =
        $"/c chcp 65001 >NUL & \"{kipExePath}\" --lang tr --exec \"{kipFilePath}\"";

            var psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = utf8NoBom,
                StandardErrorEncoding = utf8NoBom,
                StandardInputEncoding = utf8NoBom
            };

            _kipProcess = new Process
            {
                StartInfo = psi,
                EnableRaisingEvents = true
            };

            _kipProcess.OutputDataReceived += Kip_OutputDataReceived;
            _kipProcess.ErrorDataReceived += Kip_ErrorDataReceived;
            _kipProcess.Exited += Kip_Exited;

            _kipProcess.Start();
            _kipProcess.StandardInput.AutoFlush = true;
            _kipProcess.BeginOutputReadLine();
            _kipProcess.BeginErrorReadLine();
        }

        private void Kip_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    richTextBoxCikis.AppendText(e.Data + Environment.NewLine);
                });
            }
        }

        private void Kip_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    richTextBoxCikis.AppendText("HATA: " + e.Data + Environment.NewLine);
                });
            }
        }

        private void Kip_Exited(object? sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                richTextBoxCikis.AppendText("Kip işlemi sona erdi." + Environment.NewLine);
            });
        }

        private async void btnInputGonder_Click(object sender, EventArgs e)
        {
            if (_kipProcess == null || _kipProcess.HasExited)
            {
                MessageBox.Show("Çalışan Kip süreci yok.");
                return;
            }

            var line = textBoxInput.Text ?? string.Empty;
            if (string.IsNullOrWhiteSpace(line))
                return;

            try
            {
                await _kipProcess.StandardInput.WriteLineAsync(line);
                await _kipProcess.StandardInput.FlushAsync();

                _kipProcess.StandardInput.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Girdi gönderilirken hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                textBoxInput.Clear();
            }
        }

        private void btnExePath_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new();
            configForm.ShowDialog();
        }
    }
}
