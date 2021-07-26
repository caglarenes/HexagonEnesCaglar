using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonTile : MonoBehaviour
{
    public int hexagonX;
    public int hexagonY;

    public Hexagon onHexagon;
    public List<HexagonTile> neighbors = new List<HexagonTile>(6);

    public void FillTile()
    {
        /*
        if (onHexagon != null)
        {
            Destroy(onHexagon.GetGameObject());
        }
        */

        GameObject createdHexagon = Instantiate(BoardManager.Instance.hexagonPrefab, transform.position, Quaternion.identity);
        createdHexagon.transform.SetParent(BoardManager.Instance.hexagonsParent.transform, true);

        var hexagonComponent = createdHexagon.GetComponent<Hexagon>();
        onHexagon = hexagonComponent;
    }

    public void MakeBomb()
    {
        if (onHexagon != null)
        {
            Destroy(onHexagon.GetGameObject());
        }

        GameObject createdHexagon = Instantiate(BoardManager.Instance.hexagonBombPrefab, transform.position, Quaternion.identity);
        createdHexagon.transform.SetParent(BoardManager.Instance.hexagonsParent.transform, true);

        var hexagonComponent = createdHexagon.GetComponent<Hexagon>();
        onHexagon = hexagonComponent;
    }

    public static Vector3 PositionCalculator(float hexagonX, float hexagonY)
    {
        return HexagonGeometry.FindPosition(hexagonX, hexagonY);
    }

    public void FindNeighbors()
    {
        if (hexagonX % 2 == 1)
        {
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX, hexagonY + 1]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX + 1, hexagonY]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX + 1, hexagonY - 1]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX, hexagonY - 1]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX - 1, hexagonY - 1]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX - 1, hexagonY]);
        }
        else
        {
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX, hexagonY + 1]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX + 1, hexagonY + 1]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX + 1, hexagonY]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX, hexagonY - 1]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX - 1, hexagonY]);
            neighbors.Add(BoardManager.Instance.hexagonTilesArray[hexagonX - 1, hexagonY + 1]);
        }
    }

    public void FallTiles()
    {
        if (onHexagon != null)
        {
            return;
        }

        for (int i = hexagonY + 1; i < BoardManager.Instance.boardSizeY; i++)
        {
            if (BoardManager.Instance.hexagonTilesArray[hexagonX, i].onHexagon != null)
            {
                onHexagon = BoardManager.Instance.hexagonTilesArray[hexagonX, i].onHexagon;
                BoardManager.Instance.hexagonTilesArray[hexagonX, i].onHexagon = null;
                return;
            }
        }
    }
}
