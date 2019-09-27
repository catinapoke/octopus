using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStuff : StuffController
{

    public enum BonusType
    {
        Default,
        DamageImmunity,
        UnlimitedTouches,
    }

    public BonusType bonusType;
    public float EffectTime;

    protected override void Start () {
        base.SetStart();
        Type = StuffType.Bonus;
    }

    protected override void Update () {
        base.Update();
    }
}
