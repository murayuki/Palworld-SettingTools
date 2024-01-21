using FolderSelect;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PalWorldSetting.Clan;

namespace PalWorldSetting
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public JSONNode Remarks;
        public string ConfigPath = "none";
        public ObservableCollection<Config> ConfigData { get; set; }
        public Dictionary<string, string> OriginalConfig;

        public MainWindow()
        {
            ConfigData = new ObservableCollection<Config>();
            OriginalConfig = new Dictionary<string, string>();
          
            InitializeComponent();

            ZClan.Init();
            LoadRemarkFile();　/// 讀取 Remark
        }

        #region Load Remark Json
        private void LoadRemarkFile()
        {
            if (!File.Exists("./Remark.json"))
            {
                MessageBox.Show("Remark.json 文件不存在請檢查文件完整\n\n確定後將關閉程式", "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return;
            }

            try
            {
                string JsonString = File.ReadAllText("./Remark.json");
                Remarks = JSON.Parse(JsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Remark 讀取錯誤");
                Environment.Exit(0);
            }
        }
        #endregion

        private string ShowFolderDialog()
        {
            FolderSelectDialog fsd = new FolderSelectDialog();
            fsd.Title = "選擇設定文件所在目錄";
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

        static string ReadOptionSettingsLine(string filePath)
        {
            // 读取文件所有行
            string[] lines = File.ReadAllLines(filePath);

            // 查找以 "OptionSettings=" 开头的行
            foreach (string line in lines)
            {
                if (line.StartsWith("OptionSettings="))
                {
                    // 返回=后的值
                    return line.Substring("OptionSettings=".Length);
                }
            }

            // 如果没有找到匹配的行，则返回空字符串或者抛出异常，取决于你的需求
            return string.Empty;
        }

        private void LoadData(string path)
        {
            bool LoadFound = true;

            if (path == "none")
            {
                MessageBox.Show("錯誤目錄...\n\n" +
                       "1.選擇文件目錄 \n(範例: D:\\PalWorld Server\\Pal\\Saved\\Config\\WindowsServer)\n" +
                       "2.路徑中必須包含 PalWorldSettings.ini", "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string optionSettingsLine = ReadOptionSettingsLine(path);
                var matches = Regex.Matches(optionSettingsLine, @"(\w+)=(\""[^\""]*\""|\d+\.\d+|\w+),?");
                foreach (Match match in matches)
                {
                    if (!match.Success) { continue; }

                    string key = match.Groups[1].Value.Trim();
                    string value = match.Groups[2].Value.Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }

                    OriginalConfig.Add(key, value);
                    ConfigData.Add(new Config()
                    {
                        CKey = key,
                        CValue = value,
                        CRemark = Remarks[key]
                    });
                    // Debug
                    Trace.WriteLine($"{key}: {value}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadFound = false;
            }

            if (LoadFound)
            {
                dataGrid.ItemsSource = ConfigData;
                LoadFile.IsEnabled = false;
                Re_Load.IsEnabled = true;

                CloseFile.IsEnabled = true;
                SaveFile.IsEnabled = true;
                dataGrid.IsEnabled = true;
            }
        }

        #region Load File
        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            ConfigPath = ShowFolderDialog();
            LoadData(ConfigPath);
        }
        #endregion

        #region RE Load

        private void Re_Load_Click(object sender, RoutedEventArgs e)
        {
            ClearStatus();
            LoadData(ConfigPath);
            MessageBox.Show("重新讀取文件", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (Found) { 
                MessageBox.Show("成功保存設置", "完成", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Re_Load.IsEnabled = false;
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

        public class Config
        {
            public string CKey { get; set; }
            public string CValue { get; set; }
            public string CRemark { get; set; }
        }

    }
}
