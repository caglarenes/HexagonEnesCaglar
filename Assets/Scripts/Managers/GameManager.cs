using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public BoardManager boardManager;

    public IState currentState;

    void Start()
    {
        ChangeState(SetupGame.Instance);
    }


    public void ChangeState(IState newState)
    {
        currentState = newState;
        currentState.Start();
    }

    public void GroupSelection(HexagonGroup selectedGroup)
    {
        currentState.OnGroupSelect(selectedGroup);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
