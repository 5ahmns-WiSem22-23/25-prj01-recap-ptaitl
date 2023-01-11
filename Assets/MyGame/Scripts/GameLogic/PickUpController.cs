using UnityEngine;

public class PickUpController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player is triggered, the pickup methode is executed.
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CarController>().PickUp(this.gameObject);
        }
    }


}
