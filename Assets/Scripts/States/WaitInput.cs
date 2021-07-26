using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitInput : Singleton<WaitInput>, IState
{

    public void OnGroupSelect(HexagonGroup selectedGroup)
    {
        HexagonSelector.Instance.SetSelected(selectedGroup);
    }

    public void OnHexagonGroupSwipeClockwise()
    {
        if (!HexagonSelector.Instance.IsSelected())
        {
            return;
        }

        GameManager.Instance.ChangeState(TryClockwise.Instance);
    }

    public void OnHexagonGroupSwipeCounterClockwise()
    {
        if (!HexagonSelector.Instance.IsSelected())
        {
            return;
        }

        GameManager.Instance.ChangeState(TryCounterClockwise.Instance);
    }

    void IState.Start()
    {

    }

}
