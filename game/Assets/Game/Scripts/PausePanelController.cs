using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelController : MonoBehaviour {

    public Text Points;
    public Settings settings;
    public PlayerController pc;
	void Start () {
        pc = settings.Player.GetComponent<PlayerController>();
        Debug.Log("Player.name: "+settings.Player.name + " pc link+" + pc);
	}
	

	void OnEnable () {
        Points.text = pc.GetPoints().ToString();
	}

    public void Restart()
    {
        gameObject.SetActive(false);
        settings.Restart();
    }
}
