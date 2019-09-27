using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsBarController : MonoBehaviour {

    public Image[] Hearts;
    public Sprite[] heartSprites;//{0, 0.5 , 1}
    public PlayerController Pc;
    private int MaxHp;

    // Use this for initialization
    void Start()
    {
        MaxHp = Pc.GetMaxHp();
    }

    // Update is called once per frame
    void Update()
    {
        float cur_hp = Pc.GetCurrentHp();
        for (int i=0;i<Hearts.Length;i++)
        {
            if (cur_hp >= (i + 1) * 2)
                Hearts[i].overrideSprite = heartSprites[2];
            else if (cur_hp >= (i * 2 + 1))
                Hearts[i].overrideSprite = heartSprites[1];
            else
                Hearts[i].overrideSprite = heartSprites[0];
        }
    }
}
