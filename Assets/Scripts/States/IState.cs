using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    public void Start();

    public void OnHexagonGroupSwipeClockwise();
    public void OnHexagonGroupSwipeCounterClockwise();
    public void OnGroupSelect(HexagonGroup selectedGroup);

}
