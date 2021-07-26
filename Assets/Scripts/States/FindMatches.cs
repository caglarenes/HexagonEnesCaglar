using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class FindMatches : Singleton<FindMatches>, IState
{
    public GameObject destroyParticle;

    public void OnGroupSelect(HexagonGroup selectedGroup)
    {
        throw new System.NotImplementedException();
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
        var matchedGroups = FindGroups();
        DestroyHexs(matchedGroups);
        GameManager.Instance.ChangeState(FillBoardAgain.Instance);
    }

    List<HexagonGroup> FindGroups()
    {
        var matchedGroups = new List<HexagonGroup>();
        foreach (var item in BoardManager.Instance.hexagonGroups)
        {
            if (item.CheckGroup())
            {
                matchedGroups.Add(item);
            }
        }
        return matchedGroups;
    }

    void DestroyHexs(List<HexagonGroup> hexGroups)
    {
        foreach (var item in hexGroups)
        {
            foreach (var hexTile in item.hexagons)
            {
                if (hexTile.onHexagon != null)
                {
                    CreateDestroyAnimation(hexTile.onHexagon);
                    hexTile.onHexagon.TurnToPoint();
                    hexTile.onHexagon = null;
                }
            }
        }
    }

    void CreateDestroyAnimation(Hexagon hex)
    {
        var temp = LeanPool.Spawn(destroyParticle, hex.GetGameObject().transform.position, Quaternion.identity);
        temp.GetComponent<DeathParticle>().currentHexColor = hex.GetGameObject().GetComponent<SpriteRenderer>().color;
        temp.GetComponent<DeathParticle>().StartParticle();
    }
}
