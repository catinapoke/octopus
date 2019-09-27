using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodStuff : StuffController
{

	// Use this for initialization
	protected override void Start () {
        Type = StuffType.Good;
        base.SetStart();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
    }
}
