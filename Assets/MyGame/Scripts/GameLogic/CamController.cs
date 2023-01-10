using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    float smoothTime;

    [SerializeField]
    GameObject car;

    [SerializeField]
    Vector3 offset;

    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 targetPosition = new Vector3(car.transform.position.x, car.transform.position.y, transform.position.z);

        // By using the smoothdamp function, the tracking of the player is made more gradual. this way it doesn't look like the ground is moving while the car is standing still in the middle, but like the car is moving and the camera is following the car.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + offset, ref velocity, smoothTime);
    }
}
