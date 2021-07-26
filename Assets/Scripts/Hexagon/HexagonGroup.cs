using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexagonGroup : MonoBehaviour
{

    public List<HexagonTile> hexagons = new List<HexagonTile>(3);
    public PolygonCollider2D coll2D;

    public bool isLeft = false;

    public static Vector3 PositionRightCalculator(float hexagonX, float hexagonY)
    {
        var tempPosition = HexagonGeometry.FindPosition(hexagonX, hexagonY);

        //
        tempPosition += new Vector3((HexagonGeometry.hexagonHorizontalSize + HexagonGeometry.offset) / 2, 0f, 0f);

        if (hexagonX % 2 == 1)
        {
            tempPosition += new Vector3(0f, (HexagonGeometry.hexagonVerticalSize + HexagonGeometry.offset), 0f);
        }

        return tempPosition;
    }

    public static Vector3 PositionLeftCalculator(float hexagonX, float hexagonY)
    {
        var tempPosition = HexagonGeometry.FindPosition(hexagonX, hexagonY);

        tempPosition -= new Vector3((HexagonGeometry.hexagonHorizontalSize + HexagonGeometry.offset) / 2, 0f, 0f);

        if (hexagonX % 2 == 1)
        {
            tempPosition += new Vector3(0f, (HexagonGeometry.hexagonVerticalSize + HexagonGeometry.offset), 0f);
        }

        return tempPosition;
    }

    public void AddHexagon(HexagonTile hex)
    {
        hexagons.Add(hex);
    }

    public void SetGroupMembers(int i, int k, bool RightSide)
    {
        if (i % 2 == 0 && RightSide)
        {
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i, k]);
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i + 1, k + 1]);
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i + 1, k]);
        }
        else if (i % 2 == 0 && !RightSide)
        {
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i, k]);
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i - 1, k]);
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i - 1, k + 1]);

        }
        else if ((i % 2 == 1 && RightSide))
        {
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i, k + 1]);
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i + 1, k + 1]);
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i + 1, k]);
        }
        else if (i % 2 == 1 && !RightSide)
        {
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i, k + 1]);
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i - 1, k]);
            AddHexagon(BoardManager.Instance.hexagonTilesArray[i - 1, k + 1]);
        }
        else
        {
            Debug.LogError("Calculation Problem");
        }
    }

    public bool CheckGroup(bool destroy = false)
    {
        foreach (var item in hexagons)
        {
            if (item.onHexagon is null)
            {
                return false;
            }
        }

        if ((hexagons[0].onHexagon.hexagonColor == hexagons[1].onHexagon.hexagonColor) && (hexagons[1].onHexagon.hexagonColor == hexagons[2].onHexagon.hexagonColor))
        {
            if (destroy)
            {
                foreach (var item in hexagons)
                {
                    Destroy(item.onHexagon.GetGameObject());
                    item.onHexagon = null;
                }
            }

            return true;
        }

        return false;
    }

    public void GroupClick()
    {
        InputManager.Instance.GroupSelect(this);
    }

    public void RotateClockwise(float turnTime)
    {
        SetHexagonsChild();
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, -120), turnTime, RotateMode.Fast).OnComplete(ReleaseHexagons).SetEase(Ease.InOutSine).OnUpdate(RotateUpdateHexagons);
        SwapItems(true);
    }

    public void RotateCounterClockwise(float turnTime)
    {
        SetHexagonsChild();
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 120), turnTime, RotateMode.Fast).OnComplete(ReleaseHexagons).SetEase(Ease.InOutSine).OnUpdate(RotateUpdateHexagons);
        SwapItems(false);
    }

    public void CreateCollider()
    {
        var tempArray = new Vector2[hexagons.Count];
        for (int i = 0; i < hexagons.Count; i++)
        {
            var temp3 = transform.InverseTransformPoint(hexagons[i].transform.position);
            Vector2 temp2D = new Vector2(temp3.x, temp3.y);

            tempArray[i] = temp2D;
        }

        coll2D.SetPath(0, tempArray);
    }

    public void SetHexagonsChild()
    {
        foreach (var item in hexagons)
        {
            item.onHexagon.GetGameObject().transform.SetParent(transform, true);
        }
    }

    public void ReleaseHexagons()
    {
        foreach (var item in hexagons)
        {
            item.onHexagon.GetGameObject().transform.SetParent(BoardManager.Instance.hexagonsParent.transform, true);
        }
    }

    public void RotateUpdateHexagons()
    {
        foreach (var item in hexagons)
        {
            item.onHexagon.OnRotate();
        }
    }

    public void SwapItems(bool clockwise = true)
    {
        if (clockwise)
        {
            var tempItem = hexagons[2].onHexagon;
            hexagons[2].onHexagon = hexagons[1].onHexagon;
            hexagons[1].onHexagon = hexagons[0].onHexagon;
            hexagons[0].onHexagon = tempItem;
        }
        else
        {
            var tempItem = hexagons[2].onHexagon;
            hexagons[2].onHexagon = hexagons[0].onHexagon;
            hexagons[0].onHexagon = hexagons[1].onHexagon;
            hexagons[1].onHexagon = tempItem;
        }
    }
}