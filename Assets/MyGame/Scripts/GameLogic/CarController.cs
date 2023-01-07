using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [Range(0,10)]
    public float drivingSpeed;

    [SerializeField]
    [Range(0, 20)]
    float maxDrivingSpeed;

    [SerializeField]
    [Range(0, 10)]
    float rotationSpeed;

    [SerializeField]
    [Range(0, 2)]
    float pickUpSlowDown;

    float tempDrivingSpeed;

    [Range(0, 30)]
    public int maxHealth;

    [HideInInspector]
    public float health;

    [SerializeField]
    public Material healthMat;


    [SerializeField]
    GameSceneManager gameManager;

    Rigidbody2D ridbody;

    private void Start()
    {
        ridbody = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    void Update()
    {
        // Set player health every frame and check if it is zero.
        healthMat.SetFloat("_Health", Mathf.InverseLerp(0, maxHealth, health));

        // If health is zero, the game is over.
        if (health <= 0)
        {
            GlobalGameManager.currentGameState = GlobalGameManager.GameStates.GameOver;
            SceneManager.LoadScene("StartScene");
        }
    }


    private void FixedUpdate()
    {
        // By clamping the driving speed, the player is prevented from becoming infinitely fast or not moving at all.
        float drivingSpeedOverride = Mathf.Clamp(drivingSpeed, 0.3f, maxDrivingSpeed);

        // In order for the rotation to work realistically when driving backwards, it must be inverted.
        float rotationSpeedOverride = Input.GetAxis("Vertical") >= 0 ? rotationSpeed : -rotationSpeed;

        // By using the rotate method, quaternion conversions can be omitted.
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeedOverride);

        // The velocity of the rigid body can be used to control the forward and reverse movement.
        ridbody.velocity = transform.rotation * new Vector2(0, Input.GetAxis("Vertical")) * drivingSpeedOverride; ;
    }


    // The player gets slower.
    public void SlowDown(float slowDownRate)
    {
        drivingSpeed -= slowDownRate;

        // The change must also apply to the tempSpeed so that nothing is lost after the pickup is discarded.
        tempDrivingSpeed -= slowDownRate;
    }

    // The player gets faster.
    public void SpeedUp(float speedUpRate)
    {
        drivingSpeed += speedUpRate;

        // The change must also apply to the tempSpeed so that nothing is lost after the pickup is discarded.
        tempDrivingSpeed += speedUpRate;
    }

    // The player loses health.
    public void Kill(int healthDeduction)
    {
        health -= healthDeduction;
    }

    // If the player carries a pickup, it should be destroyed and the player gets faster again.
    public void DropOff()
    {
        if (transform.childCount != 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            GameSceneManager.score++;
            gameManager.spawnPickUp();
            drivingSpeed = tempDrivingSpeed;
        }
    }

    // The player becomes the parent so the pickup follows the player and the player slows down.
    public void PickUp(GameObject pickUp)
    {
        pickUp.transform.SetParent(gameObject.transform);
        pickUp.transform.position = transform.position;
        pickUp.transform.rotation = transform.rotation;
        pickUp.transform.localScale *= 0.5f;
        tempDrivingSpeed = drivingSpeed;
        drivingSpeed -= pickUpSlowDown;
    }
}