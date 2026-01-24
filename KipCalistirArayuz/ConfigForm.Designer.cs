namespace KipCalistirArayuz
{
    partial class ConfigForm
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
            textBoxKipExeYolu = new TextBox();
            btnKipExeYolu = new Button();
            btnIptal = new Button();
            btnKaydet = new Button();
            SuspendLayout();
            // 
            // textBoxKipExeYolu
            // 
            textBoxKipExeYolu.Location = new Point(135, 28);
            textBoxKipExeYolu.Name = "textBoxKipExeYolu";
            textBoxKipExeYolu.Size = new Size(653, 27);
            textBoxKipExeYolu.TabIndex = 0;
            // 
            // btnKipExeYolu
            // 
            btnKipExeYolu.Location = new Point(12, 26);
            btnKipExeYolu.Name = "btnKipExeYolu";
            btnKipExeYolu.Size = new Size(117, 29);
            btnKipExeYolu.TabIndex = 1;
            btnKipExeYolu.Text = "Exe Yolu Seç";
            btnKipExeYolu.UseVisualStyleBackColor = true;
            btnKipExeYolu.Click += btnKipExeYolu_Click;
            // 
            // btnIptal
            // 
            btnIptal.Location = new Point(694, 418);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(94, 29);
            btnIptal.TabIndex = 2;
            btnIptal.Text = "İptal";
            btnIptal.UseVisualStyleBackColor = true;
            // 
            // btnKaydet
            // 
            btnKaydet.Location = new Point(594, 418);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(94, 29);
            btnKaydet.TabIndex = 3;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = true;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // ConfigForm
            // 
            AcceptButton = btnKaydet;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnIptal;
            ClientSize = new Size(800, 450);
            Controls.Add(btnKaydet);
            Controls.Add(btnIptal);
            Controls.Add(btnKipExeYolu);
            Controls.Add(textBoxKipExeYolu);
            Name = "ConfigForm";
            Text = "ConfigForm";
            Load += ConfigForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxKipExeYolu;
        private Button btnKipExeYolu;
        private Button btnIptal;
        private Button btnKaydet;
    }
}