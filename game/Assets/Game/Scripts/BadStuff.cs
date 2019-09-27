using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadStuff : StuffController
{
    public static float[] DamageTypes = {0.5f , 1f, 2f, 999f};//Small, Medium, Big, Boss(one-shot) How i can set it up in Unity Editor? hmmmmm
    public float[] SpawnRatio = { 3, 2, 1}; //Small:Medium:Big
    public BadType badType;
    public StuffInfo BadInfo;

    public enum BadType
    {
        Small = 0,
        Medium = 1,
        Big = 2,
        Boss = 3,
        Debug
    }

    // Use this for initialization
    protected override void Start () {
        SetStartInfo(BadInfo);
        base.SetStart();
        Type = StuffType.Bad;
        badType = ChooseBadType();
        

    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}

    BadType ChooseBadType()
    {
        float sum = 0;
        foreach (float temp in SpawnRatio)
            sum += temp;
        float percent = Random.Range(0, sum);
        sum = 0;
        for (int i=0;i<SpawnRatio.Length;i++)
        {
            sum += SpawnRatio[i];
            if (sum >= percent)
                return (BadType)i;

        }
        return BadType.Debug;
    }

    float GetDamage()
    {
        return DamageTypes[(int)badType];
    }
}
