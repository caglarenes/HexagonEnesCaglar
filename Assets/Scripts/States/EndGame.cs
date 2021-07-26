using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : Singleton<EndGame>, IState
{

    public void OnGroupSelect(HexagonGroup selectedGroup)
    {
        //Just ignore input
    }

    public void OnHexagonGroupSwipeClockwise()
    {
        //Just ignore input
    }

    public void OnHexagonGroupSwipeCounterClockwise()
    {
        //Just ignore input
    }

    void IState.Start()
    {
        UIManager.Instance.ShowEndGameScreen();
    }
}
