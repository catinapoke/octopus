using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleController : hasSpeed {
    protected override void Start () {
        moveSpeed = startMoveSpeed;
	}
	
	protected override void Update () {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(gameObject.transform.position.x, 50), moveSpeed * Time.deltaTime);
    }

    public void SetStartSpeed(float speed)
    {
        startMoveSpeed = speed;
    }
}
