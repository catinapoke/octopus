using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour {

    public float SpeedDownCoef;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;

        if (other.tag == "Player")
        {
            Debug.Log("SlowDown " + other.tag);
            PlayerController temp = other.GetComponent<PlayerController>();
            temp.MultiplyDownSpeed(1 / SpeedDownCoef);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        if (other.tag == "Player")
        {
            Debug.Log("SpeedUp " + other.tag);
            PlayerController temp = other.GetComponent<PlayerController>();
            temp.MultiplyDownSpeed(SpeedDownCoef);
        }
    }
}
