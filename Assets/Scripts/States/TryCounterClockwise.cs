using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryCounterClockwise : Singleton<TryCounterClockwise>, IState
{

    public float turnTime = 0.35f;

    public void OnGroupSelect(HexagonGroup selectedGroup)
    {

    }

    public void OnHexagonGroupSwipeClockwise()
    {

    }

    public void OnHexagonGroupSwipeCounterClockwise()
    {

    }

    void IState.Start()
    {
        StartCoroutine(CheckCounterClockwise());
    }

    IEnumerator CheckCounterClockwise()
    {
        for (int i = 0; i < 3; i++)
        {
            HexagonSelector.Instance.RotateCounterClockwise(turnTime);
            yield return new WaitForSeconds(turnTime);
            if (!BoardManager.Instance.CheckAllTiles())
            {
                BoardManager.Instance.UpdateBombHexagons();
                GameManager.Instance.ChangeState(FindMatches.Instance);
                yield break;
            }

            yield return new WaitForSeconds(turnTime);
        }

        GameManager.Instance.ChangeState(WaitInput.Instance);
    }
}
