using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUpdater : MonoBehaviour {

    public Text HpText;
    public GameObject Img;
    private Image HpBar;
    public GameObject Player;
    private PlayerController Pc;
    private int MaxHp;

	// Use this for initialization
	void Start () {
        HpBar = Img.GetComponent<Image>();
        Pc = Player.GetComponent<PlayerController>();
        MaxHp = Pc.GetMaxHp();
	}
	
	// Update is called once per frame
	void Update () {
        float cur_hp = Pc.GetCurrentHp();
        HpText.text = cur_hp.ToString();
        HpBar.fillAmount = cur_hp / MaxHp;
	}
}
