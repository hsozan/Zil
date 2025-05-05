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

namespace Zil
{
    public partial class ScheduleForm : Form
    {
        private Dictionary<string, List<MusicSchedule>> scheduleProfiles;
        private string currentProfile = "Default";
        private const string profilesDirectory = "Schedules";
        private const string musicDirectory = "Music";

        public ScheduleForm(Dictionary<string, List<MusicSchedule>> profiles)
        {
            InitializeComponent();
            scheduleProfiles = profiles;
            SetupForm();
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
                MessageBox.Show("Varsayılan profili silemezsiniz.");
                return;
            }

            if (MessageBox.Show($"'{currentProfile}' profilini silmek istediğinizden emin misiniz?", "Silmeyi Onayla", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                scheduleProfiles.Remove(currentProfile);
                string profileFile = Path.Combine(Application.StartupPath, profilesDirectory, $"{currentProfile}.json");
                if (File.Exists(profileFile))
                {
                    File.Delete(profileFile);
                }

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
                openFileDialog.Filter = "MP3 dosyaları (*.mp3)|*.mp3";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string fileName = Path.GetFileName(openFileDialog.FileName);
                        string destPath = Path.Combine(Application.StartupPath, musicDirectory, fileName);
                        File.Copy(openFileDialog.FileName, destPath, true);

                        string relativePath = Path.Combine(musicDirectory, fileName);
                        selectFileButton.Tag = relativePath;
                        selectFileButton.Text = fileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"MP3 dosyası kopyalanırken hata oluştu: {ex.Message}");
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
                MessageBox.Show("Lütfen düzenlemek için bir zamanlama seçin.");
                return;
            }

            string relativeFilePath = selectFileButton.Tag?.ToString();
            if (string.IsNullOrEmpty(relativeFilePath))
            {
                relativeFilePath = scheduleProfiles[currentProfile][scheduleList.SelectedIndex].FilePath;
            }
            else
            {
                string fullFilePath = Path.Combine(Application.StartupPath, relativeFilePath);
                if (!File.Exists(fullFilePath))
                {
                    MessageBox.Show("Seçilen MP3 dosyası Music dizininde bulunamadı.");
                    return;
                }
            }

            scheduleProfiles[currentProfile][scheduleList.SelectedIndex] = new MusicSchedule
            {
                Time = timePicker.Value,
                FilePath = relativeFilePath
            };

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

        private void AddSchedule(DateTime time, string relativeFilePath)
        {
            if (string.IsNullOrEmpty(relativeFilePath))
            {
                MessageBox.Show("Lütfen geçerli bir MP3 dosyası seçin.");
                return;
            }

            string fullFilePath = Path.Combine(Application.StartupPath, relativeFilePath);
            if (!File.Exists(fullFilePath))
            {
                MessageBox.Show("Seçilen MP3 dosyası Music dizininde bulunamadı.");
                return;
            }

            if (scheduleProfiles[currentProfile].Count >= 24)
            {
                MessageBox.Show("Bu profil için maksimum zamanlama sayısına ulaşıldı.");
                return;
            }

            scheduleProfiles[currentProfile].Add(new MusicSchedule
            {
                Time = time,
                FilePath = relativeFilePath
            });

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
}