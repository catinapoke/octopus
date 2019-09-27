using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fasttest : MonoBehaviour {

    // Use this for initialization
    int tr;
    int fal;

	void Start () {
        tr = 0;
        fal = 0;
	}
	
	// Update is called once per frame
	void Update () {
        int temp = Random.Range(0, 2);
        if (temp == 1)
            tr++;
        else
            fal++;
        Debug.Log(string.Format("random test: {0}_{1}_{2}",temp,tr,fal));
	}
}
