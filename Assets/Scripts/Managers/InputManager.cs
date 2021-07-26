using UnityEngine;
using Lean.Touch;

public class InputManager : Singleton<InputManager>
{
    public GameObject selectedGroup;
    public LeanSelectByFinger leanSelect;


    public void SwipeLeft(Vector3 swipeStartPosition, Vector3 swipeStopPosition)
    {
        if (HexagonSelector.Instance.selectedGroup is null)
        {
            //Nothing selected. Just ignore.
            return;
        }

        selectedGroup = HexagonSelector.Instance.selectedGroup.gameObject;

        FindRotationDirection(swipeStartPosition, swipeStopPosition, selectedGroup, true);
    }

    public void SwipeRight(Vector3 swipeStartPosition, Vector3 swipeStopPosition)
    {

        if (HexagonSelector.Instance.selectedGroup is null)
        {
            //Nothing selected. Just ignore.
            return;
        }

        selectedGroup = HexagonSelector.Instance.selectedGroup.gameObject;

        FindRotationDirection(swipeStartPosition, swipeStopPosition, selectedGroup, false);
    }

    public void FindRotationDirection(Vector3 swipeStartPosition, Vector3 swipeStopPosition, GameObject selectedGroup, bool isLeft)
    {
        if (swipeStartPosition.x > selectedGroup.transform.position.x && swipeStartPosition.y > selectedGroup.transform.position.y)
        {
            if (isLeft)
            {
                GameManager.Instance.currentState.OnHexagonGroupSwipeCounterClockwise();
            }
            else
            {
                GameManager.Instance.currentState.OnHexagonGroupSwipeClockwise();
            }
        }
        else if (swipeStartPosition.x > selectedGroup.transform.position.x && swipeStartPosition.y <= selectedGroup.transform.position.y)
        {
            if (isLeft)
            {
                GameManager.Instance.currentState.OnHexagonGroupSwipeClockwise();
            }
            else
            {
                GameManager.Instance.currentState.OnHexagonGroupSwipeCounterClockwise();
            }
        }
        else if ((swipeStartPosition.x <= selectedGroup.transform.position.x && swipeStartPosition.y <= selectedGroup.transform.position.y))
        {
            if (isLeft)
            {
                GameManager.Instance.currentState.OnHexagonGroupSwipeClockwise();
            }
            else
            {
                GameManager.Instance.currentState.OnHexagonGroupSwipeCounterClockwise();
            }
        }
        else
        {
            if (isLeft)
            {
                GameManager.Instance.currentState.OnHexagonGroupSwipeCounterClockwise();
            }
            else
            {
                GameManager.Instance.currentState.OnHexagonGroupSwipeClockwise();
            }
        }

    }

    public void GroupSelect(HexagonGroup selectedGroup)
    {
        GameManager.Instance.GroupSelection(selectedGroup);
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
}
