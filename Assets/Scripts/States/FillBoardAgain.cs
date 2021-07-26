using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FillBoardAgain : Singleton<FillBoardAgain>, IState
{
    public float fillTime = 0.5f;

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
        StartCoroutine(FillBoard());
    }

    List<HexagonTile> FindEmptyTiles()
    {
        var emptyTiles = new List<HexagonTile>();
        foreach (var item in BoardManager.Instance.hexagonTilesList)
        {
            if (item.onHexagon is null)
            {
                emptyTiles.Add(item);
            }
        }

        return emptyTiles;
    }

    void FillEmptyTiles(List<HexagonTile> emptyTiles)
    {
        foreach (var item in emptyTiles)
        {
            item.FillTile();
        }

        CheckForBomb(emptyTiles);
    }

    void CheckBoard()
    {
        if (!BoardManager.Instance.CheckAllTiles())
        {
            GameManager.Instance.ChangeState(FindMatches.Instance);
        }
        else
        {
            if (BoardManager.Instance.IsThereAvailableMove())
            {

                if (BoardManager.Instance.CheckBombs())
                {
                    GameManager.Instance.ChangeState(EndGame.Instance);
                    return;
                }
                
                GameManager.Instance.ChangeState(WaitInput.Instance);
            }
            else
            {
                GameManager.Instance.ChangeState(EndGame.Instance);
            }
        }

    }

    void FallTiles()
    {
        for (int i = 0; i < BoardManager.Instance.boardSizeX; i++)
        {
            for (int k = 0; k < BoardManager.Instance.boardSizeY; k++)
            {
                BoardManager.Instance.hexagonTilesArray[i, k].FallTiles();
            }
        }
    }

    void AnimateEmptyTiles(List<HexagonTile> emptyTiles)
    {
        float count = 0;
        float totalCount = emptyTiles.Count;
        foreach (var item in emptyTiles)
        {
            var tempY = item.onHexagon.GetGameObject().transform.position.y;
            item.onHexagon.GetGameObject().transform.position += Vector3.up * 2 * HexagonGeometry.CalculateVerticalSize();

            item.onHexagon.GetGameObject().transform.DOMoveY(tempY, fillTime).SetDelay((count / totalCount) * fillTime).SetEase(Ease.InOutCirc);
            count++;
        }
    }

    void AnimateFallTiles(List<HexagonTile> animateTiles)
    {
        foreach (var item in BoardManager.Instance.hexagonTilesList)
        {
            if (item.onHexagon == null) continue;
            if (item.transform.position != item.onHexagon.GetGameObject().transform.position)
            {
                item.onHexagon.GetGameObject().transform.DOMove(item.transform.position, fillTime / 2).SetEase(Ease.InOutCirc);
            }
        }
    }

    IEnumerator FillBoard()
    {
        var animateTiles = FindEmptyTiles();
        FallTiles();
        AnimateFallTiles(animateTiles);
        var emptyTiles = FindEmptyTiles();
        FillEmptyTiles(emptyTiles);
        AnimateEmptyTiles(emptyTiles);
        yield return new WaitForSeconds(fillTime * 2);
        CheckBoard();
    }

    void CheckForBomb(List<HexagonTile> emptyTiles)
    {
        if (ScoreManager.Instance.willAddBomb)
        {
            emptyTiles[Random.Range(0, emptyTiles.Count)].MakeBomb();
            ScoreManager.Instance.BombAdded();
        }
    }
}
