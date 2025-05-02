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
        private const string profilesDirectory = "Schedules";

        public ZilForm()
        {
            InitializeComponent();
            Directory.CreateDirectory(profilesDirectory);
            LoadProfiles();
            SetupForm();
            SetupTimer();
        }

        private void SetupForm()
        {
            // Initialize profile combo box
            profileComboBox.Items.AddRange(scheduleProfiles.Keys.ToArray());
            profileComboBox.SelectedItem = currentProfile;

            // Update schedule list
            UpdateScheduleList();
        }

        private void profileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentProfile = profileComboBox.SelectedItem.ToString();
            UpdateScheduleList();
        }

        private void newProfileButton_Click(object sender, EventArgs e)
        {
            string newProfile = $"Profile_{scheduleProfiles.Count + 1}";
            scheduleProfiles[newProfile] = new List<MusicSchedule>();
            profileComboBox.Items.Add(newProfile);
            profileComboBox.SelectedItem = newProfile;
            currentProfile = newProfile;
            SaveProfiles();
            UpdateScheduleList();
        }

        private void deleteProfileButton_Click(object sender, EventArgs e)
        {
            if (currentProfile == "Default")
            {
                profileComboBox.Items.Clear();
                profileComboBox.SelectedItem = "Default";
                currentProfile = "Default";
                SaveProfiles();
                UpdateScheduleList();
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete the '{currentProfile}' profile?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Remove profile and its JSON file
                scheduleProfiles.Remove(currentProfile);
                string profileFile = Path.Combine(profilesDirectory, $"{currentProfile}.json");
                if (File.Exists(profileFile))
                {
                    File.Delete(profileFile);
                }

                // Switch to Default profile
                currentProfile = "Default";
                profileComboBox.Items.Clear();
                profileComboBox.Items.AddRange(scheduleProfiles.Keys.ToArray());
                profileComboBox.SelectedItem = currentProfile;

                SaveProfiles();
                UpdateScheduleList();
            }
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectFileButton.Tag = openFileDialog.FileName;
                    selectFileButton.Text = Path.GetFileName(openFileDialog.FileName);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddSchedule(timePicker.Value, selectFileButton.Tag?.ToString());
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (scheduleList.SelectedIndex >= 0)
            {
                scheduleProfiles[currentProfile].RemoveAt(scheduleList.SelectedIndex);
                SaveProfiles();
                UpdateScheduleList();
            }
        }

        private void SetupTimer()
        {
            timer.Interval = 60000; // Check every minute
            timer.Tick += (s, e) =>
            {
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
            timer.Start();
        }

        private void AddSchedule(DateTime time, string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Please select a valid MP3 file.");
                return;
            }

            // Ensure at least 20 schedules can be added (checking for daily coverage)


            scheduleProfiles[currentProfile].Add(new MusicSchedule
            {
                Time = time,
                FilePath = filePath
            });

            // Sort schedules by time
            scheduleProfiles[currentProfile] = scheduleProfiles[currentProfile]
                .OrderBy(s => s.Time.Hour)
                .ThenBy(s => s.Time.Minute)
                .ToList();

            SaveProfiles();
            UpdateScheduleList();
        }

        private void UpdateScheduleList()
        {
            scheduleList.Items.Clear();
            if (scheduleProfiles.ContainsKey(currentProfile))
            {
                foreach (var schedule in scheduleProfiles[currentProfile])
                {
                    scheduleList.Items.Add($"{schedule.Time:HH:mm} - {Path.GetFileName(schedule.FilePath)}");
                }
            }
        }

        private void PlayMusic(string filePath)
        {
            try
            {
                using (var mp3Reader = new Mp3FileReader(filePath))
                using (var waveOut = new WaveOutEvent())
                {
                    waveOut.Init(mp3Reader);
                    waveOut.Play();
                    // Wait for playback to complete
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing MP3: {ex.Message}");
            }
        }

        private void LoadProfiles()
        {
            scheduleProfiles["Default"] = new List<MusicSchedule>();
            foreach (var file in Directory.GetFiles(profilesDirectory, "*.json"))
            {
                var json = File.ReadAllText(file);
                var profileName = Path.GetFileNameWithoutExtension(file);
                scheduleProfiles[profileName] = JsonSerializer.Deserialize<List<MusicSchedule>>(json) ?? new List<MusicSchedule>();
            }
        }

        private void SaveProfiles()
        {
            foreach (var profile in scheduleProfiles)
            {
                var json = JsonSerializer.Serialize(profile.Value, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(Path.Combine(profilesDirectory, $"{profile.Key}.json"), json);
            }
        }
    }

    [Serializable]
    public class MusicSchedule
    {
        public DateTime Time { get; set; }
        public string FilePath { get; set; }
    }
}