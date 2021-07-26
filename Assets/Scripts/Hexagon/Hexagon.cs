using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hexagon
{
    GameObject GetGameObject();
    public void TurnToPoint();
    public Color hexagonColor {get; set;}
    public void OnRotate();
}
