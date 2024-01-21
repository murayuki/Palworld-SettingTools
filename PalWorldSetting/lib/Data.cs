using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PalWorldSetting.lib
{
    public class ZData
    {
        public static JSONNode Remarks;
        public static JSONNode I18n;

        #region Load Remark Json
        public static void LoadRemarkFile()
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

        #region Load I18n Json
        public static void LoadI18nFile()
        {
            if (!File.Exists("./I18n.json"))
            {
                MessageBox.Show("'I18n.json' does not exist\n\n", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return;
            }

            try
            {
                string JsonString = File.ReadAllText("./I18n.json");
                I18n = JSON.Parse(JsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "I18n Load Error");
                Environment.Exit(0);
            }
        }
        #endregion

        #region Read Line
        public static string ReadOptionSettingsLine(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.StartsWith("OptionSettings="))
                {
                    return line.Substring("OptionSettings=".Length);
                }
            }

            return string.Empty;
        }
        #endregion
    }

    #region Config Type
    public class Config
    {
        public string CKey { get; set; }
        public string CValue { get; set; }
        public string CRemark { get; set; }
    }
    #endregion
}
