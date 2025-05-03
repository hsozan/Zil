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
            this.profileComboBox = new System.Windows.Forms.ComboBox();
            this.newProfileButton = new System.Windows.Forms.Button();
            this.deleteProfileButton = new System.Windows.Forms.Button();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.scheduleList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
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
            // newProfileButton
            // 
            this.newProfileButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.newProfileButton.Location = new System.Drawing.Point(180, 20);
            this.newProfileButton.Name = "newProfileButton";
            this.newProfileButton.Size = new System.Drawing.Size(100, 23);
            this.newProfileButton.TabIndex = 1;
            this.newProfileButton.Text = "Yeni Program";
            this.newProfileButton.UseVisualStyleBackColor = false;
            this.newProfileButton.Click += new System.EventHandler(this.newProfileButton_Click);
            // 
            // deleteProfileButton
            // 
            this.deleteProfileButton.BackColor = System.Drawing.Color.IndianRed;
            this.deleteProfileButton.Location = new System.Drawing.Point(290, 20);
            this.deleteProfileButton.Name = "deleteProfileButton";
            this.deleteProfileButton.Size = new System.Drawing.Size(100, 23);
            this.deleteProfileButton.TabIndex = 2;
            this.deleteProfileButton.Text = "Programı Sil";
            this.deleteProfileButton.UseVisualStyleBackColor = false;
            this.deleteProfileButton.Click += new System.EventHandler(this.deleteProfileButton_Click);
            // 
            // timePicker
            // 
            this.timePicker.CustomFormat = "HH:mm";
            this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePicker.Location = new System.Drawing.Point(20, 60);
            this.timePicker.Name = "timePicker";
            this.timePicker.ShowUpDown = true;
            this.timePicker.Size = new System.Drawing.Size(100, 20);
            this.timePicker.TabIndex = 3;
            // 
            // selectFileButton
            // 
            this.selectFileButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.selectFileButton.Location = new System.Drawing.Point(130, 60);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(120, 23);
            this.selectFileButton.TabIndex = 4;
            this.selectFileButton.Text = "Mp3 Seç";
            this.selectFileButton.UseVisualStyleBackColor = false;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.addButton.Location = new System.Drawing.Point(260, 60);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Zamanı Ekle";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.IndianRed;
            this.deleteButton.Location = new System.Drawing.Point(370, 60);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Seçiliyi Sil";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.editButton.Location = new System.Drawing.Point(480, 60);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(80, 23);
            this.editButton.TabIndex = 7;
            this.editButton.Text = "Düzenle";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // scheduleList
            // 
            this.scheduleList.FormattingEnabled = true;
            this.scheduleList.Location = new System.Drawing.Point(20, 100);
            this.scheduleList.Name = "scheduleList";
            this.scheduleList.Size = new System.Drawing.Size(540, 251);
            this.scheduleList.TabIndex = 8;
            // 
            // ZilForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.scheduleList);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.selectFileButton);
            this.Controls.Add(this.timePicker);
            this.Controls.Add(this.deleteProfileButton);
            this.Controls.Add(this.newProfileButton);
            this.Controls.Add(this.profileComboBox);
            this.Name = "ZilForm";
            this.Text = "Bahçeşehir Zil";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox profileComboBox;
        private System.Windows.Forms.Button newProfileButton;
        private System.Windows.Forms.Button deleteProfileButton;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.ListBox scheduleList;
    }
}