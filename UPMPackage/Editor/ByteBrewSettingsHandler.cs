using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class ByteBrewSettingsHandler
{
    private static ByteBrewSettings _settingsInstance = null;
    public static ByteBrewSettings SettingsInstance {
        get {
            if (_settingsInstance == null) {
                _settingsInstance = GetOrCreateByteBrewSettings();
            }
            return _settingsInstance;
        }
    }

    public static ByteBrewSettings GetOrCreateByteBrewSettings() {
        string bytebrewSettingsDirPath = Application.dataPath + "/ByteBrewSDK/Resources";
        string bytebrewSettingsPath = bytebrewSettingsDirPath + "/ByteBrewSettings.asset";
        //if settings file exists, load and return it
        if (File.Exists(bytebrewSettingsPath)) {
            return AssetDatabase.LoadAssetAtPath<ByteBrewSettings>(bytebrewSettingsPath);
        }

        //if settings file does not exist, create it
        //first, check if the Resources folder exists
        if (!Directory.Exists(bytebrewSettingsDirPath)) {
            Directory.CreateDirectory(bytebrewSettingsDirPath);
            AssetDatabase.Refresh();
        }

        //create the settings file
        ByteBrewSettings settings = ScriptableObject.CreateInstance<ByteBrewSettings>();
        AssetDatabase.CreateAsset(settings, bytebrewSettingsPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return settings;
    }
}
