using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonColors : Singleton<HexagonColors>
{
    public Color[] hexagonColors;

    public Color RandomColor()
    {
        return hexagonColors[Random.Range(0, hexagonColors.Length)];
    }
}
