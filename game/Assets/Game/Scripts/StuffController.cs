using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffController : hasSpeed {

    public StuffType Type;
    public int Points;
    public float Damage;
    
    public enum StuffType
    {
        Bad,
        Good,
        Bonus,
        Default
    }

    public void SetStartInfo(StuffInfo info)
    {
        startMoveSpeed = info.GetSpeed();
        Points = info.GetPoints();      
        Damage = info.GetDamage();
    }

    // Use this for initialization
    protected override void Start () {
        Type = StuffType.Default;
        base.Start();
        //DropSpeed = DropSpeedStart;
    }

    void onDestroy()
    {
        Camera.main.GetComponent<SpawnStuff>().spawnedObj.Remove(gameObject);
    }

    // Update is called once per frame
    new protected virtual void Update () {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(gameObject.transform.position.x, -50), moveSpeed * Time.deltaTime);
    }
}
