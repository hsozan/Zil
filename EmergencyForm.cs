using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Zil
{
    public partial class EmergencyForm : Form
    {
        private Dictionary<string, List<MusicSchedule>> scheduleProfiles;
        private readonly string[] mp3Files = new string[]
        {
            "Music\\Anaokulu ve İlkokul İçin Yemek Vakti.mp3",
            "Music\\Etüt Başladı.mp3",
            "Music\\Etüt Sona Erdi.mp3",
            "Music\\Hepinize Günaydın.mp3",
            "Music\\İyi Akşamlar.mp3",
            "Music\\Marş Sözlü_1.mp3",
            "Music\\Marş Sözsüz.mp3",
            "Music\\Öğrenciler İyi Dersler.mp3",
            "Music\\Öğretmenlerimiz İyi Dersler.mp3",
            "Music\\Okuma Saati Başlamıştır.mp3",
            "Music\\Okuma Saati Sone Ermiştir.mp3",
            "Music\\Ortaokul ve Lise İçin Yemek Vakti.mp3",
            "Music\\Teneffüs.mp3"
        };

        public EmergencyForm(Dictionary<string, List<MusicSchedule>> profiles)
        {
            InitializeComponent();
            scheduleProfiles = profiles;
            SetupButtons();
        }

        private void SetupButtons()
        {
            foreach (string mp3Path in mp3Files)
            {
                string fileName = Path.GetFileNameWithoutExtension(mp3Path);
                Button button = new Button
                {
                    Text = fileName,
                    Size = new Size(180, 40),
                    BackColor = System.Drawing.SystemColors.ActiveCaption,
                    UseVisualStyleBackColor = false
                };
                button.Click += (sender, e) =>
                {
                    ZilForm parentForm = this.FindForm() as ZilForm;
                    parentForm?.PlayMusic(mp3Path);
                };
                flowLayoutPanel1.Controls.Add(button);
            }
        }
    }
}