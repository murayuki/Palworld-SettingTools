
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PalWorldSetting.Clan
{
    public class ZClan
    {
        static Dictionary<string, string> SettingType = new Dictionary<string, string>();

        public static void Init()
        {
            SettingType.Add("Difficulty", "String");
            SettingType.Add("DayTimeSpeedRate", "double");
            SettingType.Add("NightTimeSpeedRate", "double");
            SettingType.Add("ExpRate", "double");
            SettingType.Add("PalCaptureRate", "double");
            SettingType.Add("PalSpawnNumRate", "double");
            SettingType.Add("PalDamageRateAttack", "double");
            SettingType.Add("PalDamageRateDefense", "double");
            SettingType.Add("PlayerDamageRateAttack", "double");
            SettingType.Add("PlayerDamageRateDefense", "double");
            SettingType.Add("PlayerStomachDecreaceRate", "double");
            SettingType.Add("PlayerStaminaDecreaceRate", "double");
            SettingType.Add("PlayerAutoHPRegeneRate", "double");
            SettingType.Add("PlayerAutoHpRegeneRateInSleep", "double");
            SettingType.Add("PalStomachDecreaceRate", "double");
            SettingType.Add("PalStaminaDecreaceRate", "double");
            SettingType.Add("PalAutoHPRegeneRate", "double");
            SettingType.Add("PalAutoHpRegeneRateInSleep", "double");
            SettingType.Add("BuildObjectDamageRate", "double");
            SettingType.Add("BuildObjectDeteriorationDamageRate", "double");
            SettingType.Add("CollectionDropRate", "double");
            SettingType.Add("CollectionObjectHpRate", "double");
            SettingType.Add("CollectionObjectRespawnSpeedRate", "double");
            SettingType.Add("EnemyDropItemRate", "double");

            SettingType.Add("DeathPenalty", "String");

            SettingType.Add("bEnablePlayerToPlayerDamage", "Bool");
            SettingType.Add("bEnableFriendlyFire", "Bool");
            SettingType.Add("bEnableInvaderEnemy", "Bool");
            SettingType.Add("bActiveUNKO", "Bool");
            SettingType.Add("bEnableAimAssistPad", "Bool");
            SettingType.Add("bEnableAimAssistKeyboard", "Bool");

            SettingType.Add("DropItemMaxNum", "Int");
            SettingType.Add("DropItemMaxNum_UNKO", "Int");
            SettingType.Add("BaseCampMaxNum", "Int");
            SettingType.Add("BaseCampWorkerMaxNum", "Int");

            SettingType.Add("DropItemAliveMaxHours", "double");

            SettingType.Add("bAutoResetGuildNoOnlinePlayers", "Bool");
            SettingType.Add("AutoResetGuildTimeNoOnlinePlayers", "double");

            SettingType.Add("GuildPlayerMaxNum", "Int");

            SettingType.Add("PalEggDefaultHatchingTime", "double");
            SettingType.Add("WorkSpeedRate", "double");
            SettingType.Add("bIsMultiplay", "Bool");
            SettingType.Add("bIsPvP", "Bool");
            SettingType.Add("bCanPickupOtherGuildDeathPenaltyDrop", "Bool");


            SettingType.Add("bEnableNonLoginPenalty", "Bool");
            SettingType.Add("bEnableFastTravel", "Bool");
            SettingType.Add("bIsStartLocationSelectByMap", "Bool");
            SettingType.Add("bExistPlayerAfterLogout", "Bool");
            SettingType.Add("bEnableDefenseOtherGuildPlayer", "Bool");
            SettingType.Add("CoopPlayerMaxNum", "Int");
            SettingType.Add("ServerPlayerMaxNum", "Int");

            SettingType.Add("ServerName", "Val");
            SettingType.Add("ServerDescription", "Val");
            SettingType.Add("AdminPassword", "Val");
            SettingType.Add("ServerPassword", "Val");
            SettingType.Add("PublicPort", "Int");
            SettingType.Add("PublicIP", "Val");

            SettingType.Add("RCONEnabled", "Bool");
            SettingType.Add("RCONPort", "Int");

            SettingType.Add("Region", "Val");
            SettingType.Add("bUseAuth", "Bool");
            SettingType.Add("BanListURL", "Val");
        }

        public static string CheckType(string key, string val)
        {
            if (SettingType.ContainsKey(key))
            {
                string type = SettingType[key];
                if (type == "Val") {
                    return $"{key}=\"{val}\"";
                }
            }

            return $"{key}={val}";
        }
    }
}
