using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource clip;

    // Music should only start to play on first game.
    void Start()
    {
        if (GlobalGameManager.currentGameState == GlobalGameManager.GameStates.FirstGame)
        {
            clip.Play();
            Object.DontDestroyOnLoad(this.gameObject);
        }
    }

}
