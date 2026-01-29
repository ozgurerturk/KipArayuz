using Krypton.Toolkit;
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


    public partial class AnaSayfaFormu : KryptonForm
    {
        private Configuration _config;
        private Process? _kipProcess;

        public AnaSayfaFormu()
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            InitializeComponent();
        }

        private void Kip_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    kryRichTextBoxCikis.AppendText(e.Data + Environment.NewLine);
                    kryRibbonGroupTextBoxStdInput.Enabled = true;
                    kryRibbonGroupButtonInputGonder.Enabled = true;
                });
            }
        }

        private void Kip_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    kryRichTextBoxCikis.AppendText("HATA: " + e.Data + Environment.NewLine);
                    kryRibbonGroupTextBoxStdInput.Enabled = false;
                    kryRibbonGroupButtonInputGonder.Enabled = false;
                });
            }
        }

        private void Kip_Exited(object? sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                kryRichTextBoxCikis.AppendText("Kip işlemi sona erdi." + Environment.NewLine);
                kryRibbonGroupTextBoxStdInput.Enabled = false;
                kryRibbonGroupButtonInputGonder.Enabled = false;
                kryRibbonGroupButtonDur.Enabled = false;
                kryRibbonButtonCalistir.Enabled = true;
            });
        }

        private void btnExePath_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new();
            configForm.ShowDialog();
        }

        private static Task WriteAllTextAsyncCompat(string path, string contents, Encoding encoding)
        {
#if NET8_0_OR_GREATER
            return File.WriteAllTextAsync(path, contents, encoding);
#else
            File.WriteAllText(path, contents, encoding);
            return Task.CompletedTask;
#endif
        }

        private async void kryRibbonButtonCalistir_Click(object sender, EventArgs e)
        {
            string KipDefaultPath = _config.AppSettings.Settings["KipCalistirExeYolu"]?.Value ?? string.Empty;

            if (_kipProcess != null && !_kipProcess.HasExited)
            {
                MessageBox.Show("Program zaten çalışıyor.");
                return;
            }

            if (string.IsNullOrWhiteSpace(kryRichTextBoxKod.Text))
            {
                MessageBox.Show("Çalıştırılacak Kip kodu boş.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            kryRichTextBoxCikis.Clear();

            var kipExePath = KipDefaultPath;
            var kipCode = kryRichTextBoxKod.Text;

            var kipFilePath = Path.Combine(
            Path.GetTempPath(),
            $"kip_gui_{Guid.NewGuid():N}.kip"
            );

            var utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            await WriteAllTextAsyncCompat(kipFilePath, kipCode, utf8NoBom);

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
                StandardErrorEncoding = utf8NoBom
            };

#if NET8_0_OR_GREATER
            psi.StandardOutputEncoding = utf8NoBom;
            psi.StandardErrorEncoding = utf8NoBom;
            psi.StandardInputEncoding = utf8NoBom;
#endif

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
            kryRibbonGroupButtonDur.Enabled = true;
            kryRibbonButtonCalistir.Enabled = false;
        }

        private async void kryRibbonGroupButtonInputGonder_Click(object sender, EventArgs e)
        {
            if (_kipProcess == null || _kipProcess.HasExited)
            {
                MessageBox.Show("Çalışan Kip süreci yok.");
                return;
            }

            var line = kryRibbonGroupTextBoxStdInput.Text ?? string.Empty;
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
                kryRibbonGroupTextBoxStdInput.Clear();
            }
        }

        private void kryRibbonGroupButtonExeSec_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Kip EXE|kip.exe|Tüm EXE Dosyaları|*.exe",
                Title = "kip.exe'yi seç"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                _config.AppSettings.Settings.Remove("KipCalistirExeYolu");
                _config.AppSettings.Settings.Add("KipCalistirExeYolu", kryRibbonGroupTextBoxExeYolu.Text);
                _config.Save(ConfigurationSaveMode.Modified);

                kryRibbonGroupTextBoxExeYolu.Text = _config.AppSettings.Settings["KipCalistirExeYolu"]?.Value ?? string.Empty;
            }
        }

        private void AnaSayfaFormu_Load(object sender, EventArgs e)
        {
            kryRibbonGroupTextBoxExeYolu.Text = _config.AppSettings.Settings["KipCalistirExeYolu"]?.Value ?? string.Empty;
            KryptonManager manager = new KryptonManager();
            manager.GlobalPaletteMode = PaletteMode.Microsoft365BlueLightMode;
        }

        private void kryRibbonGroupButtonKitGitHubPage_Click(object sender, EventArgs e)
        {
            Process.Start("https://kip-dili.github.io/");
        }

        private void kryRibbonGroupButtonKitGitHubPage_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://github.com/kip-dili/kip");
        }

        private void kryRibbonGroupButtonDur_Click(object sender, EventArgs e)
        {
            if (_kipProcess == null || _kipProcess.HasExited)
            {
                MessageBox.Show("Çalışan Kip süreci yok.");
                return;
            }

            _kipProcess.Kill();
            kryRibbonGroupButtonDur.Enabled = false;
            kryRibbonButtonCalistir.Enabled = true;

            kryRichTextBoxCikis.AppendText("Kip süreci kullanıcı tarafından sonlandırıldı." + Environment.NewLine);
        }

        private void kryRibbonGroupButtonDosyaAc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yeni dosya açmak istediğinizden emin misiniz? Mevcut kod silinecektir.",
                                "Uyarı",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning)
                == DialogResult.Cancel)
            {
                return;
            }

            var ofd = new OpenFileDialog
            {
                Filter = "Kip dosyası (*.kip)|*.kip|Bütün dosyalar (*.*)|*.*",
                Title = "Kip dosyasını seç"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                var kipFile = File.ReadAllLines(ofd.FileName);

                kryRichTextBoxKod.Clear();

                kryRichTextBoxKod.Lines = kipFile;
            }
        }

        private void kryRibbonGroupButtonKaydet_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog
            {
                Description = "Kip kod dosyasını kaydetmek istediğiniz klasörü seçin"
            };

            if (fbd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                return;
            }

            var kipFilePath = Path.Combine(
                fbd.SelectedPath,
                $"kip_code_{DateTime.Now:yyyyMMdd_HHmmss}.kip"
                );

            File.WriteAllText(kipFilePath, kryRichTextBoxKod.Text, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

            MessageBox.Show($"Kip kodu başarıyla kaydedildi: {kipFilePath}", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
