using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject spawnPoints;
    public GameObject pickUp;

    void Start()
    {
        spawnPickUp();
    }

    public void spawnPickUp()
    {
        // TODO: Checken, ob das neu gespawnte Object auf dem Car ist und wenn ja zerstören und ein neues spawnen (While Loop) und es darf nicht an der selben Stelle wie zuvor spawnen

        Instantiate(pickUp, spawnPoints.transform.GetChild(Random.Range(0, spawnPoints.transform.childCount - 1)).transform.position, Quaternion.identity);
    }
}