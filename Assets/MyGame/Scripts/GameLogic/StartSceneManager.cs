using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    Text statusDisplay;
    [SerializeField]
    Text scoreDisplay;
    [SerializeField]
    Text highscoreDisplay;

    int highscore;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore");

        // If the current score is higher than the stored highscore, set a new highscore.
        if(GameSceneManager.score > highscore){
            PlayerPrefs.SetInt("highscore", GameSceneManager.score);
            highscore = GameSceneManager.score;
        }
    }

    void Update()
    {
        // Depending on the current game state, a different text is displayed.
        switch (GlobalGameManager.currentGameState)
        {
            case GlobalGameManager.GameStates.FirstGame:
                statusDisplay.text = "Let's Go!";
                break;
            case GlobalGameManager.GameStates.GameOver:
                statusDisplay.text = GameSceneManager.score > highscore ? "New Highscore!" : "Game Over";
                break;
            case GlobalGameManager.GameStates.TimeOver:
                statusDisplay.text = GameSceneManager.score > highscore ? "New Highscore!" : "Time Over";
                break;
        }

        // The scores are inserted into the string by string interpolation.
        scoreDisplay.text = $"Score: {GameSceneManager.score.ToString()}";
        highscoreDisplay.text = $"Highscore: {highscore.ToString()}";
    }

    // When the GameScene is loaded, the score has to be reset.
    public void StartGame()
    {
        GameSceneManager.score = 0;
        SceneManager.LoadScene("GameScene");
    }
}
