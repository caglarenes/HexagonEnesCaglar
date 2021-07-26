using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexagonSelector : Singleton<HexagonSelector>
{
    public HexagonGroup selectedGroup;

    public void SetSelected(HexagonGroup selected)
    {
        selectedGroup = selected;

        transform.position = Helper.Depth(selectedGroup.transform.position, -1);

        if (selectedGroup.isLeft)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        if (!gameObject.activeSelf)
        {
            ShowSelector();
        }
    }

    public void HideSelector()
    {
        gameObject.SetActive(false);
    }

    public void ShowSelector()
    {
        gameObject.SetActive(true);
    }

    public bool IsSelected()
    {
        if (selectedGroup is null)
        {
            return false;
        }

        return true;
    }

    public void RotateClockwise(float turnTime)
    {
        selectedGroup.RotateClockwise(turnTime);
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, -120), turnTime, RotateMode.Fast).SetEase(Ease.InOutSine);
    }

    public void RotateCounterClockwise(float turnTime)
    {
        selectedGroup.RotateCounterClockwise(turnTime);
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 120), turnTime, RotateMode.Fast).SetEase(Ease.InOutSine);
    }
}
