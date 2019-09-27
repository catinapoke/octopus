using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : hasSpeed {

    public FishType Type;
    int coef;
    private Spawner sr;

    [SerializeField]
    protected int points;
    [SerializeField]
    protected float seconds;
    [SerializeField]
    protected float value;

    public float GetValue()
    {
        return value;
    }

    public float GetSeconds()
    {
        return seconds;
    }

    public int GetPoints()
    {
        return points;
    }

    public enum FishType
    {
        Default,
        Gold,
        Bonus
    }

    protected override void Start()
    {
        base.Start();
    }
    // if from right need -1*50, else 50
    public void CoefRight(bool right)
    {
        coef = 1 - SupportM.bool2int(right) * 2;
    }

    public void SetParentSpawner(Spawner _sr)
    {
        sr = _sr;
    }

    void onDestroy()
    {
        sr.spawnedObj.Remove(gameObject);
    }

    // Update is called once per frame
    new protected virtual void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(50 * coef, gameObject.transform.position.y), moveSpeed * Time.deltaTime);
    }

	
}
