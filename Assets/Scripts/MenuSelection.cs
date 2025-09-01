using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles menu button selections for different game board sizes.
/// When a button is clicked, it saves the row/column size to PlayerPrefs 
/// and loads the gameplay scene.
/// </summary>
public class MenuSelection : MonoBehaviour
{
    [Header("Grid Size Buttons")]
    [Tooltip("Button for starting a 2x2 grid game.")]
    public Button btn2x2;

    [Tooltip("Button for starting a 2x3 grid game.")]
    public Button btn2x3;

    [Tooltip("Button for starting a 3x4 grid game.")]
    public Button btn3x4;

    [Tooltip("Button for starting a 4x4 grid game.")]
    public Button btn4x4;

    [Tooltip("Button for starting a 5x6 grid game.")]
    public Button btn5x6;

    /// <summary>
    /// Unity's Start method, called on the first frame.
    /// Sets up button click listeners to call their respective methods.
    /// </summary>
    void Start()
    {
        btn2x2.onClick.AddListener(playAgain_2x2_OnClick);
        btn2x3.onClick.AddListener(playAgain_2x3_OnClick);
        btn3x4.onClick.AddListener(playAgain_3x4_OnClick);
        btn4x4.onClick.AddListener(playAgain_4x4_OnClick);
        btn5x6.onClick.AddListener(playAgain_5x6_OnClick);
    }

    /// <summary>
    /// Starts a new game with a 2x2 grid.
    /// </summary>
    void playAgain_2x2_OnClick()
    {
        PlayerPrefs.SetInt("row", 2);
        PlayerPrefs.SetInt("column", 2);
        SceneManager.LoadScene("LiveGame");
    }

    /// <summary>
    /// Starts a new game with a 2x3 grid.
    /// </summary>
    void playAgain_2x3_OnClick()
    {
        PlayerPrefs.SetInt("row", 2);
        PlayerPrefs.SetInt("column", 3);
        SceneManager.LoadScene("LiveGame");
    }

    /// <summary>
    /// Starts a new game with a 3x4 grid.
    /// </summary>
    void playAgain_3x4_OnClick()
    {
        PlayerPrefs.SetInt("row", 3);
        PlayerPrefs.SetInt("column", 4);
        SceneManager.LoadScene("LiveGame");
    }

    /// <summary>
    /// Starts a new game with a 4x4 grid.
    /// </summary>
    void playAgain_4x4_OnClick()
    {
        PlayerPrefs.SetInt("row", 4);
        PlayerPrefs.SetInt("column", 4);
        SceneManager.LoadScene("LiveGame");
    }

    /// <summary>
    /// Starts a new game with a 5x6 grid.
    /// </summary>
    void playAgain_5x6_OnClick()
    {
        PlayerPrefs.SetInt("row", 5);
        PlayerPrefs.SetInt("column", 6);
        SceneManager.LoadScene("LiveGame");
    }

    /// <summary>
    /// Returns to the main menu scene.
    /// </summary>
    void playAgainOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Unity's Update method, called once per frame.
    /// Currently unused but kept for future extensions.
    /// </summary>
    void Update()
    {

    }
}
