using System;
using UnityEngine;
using UnityEngine.TextCore;

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

    public string zoneId;

    void Start()
    {
        zoneId = GenerateId(20);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Execute only if triggered with the player.
        if (collision.transform.CompareTag("Player"))
        {
            CarController carController = collision.gameObject.GetComponent<CarController>();

            // Depending on which zone it is, different methods are to be executed.
            switch (zoneTyp)
            {
                case ZoneTypes.SlowDown:
                    if (zoneId == carController.previousZoneId) return;
                    carController.SlowDown(slowDownRate);
                    break;
                case ZoneTypes.SpeedUp:
                    if (zoneId == carController.previousZoneId) return;
                    carController.SpeedUp(speedUpRate);
                    break;
                case ZoneTypes.Kill:
                    carController.Kill(healthDeduction);
                    break;
                case ZoneTypes.DropOffZone:
                    carController.DropOff();
                    break;
            }

            // As soon as the car triggers a zone, the Id is set as the previous Id.
            carController.previousZoneId = zoneId;
        }
    }

    // This function returns a random string.
    string GenerateId(int length)
    {
        string id = "";
        string glyphs = "abcdefghjklmnopqrstuvwxyz0123456789";

        for (int i = 0; i < length; i++)
        {
            id += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
        }

        return id;
    }

}
