using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

namespace Kill_Sound_GoldKingZ.Config
{
    public static class Configs
    {
        public static class Shared {
            public static string? CookiesFolderPath { get; set; }
        }
        
        private static readonly string ConfigDirectoryName = "config";
        private static readonly string ConfigFileName = "config.json";
        private static readonly string jsonFilePath = "Kill_Settings.json";
        private static string? _configFilePath;
        private static string? _jsonFilePath;
        private static ConfigData? _configData;

        private static readonly JsonSerializerOptions SerializationOptions = new()
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            WriteIndented = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };

        public static bool IsLoaded()
        {
            return _configData is not null;
        }

        public static ConfigData GetConfigData()
        {
            if (_configData is null)
            {
                throw new Exception("Config not yet loaded.");
            }
            
            return _configData;
        }

        public static ConfigData Load(string modulePath)
        {
            var configFileDirectory = Path.Combine(modulePath, ConfigDirectoryName);
            if(!Directory.Exists(configFileDirectory))
            {
                Directory.CreateDirectory(configFileDirectory);
            }
            _jsonFilePath = Path.Combine(configFileDirectory, jsonFilePath);
            Helper.CreateDefaultWeaponsJson(_jsonFilePath);

            _configFilePath = Path.Combine(configFileDirectory, ConfigFileName);
            if (File.Exists(_configFilePath))
            {
                _configData = JsonSerializer.Deserialize<ConfigData>(File.ReadAllText(_configFilePath), SerializationOptions);
            }
            else
            {
                _configData = new ConfigData();
            }

            if (_configData is null)
            {
                throw new Exception("Failed to load configs.");
            }

            SaveConfigData(_configData);
            
            return _configData;
        }

        private static void SaveConfigData(ConfigData configData)
        {
            if (_configFilePath is null)
            {
                throw new Exception("Config not yet loaded.");
            }
            string json = JsonSerializer.Serialize(configData, SerializationOptions);

            json = "// Note: To Use Modify Version And Lower Volume \n// Download https://github.com/Source2ZE/MultiAddonManager  With Gold KingZ WorkShop \n// https://steamcommunity.com/sharedfiles/filedetails/?id=3230015783\n// mm_extra_addons 3230015783\n// OtherWise Use Normal Sounds https://github.com/oqyh/cs2-Kill-Sound-GoldKingZ/blob/main/sounds/sounds.txt \n\n" + json;

            File.WriteAllText(_configFilePath, json);
        }

        public class ConfigData
        {
            public bool KS_EnableQuakeSounds { get; set; }
            
            public string empty { get; set; }
            public string KS_HeadShotKillSoundPath { get; set; }
            public string KS_BodyKillSoundPath { get; set; }
            public string KS_HeadShotHitSoundPath { get; set; }
            public string KS_BodyHitSoundPath { get; set; }
            public string empty2 { get; set; }
            public string Information_For_You_Dont_Delete_it { get; set; }
            
            public ConfigData()
            {
                KS_EnableQuakeSounds = false;
                empty = "-----------------------------------------------------------------------------------";
                KS_HeadShotKillSoundPath = "sounds/GoldKingZ/Training/bell_normal.vsnd_c";
                KS_BodyKillSoundPath = "sounds/GoldKingZ/Training/timer_bell.vsnd_c";
                KS_HeadShotHitSoundPath = "sounds/GoldKingZ/Training/bell_impact.vsnd_c";
                KS_BodyHitSoundPath = "sounds/GoldKingZ/Training/timer_bell.vsnd_c";
                empty2 = "-----------------------------------------------------------------------------------";
                Information_For_You_Dont_Delete_it = " Vist  [https://github.com/oqyh/cs2-Kill-Sound-GoldKingZ/tree/main?tab=readme-ov-file#-configuration-] To Understand All Above";
            }
        }
    }
}