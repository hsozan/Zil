namespace Zil
{
    partial class ZilForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.profileComboBox = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.anthemVocalButton = new System.Windows.Forms.Button();
            this.anthemInstrumentalButton = new System.Windows.Forms.Button();
            this.scheduleTab = new System.Windows.Forms.TabPage();
            this.emergencyTab = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.mainTab);
            this.tabControl1.Controls.Add(this.scheduleTab);
            this.tabControl1.Controls.Add(this.emergencyTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(576, 376);
            this.tabControl1.TabIndex = 0;
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.profileComboBox);
            this.mainTab.Controls.Add(this.startButton);
            this.mainTab.Controls.Add(this.stopButton);
            this.mainTab.Controls.Add(this.anthemVocalButton);
            this.mainTab.Controls.Add(this.anthemInstrumentalButton);
            this.mainTab.Location = new System.Drawing.Point(4, 22);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(568, 350);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "Ana Ekran";
            this.mainTab.UseVisualStyleBackColor = true;
            // 
            // profileComboBox
            // 
            this.profileComboBox.FormattingEnabled = true;
            this.profileComboBox.Location = new System.Drawing.Point(20, 20);
            this.profileComboBox.Name = "profileComboBox";
            this.profileComboBox.Size = new System.Drawing.Size(150, 21);
            this.profileComboBox.TabIndex = 0;
            this.profileComboBox.SelectedIndexChanged += new System.EventHandler(this.profileComboBox_SelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.startButton.Location = new System.Drawing.Point(180, 20);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Başlat";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.BackColor = System.Drawing.Color.IndianRed;
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(290, 20);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(100, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Durdur";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // anthemVocalButton
            // 
            this.anthemVocalButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.anthemVocalButton.Location = new System.Drawing.Point(180, 60);
            this.anthemVocalButton.Name = "anthemVocalButton";
            this.anthemVocalButton.Size = new System.Drawing.Size(120, 23);
            this.anthemVocalButton.TabIndex = 3;
            this.anthemVocalButton.Text = "İstiklal Marşı";
            this.anthemVocalButton.UseVisualStyleBackColor = false;
            this.anthemVocalButton.Click += new System.EventHandler(this.anthemVocalButton_Click);
            // 
            // anthemInstrumentalButton
            // 
            this.anthemInstrumentalButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.anthemInstrumentalButton.Location = new System.Drawing.Point(310, 60);
            this.anthemInstrumentalButton.Name = "anthemInstrumentalButton";
            this.anthemInstrumentalButton.Size = new System.Drawing.Size(150, 23);
            this.anthemInstrumentalButton.TabIndex = 4;
            this.anthemInstrumentalButton.Text = "İstiklal Marşı Enstrümental";
            this.anthemInstrumentalButton.UseVisualStyleBackColor = false;
            this.anthemInstrumentalButton.Click += new System.EventHandler(this.anthemInstrumentalButton_Click);
            // 
            // scheduleTab
            // 
            this.scheduleTab.Location = new System.Drawing.Point(4, 22);
            this.scheduleTab.Name = "scheduleTab";
            this.scheduleTab.Size = new System.Drawing.Size(568, 350);
            this.scheduleTab.TabIndex = 1;
            this.scheduleTab.Text = "Zamanlama Yönetimi";
            this.scheduleTab.UseVisualStyleBackColor = true;
            // 
            // emergencyTab
            // 
            this.emergencyTab.Location = new System.Drawing.Point(4, 22);
            this.emergencyTab.Name = "emergencyTab";
            this.emergencyTab.Size = new System.Drawing.Size(568, 350);
            this.emergencyTab.TabIndex = 2;
            this.emergencyTab.Text = "Acil Durum";
            this.emergencyTab.UseVisualStyleBackColor = true;
            // 
            // ZilForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.tabControl1);
            this.Name = "ZilForm";
            this.Text = "Bahçeşehir Zil";
            this.tabControl1.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mainTab;
        private System.Windows.Forms.ComboBox profileComboBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button anthemVocalButton;
        private System.Windows.Forms.Button anthemInstrumentalButton;
        private System.Windows.Forms.TabPage scheduleTab;
        private System.Windows.Forms.TabPage emergencyTab;
    }
}