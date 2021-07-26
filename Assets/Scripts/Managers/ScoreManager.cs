using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int currentPoint;
    public int bombPoint = 10;
    public int currentBombPoint = 0;

    public bool willAddBomb = false;

    public void AddPoint(int addedPoint)
    {
        currentPoint += addedPoint;
        currentBombPoint += addedPoint;
        BombCheck();
        UIManager.Instance.UpdatePoint(currentPoint);
    }

    public void BombCheck()
    {
        if (currentBombPoint >= bombPoint)
        {
            willAddBomb = true;
            currentBombPoint -= bombPoint;
        }
    }

    public void BombAdded()
    {
        willAddBomb = false;
    }
}
