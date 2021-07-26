using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetupGame : Singleton<SetupGame>, IState
{
    public float hexAnimTime = 0.5f;
    public float fillTime = 2.5f;

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
        BoardManager.Instance.SetupBoard();
        HexagonSelector.Instance.HideSelector();
        UIManager.Instance.HideEndGameScreen();
        StartCoroutine(AnimateHexagons());
    }

    IEnumerator AnimateHexagons()
    {
        float count = 0;
        float totalCount = BoardManager.Instance.boardSizeX + BoardManager.Instance.boardSizeY;

        for (int i = 0; i < BoardManager.Instance.boardSizeY; i++)
        {
            for (int k = 0; k < BoardManager.Instance.boardSizeX; k++)
            {
                var tempY = BoardManager.Instance.hexagonTilesArray[k, i].onHexagon.GetGameObject().transform.position.y;

                count = i + k;
                //Randomize
                count *= Random.Range(0.90f, 1.20f);
                BoardManager.Instance.hexagonTilesArray[k, i].onHexagon.GetGameObject().transform.position += Vector3.up * 2 * HexagonGeometry.CalculateVerticalSize();
                BoardManager.Instance.hexagonTilesArray[k, i].onHexagon.GetGameObject().transform.DOMoveY(tempY, hexAnimTime).SetDelay((count / totalCount) * 3f).SetEase(Ease.InOutCirc);
            }
        }
        yield return new WaitForSeconds(hexAnimTime + fillTime);
        GameManager.Instance.ChangeState(WaitInput.Instance);
    }
}
