using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasSpeed : MonoBehaviour {

    [SerializeField]
    protected float startMoveSpeed=9;
    [SerializeField]
    protected float moveSpeed;

	// Use this for initialization
	protected virtual void Start () {
        moveSpeed = startMoveSpeed;
	}

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    public virtual void SetStart()
    {
        moveSpeed = startMoveSpeed;
    }

    public virtual float GetStartMoveSpeed()
    {
        return startMoveSpeed;
    }

    public virtual void SetNewSpeed(float newSpeed)
    {
        if (newSpeed < 0)
            newSpeed = 0;
        moveSpeed = newSpeed;
        //Debug.Log(gameObject.name + "has new speed: " + moveSpeed);
    }
}
