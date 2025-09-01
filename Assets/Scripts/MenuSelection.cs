using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour
{

    public Button btn2x2;
    public Button btn2x3;
    public Button btn3x4;
    public Button btn4x4;
    public Button btn5x6;

    // Start is called before the first frame update
    void Start()
    {
        btn2x2.onClick.AddListener(playAgain_2x2_OnClick);
        btn2x3.onClick.AddListener(playAgain_2x3_OnClick);
        btn3x4.onClick.AddListener(playAgain_3x4_OnClick);
        btn4x4.onClick.AddListener(playAgain_4x4_OnClick);
        btn5x6.onClick.AddListener(playAgain_5x6_OnClick);

    }


    void playAgain_2x2_OnClick()
    {
        PlayerPrefs.SetInt("row", 2);
        PlayerPrefs.SetInt("column", 2);

        SceneManager.LoadScene("LiveGame");

    }


    void playAgain_2x3_OnClick()
    {
        PlayerPrefs.SetInt("row", 2);
        PlayerPrefs.SetInt("column", 3);

        

        SceneManager.LoadScene("LiveGame");

    }

    void playAgain_3x4_OnClick()
    {
        PlayerPrefs.SetInt("row", 3);
        PlayerPrefs.SetInt("column", 4);



        SceneManager.LoadScene("LiveGame");

    }

    void playAgain_4x4_OnClick()
    {
        PlayerPrefs.SetInt("row", 4);
        PlayerPrefs.SetInt("column", 4);



        SceneManager.LoadScene("LiveGame");

    }
    void playAgain_5x6_OnClick()
    {
        PlayerPrefs.SetInt("row", 5);
        PlayerPrefs.SetInt("column", 6);

        SceneManager.LoadScene("LiveGame");

    }


    void playAgainOnClick()
    {
        SceneManager.LoadScene("Menu");

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
