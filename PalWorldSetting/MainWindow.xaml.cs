using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using PalWorldSetting.lib;
using FolderSelect;
using System.Linq;
using System.Windows.Controls;
using System.Diagnostics;
using PalWorldSetting.Properties;

namespace PalWorldSetting
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ConfigPath = "none";
        private ObservableCollection<Config> ConfigData { get; set; }
        private Dictionary<string, string> OriginalConfig;

        public MainWindow()
        {
            InitializeComponent();
            ConfigData = new ObservableCollection<Config>();
            OriginalConfig = new Dictionary<string, string>();
            ZData.InitSetting();
            CheckRecordButton();
            ZData.LoadI18nFile();
            InitI18nFromUI();
            ZData.LoadDataFile();
        }

        public void CheckRecordButton()
        {
            string RecordPath = ZData.Setting.Read("OpenRecord", "Setting");
            if (File.Exists(RecordPath))
            {
                LoadRecordFile.IsEnabled = true;
            }
        }

        #region UI SET I18n
        private void InitI18nFromUI()
        {
            this.Title = (string)ZData.I18n["UI_WINDOW_TITLE_TEXT"];
            LoadFile.Content = (string)ZData.I18n["UI_LOAD_BUTTON_TEXT"];
            ReLoad.Content = (string)ZData.I18n["UI_RELOAD_BUTTON_TEXT"];
            CloseFile.Content = (string)ZData.I18n["UI_CLOSE_BUTTON_TEXT"];
            SaveFile.Content = (string)ZData.I18n["UI_SAVE_BUTTON_TEXT"];
            LoadRecordFile.Content = (string)ZData.I18n["UI_LOAD_RECORD_BUTTON_TEXT"];

            string[] colHeaders = { "Key", "Value", "Default_Limit", "Remark" };
            foreach (string colTitle in colHeaders)
            {
                if (dataGrid.Columns.Single(c => c.Header.ToString() == colTitle) is DataGridTextColumn dataGridColumn)
                {
                    dataGridColumn.Header = (string)ZData.I18n[$"UI_COLUMN_{colTitle.ToUpper()}_TITLE_TEXT"];
                }
            }
        }
        #endregion

        #region fun
        private string ShowFolderDialog()
        {
            FolderSelectDialog fsd = new FolderSelectDialog();
            fsd.Title = ZData.I18n["UI_SELECT_FOLDER_TITLE_TEXT"];
            fsd.InitialDirectory = Directory.GetCurrentDirectory();
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                if (!File.Exists($"{fsd.FileName}\\PalWorldSettings.ini"))
                {
                    return "none";
                }

                return $"{fsd.FileName}\\PalWorldSettings.ini";
            }

            return "none";
        }

        private void LoadData(string path)
        {
            bool LoadFound = true;

            if (path == "none")
            {
                MessageBox.Show((string)ZData.I18n["UI_MESSAGE_SELECT_FOLDER_ERROR_TEXT"], (string)ZData.I18n["UI_MESSAGE_ERROR_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string optionSettingsLine = ZData.ReadOptionSettingsLine(path);
                MatchCollection matches = Regex.Matches(optionSettingsLine, @"(\w+)=(\""[^\""]*\""|\d+\.\d+|\w+),?");
                foreach (Match match in matches)
                {
                    if (!match.Success) { continue; }

                    string key = match.Groups[1].Value.Trim();
                    string value = match.Groups[2].Value.Trim();
                    value = value.Replace("\"", ""); // Replace "

                    OriginalConfig.Add(key, value);
                    ConfigData.Add(new Config()
                    {
                        CKey = key,
                        CValue = value,
                        CRemark = ZData.Remarks(key),
                        CValueNote = $"{ZData.Default(key)} {ZData.Limit(key)}",
                        CDefault = ZData.Default(key),
                        CReayEnabled = ZData.IsEnabled(key),
                        CType = ZData.Type(key),
                    });
                    // Debug
                    //Trace.WriteLine($"{key}: {value}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, (string)ZData.I18n["UI_MESSAGE_ERROR_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Error);
                LoadFound = false;
            }

            if (LoadFound)
            {
                this.Title = $"{(string)ZData.I18n["UI_WINDOW_TITLE_TEXT"]} : {path}";
                dataGrid.ItemsSource = ConfigData;
                LoadFile.IsEnabled = false;
                LoadRecordFile.IsEnabled = false;
                ReLoad.IsEnabled = true;
                CloseFile.IsEnabled = true;
                SaveFile.IsEnabled = true;
                dataGrid.IsEnabled = true;
                ZData.Setting.Write("OpenRecord", path, "Setting");
            }

        }
        #endregion

        #region Load Record File
        private void LoadRecordFile_Click(object sender, RoutedEventArgs e)
        {
            string RecordPath = ZData.Setting.Read("OpenRecord", "Setting");
            if (File.Exists(RecordPath))
            {
                ConfigPath = RecordPath;
                LoadData(RecordPath);
            }
        }
        #endregion

        #region Load File
        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            ConfigPath = ShowFolderDialog();
            LoadData(ConfigPath);
        }
        #endregion

        #region RELoad

        private void ReLoad_Click(object sender, RoutedEventArgs e)
        {
            ClearStatus();
            LoadData(ConfigPath);
            MessageBox.Show((string)ZData.I18n["UI_MESSAGE_RELOAD_TEXT"], (string)ZData.I18n["UI_MESSAGE_WARNING_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        #region Save File
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            bool Found = true;

            try
            {
                string CofnigString = "(";
                foreach (Config cfg in ConfigData)
                {
                    if (OriginalConfig.ContainsKey(cfg.CKey))
                    {
                        OriginalConfig[cfg.CKey] = cfg.CValue;

                        string val = ZData.SaveFormat(cfg.CKey, cfg.CValue);
                        Trace.WriteLine(val);
                        CofnigString += $"{val},";
                    }
                }

                CofnigString = CofnigString.Substring(0, CofnigString.LastIndexOf(',')) + ")";

                string[] lines = File.ReadAllLines(ConfigPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("OptionSettings"))
                    {
                        lines[i] = "OptionSettings=" + CofnigString;
                        break;
                    }
                }
                File.WriteAllLines(ConfigPath, lines);
            }
            catch (Exception ex)
            {
                Found = false;
                MessageBox.Show(ex.Message, (string)ZData.I18n["UI_MESSAGE_ERROR_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (Found) { 
                MessageBox.Show((string)ZData.I18n["UI_MESSAGE_SAVE_OK_TEXT"], (string)ZData.I18n["UI_MESSAGE_OK_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Information);
                ClearStatus();
                CheckRecordButton();
                ConfigPath = "none";
            }
        }
        #endregion

        #region Close File
        private void ClearStatus()
        {
            this.Title = (string)ZData.I18n["UI_WINDOW_TITLE_TEXT"];
            dataGrid.ItemsSource = "";
            LoadFile.IsEnabled = true;
            CloseFile.IsEnabled = false;
            SaveFile.IsEnabled = false;
            ReLoad.IsEnabled = false;
            dataGrid.IsEnabled = false;
            ConfigData.Clear();
            OriginalConfig.Clear();
        }

        private void CloseFile_Click(object sender, RoutedEventArgs e)
        {
            ClearStatus();
            CheckRecordButton();
            ConfigPath = "none";
        }
        #endregion

    }
}
