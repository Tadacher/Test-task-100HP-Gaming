using UnityEngine;

public class GameStateMachine 
{
    public enum GameStates { Bootstrap, Ingame, Pause, GameOver }
    public GameStates CurrentState { get; private set; } = GameStates.Bootstrap;

    public GameStateMachine()
    {
        Time.timeScale = 0f;
    }
    private void ChangeGameState(GameStates nextState)
    {
        switch (nextState)
        {
            case GameStates.Bootstrap:
                Debug.LogWarning("Unavailable translation");
                break;
            case GameStates.Ingame:
                CurrentState = nextState;
                Time.timeScale = 1f;
                break;
            case GameStates.Pause:
                CurrentState = nextState;
                Time.timeScale = 0f;
                break;
            case GameStates.GameOver:
                CurrentState = nextState;
                Time.timeScale = 0f;
                break;
        }
    }

    public void StartGame() 
        => ChangeGameState(GameStates.Ingame);

    internal void PauseGame()
        => ChangeGameState(GameStates.Pause);
    
}
