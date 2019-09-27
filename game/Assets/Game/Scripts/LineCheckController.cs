using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LineCheckController : movingShit
{
    public int spawnerNum;
    private Spawner sr;
    protected override void Start () {
        startMoveSpeed = sr.GetLineSpeed();
        base.Start();
        //startMoveSpeed = Settings.settings.StuffStartSpeed;
    }

    public void SetStartSpeed(float speed)
    {
        startMoveSpeed = speed;
        moveSpeed = speed;
        Debug.Log(string.Format("99999999999999999999999999999999999999999999999 {0} line checker has new speed: {1}",spawnerNum, speed));
    }

    protected override void Update () {
        base.Update();
	}

    void onDestroy()
    {
        sr.spawnedObj.Remove(gameObject);
    }

    public void SetParentSpawner(Spawner _sr)
    {
        sr = _sr;
    }
}
