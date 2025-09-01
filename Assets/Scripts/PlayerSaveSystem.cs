using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerSaveSystem
{
    private static string savePlayersData = Application.persistentDataPath + "/scorecard.json";

    public static void Save(MyPlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePlayersData, json);
        Debug.Log("Game Saved at: " + savePlayersData);
    }

    public static MyPlayerData Load()
    {
        if (File.Exists(savePlayersData))
        {
            string json = File.ReadAllText(savePlayersData);
            MyPlayerData data = JsonUtility.FromJson<MyPlayerData>(json);
            Debug.Log("Game Loaded");
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found!");
            return null;
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(savePlayersData))
        {
            File.Delete(savePlayersData);
            Debug.Log("Save file deleted.");
        }
    }
}