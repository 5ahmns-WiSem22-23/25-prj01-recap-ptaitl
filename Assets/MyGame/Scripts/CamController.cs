using UnityEngine;

public class CamController : MonoBehaviour
{
    public float smoothTime;
    public GameObject car;

    void FixedUpdate()
    {
        Vector3 _velocity = Vector3.zero;
        Vector3 _targetPosition = new Vector3(car.transform.position.x, car.transform.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, smoothTime);
    }
}
