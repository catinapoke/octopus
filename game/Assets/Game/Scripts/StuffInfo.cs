using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StuffInfo", menuName = "Stuff info", order = 51)]
public class StuffInfo : ScriptableObject {

    [SerializeField]
    private float speed;
    public float GetSpeed()
    {
        return speed;
    }
    [SerializeField]
    private int points;
    public int GetPoints()
    {
        return points;
    }
    [SerializeField]
    private float damage;
    public float GetDamage()
    {
        return damage;
    }

}
