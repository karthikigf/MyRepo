using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Handles saving, loading, and deleting player data in JSON format.
/// Uses Unity's persistent data path to store a file called "scorecard.json".
/// </summary>
public class PlayerSaveSystem
{
    /// <summary>
    /// The full file path for saving the player's score data.
    /// Stored in Application.persistentDataPath for cross-platform safety.
    /// </summary>
    private static string savePlayersData = Application.persistentDataPath + "/scorecard.json";

    /// <summary>
    /// Saves the given player data to a JSON file.
    /// </summary>
    /// <param name="data">The player data object to serialize and save.</param>
    public static void Save(MyPlayerData data)
    {
        string json = JsonUtility.ToJson(data, true); // Convert to JSON (pretty printed)
        File.WriteAllText(savePlayersData, json);     // Write to file
        Debug.Log("Game Saved at: " + savePlayersData);
    }

    /// <summary>
    /// Loads the saved player data from the JSON file.
    /// </summary>
    /// <returns>
    /// A <see cref="MyPlayerData"/> object if the file exists, otherwise null.
    /// </returns>
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

    /// <summary>
    /// Deletes the saved game file if it exists.
    /// </summary>
    public static void DeleteSave()
    {
        if (File.Exists(savePlayersData))
        {
            File.Delete(savePlayersData);
            Debug.Log("Save file deleted.");
        }
    }
}
