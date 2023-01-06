using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public enum ZoneTypes{
        SpeedUp,
        SlowDown,
        Kill,
        DropOffZone
    }

    public ZoneTypes zoneTyp = ZoneTypes.SlowDown;

    public float speedUpRate;
    public float slowDownRate;
    public int healthDeduction;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Execute only if triggered with the player.
        if (collision.transform.CompareTag("Player"))
        {
            CarController carController = collision.gameObject.GetComponent<CarController>();

            // Depending on which zone it is, different methods are to be executed.
            switch (zoneTyp)
            {
                case ZoneTypes.SlowDown:
                    carController.SlowDown(slowDownRate);
                    break;
                case ZoneTypes.SpeedUp:
                    carController.SpeedUp(speedUpRate);
                    break;
                case ZoneTypes.Kill:
                    carController.Kill(healthDeduction);
                    break;
                case ZoneTypes.DropOffZone:
                    carController.DropOff();
                    break;
            }
        }
    }

}
