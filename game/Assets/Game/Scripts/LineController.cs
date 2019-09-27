using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineController : MonoBehaviour {

    public UnityEvent EndLineEvent = new UnityEvent();
    public int spawnerNum;
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "LineCheck")
        {
            if (other.gameObject.GetComponent<LineCheckController>().spawnerNum == this.spawnerNum)
            {
                EndLineEvent.Invoke();
                Destroy(other.gameObject);
            }
        }
    }


}
