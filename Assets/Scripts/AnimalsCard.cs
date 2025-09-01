using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

/// <summary>
/// Represents a single animal card in the card-matching game.
/// Handles flipping animation, icon display, and click interaction.
/// </summary>
public class AnimalsCards : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image iconImage; // The UI Image component that displays the card sprite.

    [Header("Card Sprites")]
    public Sprite hiddenIconSprite;  // Sprite shown when the card is hidden (face-down).
    public Sprite iconSprite;        // Sprite shown when the card is revealed (face-up).

    [Header("Card State")]
    public bool isSelected; // Indicates if the card is currently selected/revealed.

    [Header("Controller Reference")]
    public MyCardsController controllerss; // Reference to the card game controller.

    /// <summary>
    /// Triggered when the player clicks on the card.
    /// Plays a click sound and notifies the game controller.
    /// </summary>
    public void OnCardsClick()
    {
        SoundManager.instance.PlayingSound("click");
        controllerss.SetSelected(this);
    }

    /// <summary>
    /// Assigns the given sprite as this card's face-up icon.
    /// </summary>
    /// <param name="sp">The sprite to set as the face-up icon.</param>
    public void SetIconSprite(Sprite sp)
    {
        iconSprite = sp;
    }

    /// <summary>
    /// Reveals the card by flipping animation and showing its icon.
    /// </summary>
    public void Show()
    {
        // Animate card flip
        Tween.Rotation(transform, new Vector3(0f, 180f, 0f), 0.2f);

        // Delay to simulate flip and then reveal the card face
        Tween.Delay(0.1f, () =>
        {
            iconImage.sprite = iconSprite;
            isSelected = true;
        });
    }

    /// <summary>
    /// Hides the card by flipping animation and showing the hidden icon.
    /// </summary>
    public void Hide()
    {
        // Animate card flip back to hidden state
        Tween.Rotation(transform, new Vector3(0f, 0f, 0f), 0.2f);

        // Delay to simulate flip and then hide the card face
        Tween.Delay(0.1f, () =>
        {
            iconImage.sprite = hiddenIconSprite;
            isSelected = false;
        });
    }
}

