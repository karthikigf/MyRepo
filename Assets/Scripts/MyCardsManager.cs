using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls the overall card-matching game logic.
/// Handles card creation, matching, scoring, game states (win/lose),
/// and UI updates for score, timer, and panels.
/// </summary>
public class MyCardsController : MonoBehaviour
{
    // Grid settings
    private int row = 0;
    private int column = 0;
    private int totalcount = 0;

    [Header("Card Setup")]
    [SerializeField] AnimalsCards cardsPrefebs;  // Card prefab reference
    [SerializeField] Transform gridTransform;    // Parent transform for card grid
    public GridLayoutGroup gridLayoutGroup;      // Grid layout for arranging cards

    [Header("Animal Sprites")]
    [SerializeField] Sprite[] sprites;           // Pool of sprites to use
    private List<Sprite> spritePairs;            // Shuffled list of sprite pairs

    // Card selection tracking
    private AnimalsCards firstSelect;
    private AnimalsCards SecondsSelect;

    // Gameplay state
    private int matchCounts;                     // Tracks number of matched pairs
    private int score = 0;                       // Player’s current score

    [Header("Score UI")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;

    // Score persistence
    public int saved_score;
    public int failcount = 0;

    [Header("Game Timer")]
    [SerializeField] TMP_Text timerText;         // (Optional) countdown text
    [SerializeField] float gameTime = 60f;       // Default 60 sec countdown

    [Header("UI Panels")]
    [SerializeField] GameObject gamePanel;       // Active game UI
    [SerializeField] GameObject gameWin;         // Win screen panel
    [SerializeField] GameObject gameOverPanel;   // Game over panel

    [Header("Game Over UI")]
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text GameOverText;

    [Header("Other UI")]
    public Button playAgain;
    public TMP_Text countdownText;               // Temporary reveal countdown
    public float countdownTime = 10f;            // Countdown time in seconds

    // Unity Start
    void Start()
    {
        GetRowsandColums();

        if (gridLayoutGroup == null)
            gridLayoutGroup = GetComponent<GridLayoutGroup>();

        // Ensure grid uses fixed columns
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = column;

        // Prepare and spawn cards
        PrepareSprite(totalcount);
        CreateCards();

        // Show all cards briefly before hiding
        StartCoroutine(ShowAllCardsWithCountdown());

        // Button listeners
        playAgain.onClick.AddListener(playAgainOnClick);
    }

    /// <summary>
    /// Instantiates card objects with shuffled sprites.
    /// </summary>
    void CreateCards()
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            AnimalsCards cards = Instantiate(cardsPrefebs, gridTransform);
            cards.SetIconSprite(spritePairs[i]);
            cards.controllerss = this;
        }
    }

    /// <summary>
    /// Prepares sprite pairs based on grid count and shuffles them.
    /// </summary>
    private void PrepareSprite(int mycount)
    {
        spritePairs = new List<Sprite>();

        for (int i = 0; i < mycount; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]); // add pair
        }

        // Ensure even count
        if (spritePairs.Count % 2 != 0)
            spritePairs.RemoveAt(spritePairs.Count - 1);

        ShuffleSprites(spritePairs);
    }

    /// <summary>
    /// Shuffles the sprite list using Fisher-Yates algorithm.
    /// </summary>
    void ShuffleSprites(List<Sprite> spriteList)
    {
        for (int i = spriteList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Sprite temp = spriteList[i];
            spriteList[i] = spriteList[randomIndex];
            spriteList[randomIndex] = temp;
        }

        // Play shuffle sound
        SoundManager.instance.PlayingSound("win");
    }

    /// <summary>
    /// Displays game over or win panel with appropriate title.
    /// Saves score and disables main game panel.
    /// </summary>
    private void GameStatsPanel(string strTitle)
    {
        SoundManager.instance.PlayingSound("over");
        SaveGame();

        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        GameOverText.text = strTitle;
        finalScoreText.text = score.ToString();
    }

    /// <summary>
    /// Retrieves saved row/column values from PlayerPrefs.
    /// </summary>
    void GetRowsandColums()
    {
        row = PlayerPrefs.GetInt("row", 0);
        column = PlayerPrefs.GetInt("column", 0);
        totalcount = row * column / 2;

        Debug.Log("The value of rows and column is: " + totalcount);
    }

    /// <summary>
    /// Called when a card is selected.
    /// Manages first and second selection tracking.
    /// </summary>
    public void SetSelected(AnimalsCards cards)
    {
        if (!cards.isSelected)
        {
            cards.Show();

            if (firstSelect == null)
            {
                firstSelect = cards;
                return;
            }

            if (SecondsSelect == null)
            {
                SecondsSelect = cards;
                StartCoroutine(CheckMatching(firstSelect, SecondsSelect));

                firstSelect = null;
                SecondsSelect = null;
            }
        }
    }

    /// <summary>
    /// Coroutine to check if two selected cards match.
    /// Handles scoring, match counts, and game over logic.
    /// </summary>
    IEnumerator CheckMatching(AnimalsCards aSide, AnimalsCards bSide)
    {
        yield return new WaitForSeconds(0.3f);

        if (aSide.iconSprite == bSide.iconSprite)
        {
            // Matched
            matchCounts++;
            score += 10;
            UpdateScoreUI();
            SaveGame();

            // Win condition
            if (matchCounts >= spritePairs.Count / 2)
            {
                PrimeTween.Sequence.Create()
                    .Chain(PrimeTween.Tween.Scale(gridTransform, Vector3.one * 1.2f, 0.2f, ease: PrimeTween.Ease.OutBack))
                    .Chain(PrimeTween.Tween.Scale(gridTransform, Vector3.one, 0.1f));

                SoundManager.instance.PlayingSound("win");
                GameStatsPanel("You Rock!");
                UpdateScoreUI();
            }
        }
        else
        {
            // Not matched
            score -= 2;
            failcount += 1;

            // Clamp score
            if (score < 0) score = 0;

            // Game Over condition
            if (failcount == column)
            {
                GameStatsPanel("Game Over!");
                UpdateScoreUI();
            }

            aSide.Hide();
            bSide.Hide();
        }
    }

    /// <summary>
    /// Reloads menu scene when Play Again is clicked.
    /// </summary>
    void playAgainOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Reveals all cards for a short countdown, then hides them.
    /// </summary>
    IEnumerator ShowAllCardsWithCountdown()
    {
        countdownText.enabled = true;

        // Show all cards
        foreach (Transform child in gridTransform)
            child.GetComponent<AnimalsCards>().Show();

        int count = 3;
        while (count > 0)
        {
            yield return new WaitForSeconds(1f);
            count--;
            countdownText.text = count.ToString();
        }

        yield return new WaitForSeconds(1f);

        countdownText.enabled = false;

        // Hide all cards again
        foreach (Transform child in gridTransform)
            child.GetComponent<AnimalsCards>().Hide();
    }

    void Update()
    {
        // Continuously update saved score
        LoadGame();
    }

    /// <summary>
    /// Updates the score and high score UI text.
    /// Ensures no negative values are shown.
    /// </summary>
    private void UpdateScoreUI()
    {
        if (score < 0)
        {
            score = 0;
            scoreText.text = score.ToString();
            FixingNegativeText(scoreText);
        }
        else
        {
            scoreText.text = score.ToString();
        }

        if (saved_score < 0)
        {
            saved_score = 0;
            highScoreText.text = saved_score.ToString();
        }
        else
        {
            highScoreText.text = saved_score.ToString();
        }
    }

    /// <summary>
    /// Utility method to clamp integer to zero minimum.
    /// </summary>
    int ClampToZero(int value) => Mathf.Max(0, value);

    /// <summary>
    /// Fixes any negative number shown in UI text.
    /// </summary>
    private void FixingNegativeText(TMP_Text textUI)
    {
        string textValue = textUI.text.Replace("", "");
        if (int.TryParse(textValue, out int number))
        {
            number = Mathf.Max(0, number); // clamp
            textUI.text = number.ToString();
        }
    }

    /// <summary>
    /// Saves the current score to persistent storage.
    /// </summary>
    public void SaveGame()
    {
        MyPlayerData data = new MyPlayerData(score);
        PlayerSaveSystem.Save(data);
    }

    /// <summary>
    /// Loads saved score from persistent storage.
    /// </summary>
    public void LoadGame()
    {
        MyPlayerData data = PlayerSaveSystem.Load();
        if (data != null)
            saved_score = data.score;
    }
}
