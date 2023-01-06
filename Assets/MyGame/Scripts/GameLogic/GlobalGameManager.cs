using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    // Using a static enum, fixed game states can be defined, which can be accessed beyond the different scenes.
    public enum GameStates
    {
        FirstGame,
        TimeOver,
        GameOver
    }

    public static GameStates currentGameState = GameStates.FirstGame;
}
