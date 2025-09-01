using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MyCardsController : MonoBehaviour
{

    // Grid row and column
    int row = 0;
    int column = 0;
    int totalcount = 0;

    //Setrup cards and Grid

    [SerializeField] AnimalsCards cardsPrefebs;
    [SerializeField] Transform gridTransform;
    public GridLayoutGroup gridLayoutGroup;

    // Animal Sprites Array
    [SerializeField] Sprite[] sprites;
    private List<Sprite> spritePairs;

    // Cards first and Seconds
    AnimalsCards firstSelect;
    AnimalsCards SecondsSelect;

    int matchCounts;
    private int score = 0;

    // UI Score Update
    [Header("UI Elements")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;


    // Load Score
    public int saved_score;
    public int failcount = 0;

    // Show Cards Count Down
    [Header("Game Timer")]
    [SerializeField] TMP_Text timerText;
    [SerializeField] float gameTime = 60f;   // default 60 sec countdown

    // Menu
    [Header("UI Panels")]
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject gameWin;
    [SerializeField] GameObject gameOverPanel;

    [Header("Game Over UI")]
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text GameOverText;


    public Button playAgain;

    // Show Cards 3 Sec
    public TMP_Text countdownText;
    // countdown start in seconds
    public float countdownTime = 10f;

    // Start is called before the first frame update
    void Start()
    {


        GetRowsandColums();

        if (gridLayoutGroup == null)
        {
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        // Set a fixed column count
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = column;

        PrepareSprite(totalcount);
        CreateCards();
        StartCoroutine(ShowAllCardsWithCountdown());
        playAgain.onClick.AddListener(playAgainOnClick);

    }



    // Create Cards
    void CreateCards()
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            AnimalsCards cards = Instantiate(cardsPrefebs, gridTransform);
            cards.SetIconSprite(spritePairs[i]);
            cards.controllerss = this;

        }
    }

    // Prepare cards and suffle
    private void PrepareSprite(int mycount)
    {
        spritePairs = new List<Sprite>();

        for (int i = 0; i < mycount; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);

        }

        if (spritePairs.Count % 2 != 0)
            spritePairs.RemoveAt(spritePairs.Count - 1);

        ShuffleSprites(spritePairs);

    }

    // Shuffle Cards
    void ShuffleSprites(List<Sprite> spriteList)
    {

        for (int i = spriteList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Sprite temp = spriteList[i];
            spriteList[i] = spriteList[randomIndex];
            spriteList[randomIndex] = temp;


        }

        SoundManager.instance.PlayingSound("win");

    }

    // Gamer Over
    private void GameStatsPanel(string strTitle)
    {
        // Game Over Sound
        SoundManager.instance.PlayingSound("over");
        
        // Save Score
        SaveGame();

        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        GameOverText.text = strTitle;

        finalScoreText.text = score.ToString();
    }


    // Get Rows and Columns
    void GetRowsandColums()
    {
        row = PlayerPrefs.GetInt("row", 0);
        column = PlayerPrefs.GetInt("column", 0);
        totalcount = row * column / 2;
        Debug.Log("The value of rows and column is: " + totalcount);

    }

    // Check Cards
    public void SetSelected(AnimalsCards cards)
    {


        if (cards.isSelected == false)
        {
            cards.show();

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


    //Check Matching Card
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

            if (matchCounts >= spritePairs.Count / 2)
            {
                PrimeTween.Sequence.Create().Chain(PrimeTween.Tween.Scale(gridTransform, Vector3.one * 1.2f, 0.2f, ease: PrimeTween.Ease.OutBack).Chain(PrimeTween.Tween.Scale(gridTransform, Vector3.one, 0.1f)));

                // Winning Sound
                SoundManager.instance.PlayingSound("win");

                GameStatsPanel("You Rock!");
                UpdateScoreUI();

            }

        }
        else
        {
            // flip them back
            // mismatch
            score -= 2;
            failcount += 1;

            // Check - value
            if (score < 0) score = 0;

            if (failcount == column)
            {
                // GameOver

                GameStatsPanel("Game Over!");
                UpdateScoreUI();


            }



            aSide.Hide();
            bSide.Hide();
        }
    }

    // Play again 
    void playAgainOnClick()
    {
        SceneManager.LoadScene("Menu");

    }

    // Show Card Details 

    IEnumerator ShowAllCardsWithCountdown()
    {
        countdownText.enabled = true;

        // Show all
        foreach (Transform child in gridTransform)
            child.GetComponent<AnimalsCards>().show();


        int count = 3;



        while (count > 0)
        {

            yield return new WaitForSeconds(1f);
            count--;

            countdownText.text = count.ToString();


        }

        yield return new WaitForSeconds(1f);

        countdownText.enabled = false;
        // Hide all
        foreach (Transform child in gridTransform)
            child.GetComponent<AnimalsCards>().Hide();

    }


    // Update is called once per frame
    void Update()
    {

        // Update Saved Score
        LoadGame();


    }
    
    // Update Score
    private void UpdateScoreUI()
    {
        
        if (score < 0)
        {
            score = 0;

            scoreText.text = "" +score;
            FixingNegativeText(scoreText);

        }
        else
        {
            scoreText.text = "" +score;

        }
       


        if (saved_score < 0)
        {
            saved_score = 0;
            highScoreText.text = "" + saved_score;

        }
        else
        {
            highScoreText.text = "" + saved_score;
        }
         



    }

    int ClampToZero(int value)
    {
        return Mathf.Max(0, value);
    }


    private void FixingNegativeText(TMP_Text textUI)
    {
        string textValue = textUI.text.Replace("", "");
        if (int.TryParse(textValue, out int number))
        {
            number = Mathf.Max(0, number); // clamp
            textUI.text = "" + number;
        }
    }


    // Update Saved Score

    public void SaveGame()
    {
        MyPlayerData data = new MyPlayerData(score);
        PlayerSaveSystem.Save(data);
    }


    // Update Saved Score
    public void LoadGame()
    {
        MyPlayerData data = PlayerSaveSystem.Load();
        if (data != null)
        {
            saved_score = data.score;

        }
    }



}
