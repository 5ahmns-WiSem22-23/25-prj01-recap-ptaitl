using UnityEngine;

public class CarController : MonoBehaviour
{
    public float drivingSpeed;
    public float rotationSpeed;

    public Rigidbody2D rb;
    public GameManager gm;


    float rotation;

    void Update()
    {
        // TODO: Speed Limit

        rb.AddRelativeForce(new Vector2(0, Input.GetAxis("Vertical") * (1 / drivingSpeed) * (1 / Time.deltaTime)));
        rotation += Input.GetAxis("Horizontal") * (1 / Time.deltaTime) * (1 / rotationSpeed) * (Input.GetAxis("Vertical") < 0 ? 1 : -1);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PickUp"))
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
            collision.gameObject.transform.position = transform.position;
            collision.gameObject.transform.rotation = transform.rotation;
            collision.gameObject.transform.localScale *= 0.5f;
        }
        else if (collision.transform.CompareTag("DropOffZone"))
        {
            if (transform.childCount != 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }

                gm.spawnPickUp();
            }
        }
    }
}
