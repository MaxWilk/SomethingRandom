using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class ByteBrewSettingsUtility
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

	public static ByteBrewSettings GetOrCreateByteBrewSettings() 
	{
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
        }

		//create the settings file
		ByteBrewSettings settings = ScriptableObject.CreateInstance<ByteBrewSettings>();
		AssetDatabase.CreateAsset(settings, bytebrewSettingsPath);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		return settings;
	}

	[MenuItem("Window/ByteBrew/Create ByteBrew GameObject")]
	public static void CreateByteBrewGameObject()
	{

		// Make sure the file name is unique, in case an existing Prefab has the same name.
		Object bytePref = AssetDatabase.LoadAssetAtPath("Assets/ByteBrewSDK/Prefabs/ByteBrew.prefab", typeof(GameObject));

		// Create the new Prefab.
		PrefabUtility.InstantiatePrefab(bytePref);
		EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
	}

	[MenuItem("Window/ByteBrew/Select ByteBrew settings")]
	public static void SelectSettings()
	{
		//ByteBrewSettings asset = ScriptableObject.CreateInstance<ByteBrewSettings>();

		//AssetDatabase.CreateAsset(asset, "Assets/ByteBrewSDK/Resources/ByteBrewSettings.asset");

		ByteBrewSettings asset = SettingsInstance;

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}
