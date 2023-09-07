using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public Color InteractHighlightColor
    {
        get => interactHighlightColor;
    }

    public Color LinkHighlightColor
    {
        get => linkHighlightColor;
    }

    public Color PickupHighlightColor
    {
        get => pickupHighlightColor;
    }

    public Vector2Int Resolution
    {
        get => resolution;
    }

    [SerializeField]
    private Color interactHighlightColor = new Color(0.5f, 0.9f, 0.7f, 1f);

    [SerializeField]
    private Color linkHighlightColor = new Color(0.34f, 0.81f, 1f, 1f);

    [SerializeField]
    private Color pickupHighlightColor = new Color(0.88f, 0.42f, 0.34f);

    [SerializeField]
    Vector2Int resolution = new Vector2Int(240, 160);
}
