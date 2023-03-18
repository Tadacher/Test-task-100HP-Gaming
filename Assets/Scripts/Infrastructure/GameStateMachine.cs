using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.Events;

public class GameStateMachine : MonoBehaviour
{
    public enum GameStates { Start, Ingame, Pause, GameOver }
    internal GameStates CurrentState { get; private set; } = GameStates.Start;

    internal UnityAction PauseHandlers, PlayHandlers;
    internal PlayerDeath playerDeathHandlers;
    internal event GameStateChanged OnGameStateChanged;
    internal void Initialize()
    {
        PauseHandlers += () => { ChangeGameState(GameStates.Pause); };
        PlayHandlers += () => { ChangeGameState(GameStates.Ingame); };
        Debug.Log(PlayHandlers.GetInvocationList().Length);
        playerDeathHandlers += () => { ChangeGameState(GameStates.GameOver); };
        Time.timeScale = 0f;
    }
    internal void ChangeGameState(GameStates nextState)
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
