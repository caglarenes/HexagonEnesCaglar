using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexagonGeometry
{
    public static float hexagonHorizontalSize = 1;
    public static float offset = 0.05f;

    public static float hexagonVerticalSize = 0f;

    static HexagonGeometry()
    {
        hexagonVerticalSize = hexagonHorizontalSize / 2 * (float)Math.Sqrt(3);
    }

    public static float CalculateVerticalSize()
    {
        return (hexagonVerticalSize + offset) * BoardManager.Instance.boardSizeY;
    }

    public static float CalculateHorizontalSize()
    {
        return (hexagonHorizontalSize + offset) + (((hexagonHorizontalSize + offset) * (float)(3f / 4f)) * BoardManager.Instance.boardSizeX - 1);
    }

    public static Vector3 FindPosition(float X, float Y)
    {
        Vector3 position = new Vector3(((hexagonHorizontalSize + offset) * (float)(3f / 4f)) * X, (hexagonVerticalSize + offset) * Y, 0f);

        if (X % 2 == 1)
        {
            position -= new Vector3(0, (hexagonVerticalSize + offset) / 2, 0);
        }

        return position;
    }
}
