using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the player data that will be saved and loaded.
/// Currently stores the player's score, but can be expanded with more fields
/// such as level, health, unlocked items, etc.
/// </summary>
[System.Serializable] // Required for Unity's JsonUtility to serialize/deserialize
public class MyPlayerData
{
    /// <summary>
    /// The player's score value.
    /// </summary>
    public int score;

    /// <summary>
    /// Creates a new instance of <see cref="MyPlayerData"/> with a given score.
    /// </summary>
    /// <param name="score">The player's current score.</param>
    public MyPlayerData(int score)
    {
        this.score = score;
    }
}
