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
using System.Text.Json;
using NAudio.Wave;

namespace Zil
{
    public partial class ZilForm : Form
    {
        private Dictionary<string, List<MusicSchedule>> scheduleProfiles = new Dictionary<string, List<MusicSchedule>>();
        private string currentProfile = "Default";
        private Timer timer = new Timer();
        private bool isRunning = false;
        private const string profilesDirectory = "Schedules";
        private const string musicDirectory = "Music";
        private const string anthemVocal = "Music\\Marş Sözlü_1.8dk.mp3";
        private const string anthemInstrumental = "Music\\Marş Sözsüz.mp3";

        public ZilForm()
        {
            InitializeComponent();
            Directory.CreateDirectory(Path.Combine(Application.StartupPath, profilesDirectory));
            Directory.CreateDirectory(Path.Combine(Application.StartupPath, musicDirectory));
            LoadProfiles();
            SetupForm();
            SetupTimer();
        }

        private void SetupForm()
        {
            // Initialize profile combo box
            profileComboBox.Items.AddRange(scheduleProfiles.Keys.ToArray());
            profileComboBox.SelectedItem = currentProfile;

            // Initialize schedule form in tab
            ScheduleForm scheduleForm = new ScheduleForm(scheduleProfiles);
            scheduleForm.TopLevel = false;
            scheduleForm.FormBorderStyle = FormBorderStyle.None;
            scheduleForm.Dock = DockStyle.Fill;
            tabControl1.TabPages[1].Controls.Add(scheduleForm);
            scheduleForm.Show();

            // Initialize emergency form in tab
            EmergencyForm emergencyForm = new EmergencyForm(scheduleProfiles);
            emergencyForm.TopLevel = false;
            emergencyForm.FormBorderStyle = FormBorderStyle.None;
            emergencyForm.Dock = DockStyle.Fill;
            tabControl1.TabPages[2].Controls.Add(emergencyForm);
            emergencyForm.Show();
        }

        private void profileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentProfile = profileComboBox.SelectedItem.ToString();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!scheduleProfiles.ContainsKey(currentProfile) || scheduleProfiles[currentProfile].Count == 0)
            {
                MessageBox.Show("Lütfen geçerli bir profil seçin ve zamanlamalar ekleyin.");
                return;
            }
            isRunning = true;
            timer.Start();
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            isRunning = false;
            timer.Stop();
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        private void anthemVocalButton_Click(object sender, EventArgs e)
        {
            PlayMusic(anthemVocal);
        }

        private void anthemInstrumentalButton_Click(object sender, EventArgs e)
        {
            PlayMusic(anthemInstrumental);
        }

        private void SetupTimer()
        {
            timer.Interval = 60000; // Check every minute
            timer.Tick += (s, e) =>
            {
                if (!isRunning) return;
                var now = DateTime.Now;
                if (!scheduleProfiles.ContainsKey(currentProfile)) return;

                foreach (var schedule in scheduleProfiles[currentProfile])
                {
                    if (now.Hour == schedule.Time.Hour && now.Minute == schedule.Time.Minute)
                    {
                        PlayMusic(schedule.FilePath);
                    }
                }
            };
        }

        public void PlayMusic(string relativeFilePath)
        {
            try
            {
                string fullFilePath = Path.Combine(Application.StartupPath, relativeFilePath);
                if (!File.Exists(fullFilePath))
                {
                    MessageBox.Show($"MP3 dosyası bulunamadı: {relativeFilePath}");
                    return;
                }

                using (var mp3Reader = new Mp3FileReader(fullFilePath))
                using (var waveOut = new WaveOutEvent())
                {
                    waveOut.Init(mp3Reader);
                    waveOut.Play();
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MP3 çalınırken hata oluştu: {ex.Message}");
            }
        }

        private void LoadProfiles()
        {
            scheduleProfiles["Default"] = new List<MusicSchedule>();
            string profilesPath = Path.Combine(Application.StartupPath, profilesDirectory);
            if (Directory.Exists(profilesPath))
            {
                foreach (var file in Directory.GetFiles(profilesPath, "*.json"))
                {
                    var json = File.ReadAllText(file);
                    var profileName = Path.GetFileNameWithoutExtension(file);
                    scheduleProfiles[profileName] = JsonSerializer.Deserialize<List<MusicSchedule>>(json) ?? new List<MusicSchedule>();
                }
            }
        }
    }

    [Serializable]
    public class MusicSchedule
    {
        public DateTime Time { get; set; }
        public string FilePath { get; set; } // Stores relative path (e.g., Music\filename.mp3)
    }
}