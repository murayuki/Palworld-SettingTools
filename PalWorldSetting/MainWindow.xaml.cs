using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using PalWorldSetting.lib;
using FolderSelect;
using SimpleJSON;
using System.Linq;
using System.Windows.Controls;
using System.Diagnostics;

namespace PalWorldSetting
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        public string ConfigPath = "none";
        public ObservableCollection<Config> ConfigData { get; set; }
        public Dictionary<string, string> OriginalConfig;

        private string DataGridColumnKey;
        private string DataGridColumnVal;
        private string DataGridColumnRemark;


        public MainWindow()
        {
            ConfigData = new ObservableCollection<Config>();
            OriginalConfig = new Dictionary<string, string>();
          
            InitializeComponent();
            ZClan.Init();
            ZData.LoadI18nFile();
            InitI18nFromUI();

            ZData.LoadRemarkFile();
        }

        #region UI SET I18n
        private void InitI18nFromUI()
        {
            this.Title = (string)ZData.I18n["UI_WINDOW_TITLE_TEXT"];
            LoadFile.Content = (string)ZData.I18n["UI_LOAD_BUTTON_TEXT"];
            ReLoad.Content = (string)ZData.I18n["UI_RELOAD_BUTTON_TEXT"];
            CloseFile.Content = (string)ZData.I18n["UI_CLOSE_BUTTON_TEXT"];
            SaveFile.Content = (string)ZData.I18n["UI_SAVE_BUTTON_TEXT"];

            string[] colHeaders = { "Key", "Value", "Remark" };
            foreach (string colTitle in colHeaders)
            {
                DataGridTextColumn dataGridColumn = dataGrid.Columns.Single(c => c.Header.ToString() == colTitle) as DataGridTextColumn;
                if (dataGridColumn != null)
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
                var matches = Regex.Matches(optionSettingsLine, @"(\w+)=(\""[^\""]*\""|\d+\.\d+|\w+),?");
                foreach (Match match in matches)
                {
                    if (!match.Success) { continue; }

                    string key = match.Groups[1].Value.Trim();
                    string value = match.Groups[2].Value.Trim();
                    value = value.Replace("\"", ""); // Replace "

                    string Remark = (!ZData.Remarks[key]) ? (string)ZData.I18n["UI_REMARK_MISSING_TEXT"] : (string)ZData.Remarks[key];

                    OriginalConfig.Add(key, value);
                    ConfigData.Add(new Config()
                    {
                        CKey = key,
                        CValue = value,
                        CRemark = Remark
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
                dataGrid.ItemsSource = ConfigData;
                LoadFile.IsEnabled = false;
                ReLoad.IsEnabled = true;
                CloseFile.IsEnabled = true;
                SaveFile.IsEnabled = true;
                dataGrid.IsEnabled = true;
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

                        string val = ZClan.CheckType(cfg.CKey, cfg.CValue);

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
                ConfigPath = "none";
            }
        }
        #endregion

        #region Close File
        private void ClearStatus()
        {
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
            ConfigPath = "none";
        }
        #endregion
    }
}
