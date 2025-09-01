using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;


public class AnimalsCards : MonoBehaviour
{


    [SerializeField] private Image iconImage;

    public Sprite hiddenIconSprite;
    public Sprite iconSprite;

    public bool isSelected;
    public MyCardsController controllerss;


    // Cards Click
    public void OnCardsClick()
    {

        SoundManager.instance.PlayingSound("click");

        controllerss.SetSelected(this);
    }

    public void SetIconSprite(Sprite sp)
    {
        iconSprite = sp;

    }

    public void show()
    {
        // Tween  Flip Cards

        Tween.Rotation(transform, new Vector3(0f, 180f, 0f), 0.2f);
        Tween.Delay(0.1f, () => { iconImage.sprite = iconSprite; isSelected = true; });

        //iconImage.sprite = iconSprite; isSelected = true;

    }

    public void Hide()
    {

        // Tween  Flip Cards

        Tween.Rotation(transform, new Vector3(0f, 0f, 0f), 0.2f);

        Tween.Delay(0.1f, () => {
            iconImage.sprite = hiddenIconSprite;

            isSelected = false;
        });


    }





}
