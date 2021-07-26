using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartHexagon : MonoBehaviour, Hexagon
{
    public Color hexagonColor { get; set; }
    public SpriteRenderer spriteRenderer;
    public static int point = 5;

    void Awake()
    {
        hexagonColor = HexagonColors.Instance.RandomColor();
        spriteRenderer.color = hexagonColor;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void TurnToPoint()
    {
        ScoreManager.Instance.AddPoint(point);
        Destroy(gameObject);
    }

    public void OnRotate()
    {
        
    }
}

