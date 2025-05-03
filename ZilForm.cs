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
        private const string musicDirectory = "Music";

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
                MessageBox.Show("Cannot delete the Default profile.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete the '{currentProfile}' profile?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Remove profile and its JSON file
                scheduleProfiles.Remove(currentProfile);
                string profileFile = Path.Combine(Application.StartupPath, profilesDirectory, $"{currentProfile}.json");
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
                    try
                    {
                        // Copy MP3 to Music directory
                        string fileName = Path.GetFileName(openFileDialog.FileName);
                        string destPath = Path.Combine(Application.StartupPath, musicDirectory, fileName);
                        File.Copy(openFileDialog.FileName, destPath, true);

                        // Store relative path
                        string relativePath = Path.Combine(musicDirectory, fileName);
                        selectFileButton.Tag = relativePath;
                        selectFileButton.Text = fileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error copying MP3 file: {ex.Message}");
                    }
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddSchedule(timePicker.Value, selectFileButton.Tag?.ToString());
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (scheduleList.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a schedule to edit.");
                return;
            }

            string relativeFilePath = selectFileButton.Tag?.ToString();
            if (string.IsNullOrEmpty(relativeFilePath))
            {
                // Use existing file path if no new MP3 is selected
                relativeFilePath = scheduleProfiles[currentProfile][scheduleList.SelectedIndex].FilePath;
            }
            else
            {
                // Validate new MP3 file
                string fullFilePath = Path.Combine(Application.StartupPath, relativeFilePath);
                if (!File.Exists(fullFilePath))
                {
                    MessageBox.Show("The selected MP3 file does not exist in the Music directory.");
                    return;
                }
            }

            // Update the selected schedule
            scheduleProfiles[currentProfile][scheduleList.SelectedIndex] = new MusicSchedule
            {
                Time = timePicker.Value,
                FilePath = relativeFilePath
            };

            // Sort schedules by time
            scheduleProfiles[currentProfile] = scheduleProfiles[currentProfile]
                .OrderBy(s => s.Time.Hour)
                .ThenBy(s => s.Time.Minute)
                .ToList();

            SaveProfiles();
            UpdateScheduleList();
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

        private void AddSchedule(DateTime time, string relativeFilePath)
        {
            if (string.IsNullOrEmpty(relativeFilePath))
            {
                MessageBox.Show("Please select a valid MP3 file.");
                return;
            }

            // Resolve full path to check existence
            string fullFilePath = Path.Combine(Application.StartupPath, relativeFilePath);
            if (!File.Exists(fullFilePath))
            {
                MessageBox.Show("The selected MP3 file does not exist in the Music directory.");
                return;
            }


            scheduleProfiles[currentProfile].Add(new MusicSchedule
            {
                Time = time,
                FilePath = relativeFilePath // Store relative path
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

        private void PlayMusic(string relativeFilePath)
        {
            try
            {
                // Resolve full path
                string fullFilePath = Path.Combine(Application.StartupPath, relativeFilePath);
                if (!File.Exists(fullFilePath))
                {
                    MessageBox.Show($"MP3 file not found: {relativeFilePath}");
                    return;
                }

                using (var mp3Reader = new Mp3FileReader(fullFilePath))
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

        private void SaveProfiles()
        {
            foreach (var profile in scheduleProfiles)
            {
                var json = JsonSerializer.Serialize(profile.Value, new JsonSerializerOptions { WriteIndented = true });
                string profileFile = Path.Combine(Application.StartupPath, profilesDirectory, $"{profile.Key}.json");
                File.WriteAllText(profileFile, json);
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