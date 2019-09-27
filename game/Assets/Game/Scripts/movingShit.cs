using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingShit : hasSpeed {

    public Vector2 way;
    protected override void Start()
    {
        base.Start();
        way = Vector3.Normalize(way);
        way *= 50;
    }

    // Update is called once per frame
    new protected virtual void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    way, moveSpeed * Time.deltaTime);
    }
}
