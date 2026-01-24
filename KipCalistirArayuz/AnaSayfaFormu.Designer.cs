namespace KipCalistirArayuz
{
    partial class AnaSayfaFormu
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
            btnCalistir = new Button();
            richTextBoxKod = new RichTextBox();
            panel1 = new Panel();
            richTextBoxCikis = new RichTextBox();
            btnExePath = new Button();
            textBoxInput = new TextBox();
            label1 = new Label();
            btnInputGonder = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCalistir
            // 
            btnCalistir.Location = new Point(578, 15);
            btnCalistir.Name = "btnCalistir";
            btnCalistir.Size = new Size(94, 29);
            btnCalistir.TabIndex = 0;
            btnCalistir.Text = "Çalıştır";
            btnCalistir.UseVisualStyleBackColor = true;
            btnCalistir.Click += btnCalistir_Click;
            // 
            // richTextBoxKod
            // 
            richTextBoxKod.Dock = DockStyle.Bottom;
            richTextBoxKod.Location = new Point(0, 24);
            richTextBoxKod.Name = "richTextBoxKod";
            richTextBoxKod.Size = new Size(560, 402);
            richTextBoxKod.TabIndex = 1;
            richTextBoxKod.Text = "";
            // 
            // panel1
            // 
            panel1.Controls.Add(richTextBoxKod);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(560, 426);
            panel1.TabIndex = 3;
            // 
            // richTextBoxCikis
            // 
            richTextBoxCikis.Location = new Point(678, 16);
            richTextBoxCikis.Name = "richTextBoxCikis";
            richTextBoxCikis.ReadOnly = true;
            richTextBoxCikis.Size = new Size(557, 422);
            richTextBoxCikis.TabIndex = 4;
            richTextBoxCikis.Text = "";
            // 
            // btnExePath
            // 
            btnExePath.Location = new Point(578, 97);
            btnExePath.Name = "btnExePath";
            btnExePath.Size = new Size(94, 29);
            btnExePath.TabIndex = 5;
            btnExePath.Text = "Kip Exe Seç";
            btnExePath.UseVisualStyleBackColor = true;
            btnExePath.Click += btnExePath_Click;
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(578, 238);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(94, 27);
            textBoxInput.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(595, 215);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 7;
            label1.Text = "St Input";
            // 
            // btnInputGonder
            // 
            btnInputGonder.Location = new Point(578, 271);
            btnInputGonder.Name = "btnInputGonder";
            btnInputGonder.Size = new Size(94, 57);
            btnInputGonder.TabIndex = 8;
            btnInputGonder.Text = "Input Gönder";
            btnInputGonder.UseVisualStyleBackColor = true;
            btnInputGonder.Click += btnInputGonder_Click;
            // 
            // AnaSayfaFormu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1247, 450);
            Controls.Add(btnInputGonder);
            Controls.Add(label1);
            Controls.Add(textBoxInput);
            Controls.Add(btnExePath);
            Controls.Add(richTextBoxCikis);
            Controls.Add(panel1);
            Controls.Add(btnCalistir);
            Name = "AnaSayfaFormu";
            Text = "AnaSayfaFormu";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCalistir;
        private RichTextBox richTextBoxKod;
        private Panel panel1;
        private RichTextBox richTextBoxCikis;
        private Button btnExePath;
        private TextBox textBoxInput;
        private Label label1;
        private Button btnInputGonder;
    }
}