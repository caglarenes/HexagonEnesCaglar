using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHexagon : MonoBehaviour, Hexagon
{
    public Color hexagonColor { get; set; }
    public SpriteRenderer spriteRenderer;
    public static int point = 5;
    public int moveLeft = 5;

    public TextMesh moveLeftText;
    public GameObject bombImage;

    void Awake()
    {
        hexagonColor = HexagonColors.Instance.RandomColor();
        spriteRenderer.color = hexagonColor;
        moveLeftText.text = moveLeft.ToString();
        BoardManager.Instance.AddBombToList(this);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void TurnToPoint()
    {
        ScoreManager.Instance.AddPoint(point);
        BoardManager.Instance.RemoveBombFromList(this);
        Destroy(gameObject);
    }

    public void UpdateMoves()
    {
        moveLeft--;
        moveLeftText.text = moveLeft.ToString();
    }

    public void OnRotate()
    {
        bombImage.transform.rotation = Quaternion.Euler(Vector3.zero);
        moveLeftText.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}

