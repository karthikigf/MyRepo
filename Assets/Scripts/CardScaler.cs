using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CardScaler : MonoBehaviour
{
    public Vector2 aspectRatio = new Vector2(2, 3);

    private AspectRatioFitter fitter;

    void Awake()
    {
        fitter = gameObject.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        fitter.aspectRatio = aspectRatio.x / aspectRatio.y;
    }
}
