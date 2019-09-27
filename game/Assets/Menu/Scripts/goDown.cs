using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goDown : MonoBehaviour {

    // Use this for initialization
    public float moveSpeed;
	void Start () {
		
	}

    // Update is called once per frame
    protected virtual void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(gameObject.transform.position.x, -50), moveSpeed * Time.deltaTime);
    }
}
