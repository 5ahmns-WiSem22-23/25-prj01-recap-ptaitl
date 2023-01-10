using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject spawnPoints;
    [SerializeField]
    GameObject pickUp;
    [SerializeField]
    Text scoreDisplay;
    [SerializeField]
    Text gameTimeDisplay;
    public static int score;
    float currentGameTime;
    [SerializeField]
    float maxGameTime;

    void Start()
    {
        // At the beginning of the game the first pickup is spawned.
        spawnPickUp();
    }

    void Update()
    {
        // Using the delta time, the time can be counted up independently of the frame rate.
        currentGameTime += Time.deltaTime;

        // If the time is over, the StartScene is loaded.
        if(currentGameTime >= maxGameTime)
        {
            GlobalGameManager.currentGameState = GlobalGameManager.GameStates.TimeOver;
            SceneManager.LoadScene("StartScene");
        }

        // The score and the gameTime are updated every frame.
        scoreDisplay.text = score.ToString();
        gameTimeDisplay.text = Mathf.Round((maxGameTime - currentGameTime)).ToString();
    }
    
    public void spawnPickUp()
    {
        // Using predefined spawn points instead of randomly generated positions prevents a spawn point from spawning too close to the target location or on another static game element.
        GameObject spawnedPickUp = Instantiate(pickUp, spawnPoints.transform.GetChild(Random.Range(0, spawnPoints.transform.childCount - 1)).transform.position, Quaternion.identity);


        // If the spawned pickup is spawned on the player it is immediately destroyed again and spawned in a different position.
        while (Physics2D.Raycast(spawnedPickUp.transform.position, -Vector2.up, 0.5f).collider.tag == "Player")
        {
            Destroy(spawnedPickUp);
            spawnedPickUp = Instantiate(pickUp, spawnPoints.transform.GetChild(Random.Range(0, spawnPoints.transform.childCount - 1)).transform.position, Quaternion.identity);
        }
    }
}