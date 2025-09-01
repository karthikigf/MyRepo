using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicLayout : MonoBehaviour
{
    [Header("Text Fields")]
    public Text numOfrows;
    public Text numOfcolumns;

    [Header("UI Canvas")]
    public RectTransform panelColumn;
    public GameObject cell;
    public Transform Board;

    private int rowSize;
    private int columnSize;


    void Initialize()
    {
      //  rowSize = numOfrows.text != "" ? int.Parse(numOfrows.text) : 4;

      //  columnSize = numOfcolumns.text != "" ? int.Parse(numOfcolumns.text) : 4;


        rowSize =3;
        columnSize =  4;

      //  GenerateBoard();
    }




  
    void ClearBoard()
    {
        for(int i = 0; i < Board.childCount; i++)
        {
            Destroy(Board.GetChild(i).gameObject);
        }
    }



    private void Start()
    {
        GenerateBoard();
    }

    public void GenerateBoard()
    {
        ClearBoard();
        Initialize();

        RectTransform colParent; 

        for(int colIndex = 0; colIndex < columnSize; colIndex++)
        {
            colParent = Instantiate(panelColumn, Board);

            for(int rowIndex = 0; rowIndex <rowSize; rowIndex++)
            {

                Instantiate(cell , colParent);

            }
            

        }
    }


}
