using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PalWorldSetting.lib
{
    public class ZData
    {
        public static JSONNode Data;
        public static InIConfig Setting;
        public static JSONNode I18n;

        #region Init Setting 
        public static void InitSetting()
        {
            Setting = new InIConfig();
            if (!Setting.KeyExists("InitSetting", "Setting"))
            {
                Setting.Write("OpenRecord", "", "Setting");
                Setting.Write("InitSetting", "True", "Setting");
            }
        }
        #endregion

        #region Load Data Json
        public static void LoadDataFile()
        {
            if (!File.Exists("./data.json"))
            {
                MessageBox.Show((string)I18n["UI_MESSAGE_DATE_MISSING_TEXT"], (string)I18n["UI_MESSAGE_ERROR_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return;
            }

            try
            {
                string JsonString = File.ReadAllText("./data.json");
                Data = JSON.Parse(JsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "data.json Load Error");
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

        #region Get Type
        public static string Type(string key)
        {
            if (!Data.HasKey(key))
            {
                return "String";
            }

            JSONNode keyVal = Data[key];

            if (!keyVal.HasKey("Remark"))
            {
                return "String";
            }

            return (string)keyVal["Type"];
        }
        #endregion

        #region Get Remarks
        public static string Remarks(string key)
        {
            if (!Data.HasKey(key))
            {
                return (string)I18n["UI_REMARK_MISSING_TEXT"];
            }

            JSONNode keyVal = Data[key];

            if (!keyVal.HasKey("Remark"))
            {
                return (string)I18n["UI_REMARK_MISSING_TEXT"];
            }

            return (string)keyVal["Remark"];
        }
        #endregion

        #region Get IsEnabled
        public static bool IsEnabled(string key)
        {
            if (!Data.HasKey(key))
            {
                return false;
            }

            JSONNode keyVal = Data[key];

            if (!keyVal.HasKey("IsEnabled"))
            {
                return false;
            }

            return (bool)keyVal["IsEnabled"];
        }
        #endregion

        #region Get Default
        public static string Default(string key)
        {
            if (!Data.HasKey(key))
            {
                return "";
            }

            JSONNode keyVal = Data[key];

            if (!keyVal.HasKey("Default"))
            {
                return "";
            }

            if (Type(key) == "Double")
            {
                double result = double.Parse((string)Data[key]["Default"]);
                return $"{result:F6}";
            }

            return (string)keyVal["Default"];
        }
        #endregion

        #region Get Limit
        public static string Limit(string key)
        {
            if (!Data.HasKey(key))
            {
                return "";
            }

            JSONNode keyVal = Data[key];

            if (keyVal.HasKey("MinVal") && keyVal.HasKey("MaxVal"))
            {
                return $"({keyVal["MinVal"]} - {keyVal["MaxVal"]})";
            }

            if (keyVal.HasKey("Options"))
            {
                string Option = "\n";
                foreach (var option in keyVal["Options"])
                {
                    Option += $"\n{(string)option.Key}: {(string)option.Value}";
                }
                return Option;
            }


            return "";
        }
        #endregion

        #region INI Format
        public static string SaveFormat(string key, string val)
        {
            string keyType = Type(key);

            if (keyType == "Val")
            {
                return $"{key}=\"{val}\"";
            }

            if (keyType == "Double")
            {
                double result = double.Parse(val);
                return $"{key}={result:F6}";
            }

            return $"{key}={val}";
        }
        #endregion
    }

    #region Config Type
    public class Config
    {
        private string PValue { get; set; }

        public string CKey { get; set; }
        public string CRemark { get; set; }
        public string CValueNote { get; set; }
        public string CDefault { get; set; }
        public bool CReayEnabled { get; set; }
        public string CType { get; set; }

        public string CValue
        {
            get { return PValue; }
            set
            {
                string OldValue = PValue;
                bool found = true;
                JSONNode KeyVal = ZData.Data[CKey];
                if (KeyVal == null)
                {
                    PValue = value;
                    return;
                }

                if ((string)KeyVal["Type"] == "Int" || (string)KeyVal["Type"] == "Double")
                {
                    if (KeyVal.HasKey("MinVal") && KeyVal.HasKey("MaxVal"))
                    {
                        try
                        {
                            double result = double.Parse(value);
                            if (result >= (double)KeyVal["MaxVal"])
                            {
                                PValue = KeyVal["MaxVal"];
                                found = false;
                            }
                            if (result < (double)KeyVal["MinVal"])
                            {
                                PValue = (string)KeyVal["MinVal"];
                                found = false;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show((string)ZData.I18n["UI_MESSAGE_AVAILABLE_LIMIT_TEXT"].ToString().Replace("{Value}", ZData.Limit(CKey)), (string)ZData.I18n["UI_MESSAGE_ERROR_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Error);
                            PValue = OldValue;
                            found = false;
                        }
                    }
                }

                if (found)
                {
                    PValue = value;
                    bool found2 = true;

                    if ((string)KeyVal["Type"] == "Bool")
                    {
                        if (PValue.ToLower() != "true" && PValue.ToLower() != "false")
                        {
                            MessageBox.Show((string)ZData.I18n["UI_MESSAGE_AVAILABLE_OPTIONS_TEXT"].ToString().Replace("{Options}", "True / False"), (string)ZData.I18n["UI_MESSAGE_ERROR_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Error);
                            found2 = false;
                        }
                    }

                    if ((string)KeyVal["Type"] == "Option")
                    {
                        string Option = "";
                        JSONNode Options = KeyVal["Options"];
                  
                        foreach (var option in Options)
                        {
                            if (option.Key.ToLower() == PValue)
                            {
                                PValue = option.Key;
                            }
                            
                            Option += $"\n{(string)option.Key}: {(string)option.Value}";
                        }

                        if (!Options.HasKey(PValue)) { 
                            MessageBox.Show((string)ZData.I18n["UI_MESSAGE_AVAILABLE_OPTIONS_TEXT"].ToString().Replace("{Options}", Option), (string)ZData.I18n["UI_MESSAGE_ERROR_TITLE_TEXT"], MessageBoxButton.OK, MessageBoxImage.Error);
                            found2 = false;
                        }
                    }

                    if (!found2)
                    {
                        PValue = OldValue;
                    }
                }

                // Check Modify
                if ((string)KeyVal["Type"] == "Double")
                {
                    PValue = $"{double.Parse(PValue):F6}";
                }

                if ((string)KeyVal["Type"] == "Bool")
                {
                    PValue = (PValue.ToLower() == "true") ? "True" : "False" ;
                }
            }
        }
    }
    #endregion
}
