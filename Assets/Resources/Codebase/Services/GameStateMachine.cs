using UnityEngine;
using Events;
using UnityEngine.Events;

public class GameStateMachine 
{
    public enum GameStates { Start, Ingame, Pause, GameOver }
    public GameStates CurrentState { get; private set; } = GameStates.Start;

    public UnityAction PauseHandlers, PlayHandlers;
    public PlayerDeath playerDeathHandlers;
    public event GameStateChanged OnGameStateChanged;
    public GameStateMachine(GameStateChanged _gameStateChangedHandlers)
    {
        PauseHandlers += () => { ChangeGameState(GameStates.Pause); };
        PlayHandlers += () => { ChangeGameState(GameStates.Ingame); };
        Debug.Log(PlayHandlers.GetInvocationList().Length);
        playerDeathHandlers += () => { ChangeGameState(GameStates.GameOver); };
        Time.timeScale = 0f;
    }
    public void ChangeGameState(GameStates nextState)
    {
        switch (nextState)
        {
            case GameStates.Start:
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
        OnGameStateChanged?.Invoke(nextState);
    }
}
