using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpace : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        Debug.Log("Collision with " + other.tag);
        if (other.tag == "Player")
        {
            PlayerController temp = other.GetComponent<PlayerController>();
            temp.Die();
        }
        else if (other.tag == "Stuff")
        {
            Destroy(other, 3);
        }
		else if (other.tag == "Fish")
        {
            Destroy(other);
        }
    }
}
